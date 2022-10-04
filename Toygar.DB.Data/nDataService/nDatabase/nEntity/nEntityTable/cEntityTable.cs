
using Bootstrapper.Core.nCore;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toygar.DB.Data.nConfiguration;

namespace Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable
{
    public class cEntityTable 
    {
        public cEntityManager EntityManager { get; set; }
        public Type EntityType { get; set; }
        public List<cEntityColumn> EntityFieldList { get; set; }

        public cEntityTable(cEntityManager _EntityManager, Type _EnitityType)
        {
            EntityManager = _EntityManager;
            EntityType = _EnitityType;
            Reload();
        }

        public static string GetTableNameByTypeEntity(Type _Type)
        {
            if (_Type.Name.StartsWith("c") && _Type.Name.EndsWith("Entity"))
            {
                return _Type.Name.Substring(1, _Type.Name.Length - ("Entity".Length + 1)) + "s";
            }
            else
            {
                throw new Exception(_Type.Name + " : Bir DB Entity 'c' harfiyle başlayıp 'Entity ile bitmelidir!'");
            }
        }

        public cEntityColumn GetEntityColumnByName(string _Name)
        {
            return EntityFieldList.Where(__Item => __Item.ColumnName == _Name).FirstOrDefault();
        }

        public bool ControlColumnByName(string _Name)
        {
            if (GetEntityColumnByName(_Name) != null)
            {
                return true;
            }
            else
            {
                throw new Exception(TableName + " tablosunda " + _Name + " kolonu bulunamadı");
            }
        }

        public bool ControlColumnByName<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            try
            {
                return ControlColumnByName(EntityManager.GetEntityTableByEnitityType<TRelationEntity>().TableForeing_ColumnName_For_InOtherTable);
            }
            catch(Exception _Ex)
            {
				EntityManager.Database.App.Loggers.SqlLogger.LogError(_Ex);
				throw new Exception(TableName +" tablosunda " + EntityManager.GetEntityTableByEnitityType<TRelationEntity>().TableForeing_ColumnName_For_InOtherTable + " kolonu bulunamadı");
            }
        }

        public string GetRelationColumnName<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            return EntityManager.GetEntityTableByEnitityType<TRelationEntity>().TableForeing_ColumnName_For_InOtherTable;
        }

        public cEntityColumn GetEntityIDColumn()
        {
            return GetEntityColumnByName(cEntityColumn.ID_ColumnName);
        }

        public string TableName
        {
            get
            {
                return GetTableNameByTypeEntity(EntityType);
            }
        }

        public string TableForeing_ColumnName_For_InOtherTable
        {
            get
            {
                return TableName.Substring(0, TableName.Length - 1) + cEntityColumn.ID_ColumnName;
            }
        }

        public void Reload()
        {
            EntityFieldList = new List<cEntityColumn>();
            List<PropertyInfo> __PropertList = EntityType.GetProperties().ToList();
            foreach (PropertyInfo __PropertyInfo in __PropertList)
            {
                bool? __IsVirtual = __PropertyInfo.IsVirtual();
                if (__IsVirtual != null && __IsVirtual.Value && (__PropertyInfo.PropertyType.IsPrimitiveWithString() || __PropertyInfo.PropertyType.IsAssignableFrom(typeof(DateTime))))
                {
                    if (__PropertyInfo.GetCustomAttribute<TDBField>() != null)
                    {
                        EntityFieldList.Add(new cEntityColumn(this, __PropertyInfo.Name, __PropertyInfo.PropertyType, __PropertyInfo.GetCustomAttribute<TDBField>()));
                    }                    
                }                
            }
        }

        public List<cEntityTable> GetThisTableReferencedBy()
        {
            List<cEntityTable> __Result = new List<cEntityTable>();
            List<Type> __Types = EntityManager.Database.App.Handlers.AssemblyHandler.GetTypesFromBaseType(EntityManager.Database.GetEntityType(), null);
            foreach (Type __Type in __Types)
            {
                if (__Type != EntityType)
                {
                    List<PropertyInfo> __PropertList = __Type.GetProperties().ToList();
                    foreach (PropertyInfo __PropertyInfo in __PropertList)
                    {
                        bool? __IsVirtual = __PropertyInfo.IsVirtual();
                        if (__IsVirtual != null && __IsVirtual.Value && !__PropertyInfo.PropertyType.IsPrimitiveWithString() && !__PropertyInfo.PropertyType.IsAssignableFrom(typeof(DateTime))  && __PropertyInfo.PropertyType == EntityType)
                        {
                            __Result.Add(EntityManager.GetEntityTableByEnitityType(__Type));
                        }
                        else if (typeof(IEntityList).IsAssignableFrom(__PropertyInfo.PropertyType))
                        {
                            Type __PropertyType = __PropertyInfo.PropertyType.GetGenericArguments()[0];
                            if (__PropertyType == EntityType)
                            {
                                __Result.Add(EntityManager.GetEntityTableByEnitityType(__Type));
                            }
                        }
                        else if (typeof(IMappedEntity).IsAssignableFrom(__PropertyInfo.PropertyType))
                        {
                            Type __PropertyType = __PropertyInfo.PropertyType.GetGenericArguments()[0];
                            if (__PropertyType == EntityType)
                            {
                                __Result.Add(EntityManager.GetEntityTableByEnitityType(__Type));
                            }
                        }
                    }
                }
            }
            return __Result;
        }

        public void ReferencedEntity()
        {
            List<Type> __Types = EntityManager.Database.App.Handlers.AssemblyHandler.GetTypesFromBaseType(EntityManager.Database.GetEntityType(), null);
            List<PropertyInfo> __PropertList = EntityType.GetAllProperties().ToList();
            foreach (PropertyInfo __PropertyInfo in __PropertList)
            {
                bool? __IsVirtual = __PropertyInfo.IsVirtual();
                if (__IsVirtual != null && __IsVirtual.Value && !__PropertyInfo.PropertyType.IsPrimitiveWithString() && !__PropertyInfo.PropertyType.IsAssignableFrom(typeof(DateTime)))
                {
                    if (EntityManager.Database.GetEntityType().IsAssignableFrom(__PropertyInfo.PropertyType))
                    {
                        if (EntityType == __PropertyInfo.PropertyType)
                        {
                            cEntityTable __EntityTable = EntityManager.GetEntityTableByEnitityType(__PropertyInfo.PropertyType);
                            __EntityTable.EntityFieldList.Add(new cEntityColumn(__EntityTable, __PropertyInfo.Name, typeof(int), new TDBField(_Nullable: true, _DataType: EDataType.Bigint, _ToForeingKey: EntityType)));
                        }
                        else
                        {
                            cEntityTable __EntityTable = EntityManager.GetEntityTableByEnitityType(__PropertyInfo.PropertyType);
                            __EntityTable.EntityFieldList.Add(new cEntityColumn(__EntityTable, TableForeing_ColumnName_For_InOtherTable, typeof(int), new TDBField(_Nullable: false, _DataType: EDataType.Bigint, _DefaultValue: 0, _ToForeingKey: EntityType)));
                        }
                    }
                    else if (typeof(IEntityList).IsAssignableFrom(__PropertyInfo.PropertyType))
                    {
                        Type __PropertyType = __PropertyInfo.PropertyType.GetGenericArguments()[0];
                        if (EntityManager.Database.GetEntityType().IsAssignableFrom(__PropertyType))
                        {
                            cEntityTable __EntityTable = EntityManager.GetEntityTableByEnitityType(__PropertyType);
                            __EntityTable.EntityFieldList.Add(new cEntityColumn(__EntityTable, TableForeing_ColumnName_For_InOtherTable, typeof(int), new TDBField(_Nullable: false, _DataType: EDataType.Bigint, _DefaultValue: 0, _ToForeingKey: EntityType)));
                        }
                    }
                    else if (typeof(IMappedEntity).IsAssignableFrom(__PropertyInfo.PropertyType))
                    {
                        Type __PropertyType = __PropertyInfo.PropertyType.GetGenericArguments()[0];
                        if (EntityManager.Database.GetEntityType().IsAssignableFrom(__PropertyType))
                        {
                            cEntityTable __EntityTable = EntityManager.GetEntityTableByEnitityType(__PropertyType);
                            __EntityTable.EntityFieldList.Add(new cEntityColumn(__EntityTable, TableForeing_ColumnName_For_InOtherTable, typeof(int), new TDBField(_Nullable: false, _DataType: EDataType.Bigint, _DefaultValue: 0, _ToForeingKey: EntityType)));
                        }
                    }
                }
            }
        }
    }
}
