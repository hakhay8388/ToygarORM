using Bootstrapper.Core.nCore;
using Toygar.DB.Data.nDataService.nDatabase;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toygar.DB.Data.nDataService.nDatabase.nIDController;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using Toygar.DB.Data.nDataService.nDatabase.nDBInfo; 
using Toygar.DB.Data.nConfiguration; 
using Bootstrapper.Core.nApplication;
using System.Dynamic;

namespace Toygar.DB.Data.nDataService.nDatabase.nEntity
{
    public class cEntityManager : cBaseDatabaseComponent
    {
        public List<cEntityTable> EntityTableList { get; set; }
        public cEntityManager(IDatabase _Database)
            : base(_Database)
        {
            Reload();
            ReloadReferencedEntity();
        }

        public void Reload()
        {
            EntityTableList = new List<cEntityTable>();
            List<Type> __Type = Database.App.Handlers.AssemblyHandler.GetTypesFromBaseType(Database.GetEntityType(), null);
            foreach (Type __EntityType in __Type)
            {
                EntityTableList.Add(new cEntityTable(this, __EntityType));
            }
            EntityTableList.Add(new cEntityTable(this, typeof(cDBInfoEntity)));
            EntityTableList.Add(new cEntityTable(this, typeof(cIDCounterEntity)));
        }

        public IList GetEntityByColumnValue(Type _Type, string _ColumnName, object _Value)
        {
            if (Database.GetEntityType().IsAssignableFrom(_Type))
            {
                Type __Type = GetType();
                MethodInfo __MethodInfo = __Type.GetMethod("GetEntityByColumnValue", new Type[] { typeof(string), typeof(object) });
                __MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _Type });
                IList __Temp = (IList)__MethodInfo.Invoke(this, new object[] { _ColumnName, _Value });
                return __Temp;
            }
            else
            {
                throw new Exception("cEntityManager->GetEntityByColumnValue");
            }
        }


        public object GetColumnValueInEntiyByColumnValue(Type _Type, string __GettingColumnName, string _ColumnName, object _Value)
        {
            if (Database.GetEntityType().IsAssignableFrom(_Type))
            {
                Type __Type = GetType();
                MethodInfo __MethodInfo = __Type.GetMethod("GetColumnValueInEntiyByColumnValue", new Type[] { typeof(string), typeof(string), typeof(object) });
                __MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _Type });
                return __MethodInfo.Invoke(this, new object[] { __GettingColumnName, _ColumnName, _Value });
            }
            else
            {
                throw new Exception("cEntityManager->GetColumnValueInEntiyByColumnValue");
            }
        }

        public int DeleteEntityByColumnValue(Type _Type, string _ColumnName, object _Value)
        {
            if (Database.GetEntityType().IsAssignableFrom(_Type))
            {
                Type __Type = GetType();
                MethodInfo __MethodInfo = __Type.GetMethod("DeleteEntityByColumnValue", new Type[] { typeof(string), typeof(object) });
                __MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _Type });
                return (int)__MethodInfo.Invoke(this, new object[] { _ColumnName, _Value });
            }
            else
            {
                throw new Exception("cEntityManager->DeleteEntityByColumnValue");
            }
        }

        public int DeleteEntityByDoubleColumnValue(Type _Type, string _ColumnName, object _Value, string _ColumnName2, object _Value2)
        {
            if (Database.GetEntityType().IsAssignableFrom(_Type))
            {
                Type __Type = GetType();
                MethodInfo __MethodInfo = __Type.GetMethod("DeleteEntityByDoubleColumnValue", new Type[] { typeof(string), typeof(object), typeof(string), typeof(object) });
                __MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _Type });
                return (int)__MethodInfo.Invoke(this, new object[] { _ColumnName, _Value, _ColumnName2, _Value2 });
            }
            else
            {
                throw new Exception("cEntityManager->DeleteEntityByDoubleColumnValue");
            }

        }

        public IList GetEntityByColumnValue(Type _Type, string _ColumnName, object _Value, int _StartRowNumber, int _EndRowNumber)
        {
            if (Database.GetEntityType().IsAssignableFrom(_Type))
            {
                Type __Type = GetType();
                MethodInfo __MethodInfo = __Type.GetMethod("GetEntityByColumnValue", new Type[] { typeof(string), typeof(object), typeof(int), typeof(int) });
                __MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _Type });
                IList __Temp = (IList)__MethodInfo.Invoke(this, new object[] { _ColumnName, _Value, _StartRowNumber, _EndRowNumber });
                return __Temp;
            }
            else
            {
                throw new Exception("cEntityManager->GetEntityByColumnValue");
            }
        }

        public IList GetEntityByColumnValue(Type _MapType, Type _MappedType, string _ColumnName, object _Value, int _StartRowNumber, int _EndRowNumber)
        {
            if (Database.GetEntityType().IsAssignableFrom(_MappedType))
            {
                Type __Type = GetType();
                MethodInfo __MethodInfo = __Type.GetMethod("GetEntityByColumnValueForMapped", new Type[] { typeof(string), typeof(object), typeof(int), typeof(int) });
                __MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _MapType, _MappedType });
                IList __Temp = (IList)__MethodInfo.Invoke(this, new object[] { _ColumnName, _Value, _StartRowNumber, _EndRowNumber });
                return __Temp;
            }
            else
            {
                throw new Exception("cEntityManager->GetEntityByColumnValue");
            }
        }

		public List<dynamic> GetEntityByColumnValueForMappedToDynamicObjectList(Action<dynamic> _Action, Type _MapType, Type _MappedType, string _ColumnName, object _Value)
		{
			if (Database.GetEntityType().IsAssignableFrom(_MappedType))
			{
				Type __Type = GetType();
				MethodInfo __MethodInfo = __Type.GetMethod("GetEntityByColumnValueForMappedToDynamicObjectList", new Type[] { typeof(Action<dynamic>), typeof(string), typeof(object) });
				__MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _MapType, _MappedType });
				List<dynamic> __Temp = (List<dynamic>)__MethodInfo.Invoke(this, new object[] { _Action,  _ColumnName, _Value});
				return __Temp;
			}
			else
			{
				throw new Exception("cEntityManager->GetEntityByColumnValueForMappedToDynamicObjectList");
			}
		}

		public IList GetEntityByColumnValue(Type _MapType, Type _MappedType, string _ColumnName, object _Value)
        {
            if (Database.GetEntityType().IsAssignableFrom(_MappedType))
            {
                Type __Type = GetType();
                MethodInfo __MethodInfo = __Type.GetMethod("GetEntityByColumnValueForMapped", new Type[] { typeof(string), typeof(object) });
                __MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _MapType, _MappedType });
                IList __Temp = (IList)__MethodInfo.Invoke(this, new object[] { _ColumnName, _Value });
                return __Temp;
            }
            else
            {
                throw new Exception("cEntityManager->GetEntityByColumnValue");
            }
        }

		public int GetEntityCountByColumnValue(Type _Type, string _ColumnName, object _Value)
        {
            if (Database.GetEntityType().IsAssignableFrom(_Type))
            {
                Type __Type = GetType();
                MethodInfo __MethodInfo = __Type.GetMethod("GetEntityCountByColumnValue", new Type[] { typeof(string), typeof(object) });
                __MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _Type });
                int __Temp = (int)__MethodInfo.Invoke(this, new object[] { _ColumnName, _Value });
                return __Temp;
            }
            else
            {
                throw new Exception("cEntityManager->GetEntityCountByColumnValue");
            }
        }

        public int GetEntityCountByColumnValueForMapped(Type _TMapEntity, Type _TMappedEntity, string _ColumnName, object _Value)
        {
            if (Database.GetEntityType().IsAssignableFrom(_TMapEntity))
            {
                Type __Type = GetType();
                MethodInfo __MethodInfo = __Type.GetMethod("GetEntityCountByColumnValueForMapped", new Type[] { typeof(string), typeof(object) });
                __MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _TMapEntity, _TMappedEntity });
                int __Temp = (int)__MethodInfo.Invoke(this, new object[] { _ColumnName, _Value });
                return __Temp;
            }
            else
            {
                throw new Exception("cEntityManager->GetEntityCountByColumnValueForMapped");
            }
        }


        public int GetEntityCountByColumnValue<TEntity>(string _ColumnName, object _Value) where TEntity : cBaseEntity
        {
            List<TEntity> __Result = new List<TEntity>();
            Type __Type = typeof(TEntity);
            string __CountAlias = AliasGenerator.GetNewAlias("CountAlias");
            cEntityTable __Table = GetEntityTableByEnitityType(__Type);
            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelect(__Table.TableName, _ColumnName + "=:" + _ColumnName);
            __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelectCount(__Sql, __CountAlias);
            __Sql.SetParameter(_ColumnName, _Value);
            DataTable __DataTable = Database.DefaultConnection.Query(__Sql);
            return Convert.ToInt32(__DataTable.Rows[0][__CountAlias].ToString());
        }

        public int GetEntityCountByColumnValueForMapped<TMapEntity, TMappedEntity>(string _ColumnName, object _Value)
            where TMapEntity : cBaseEntity
            where TMappedEntity : cBaseEntity
        {
            List<TMappedEntity> __Result = new List<TMappedEntity>();
            string __CountAlias = AliasGenerator.GetNewAlias("CountAlias");
            Type __MappedType = typeof(TMappedEntity);
            cEntityTable __MappedTable = GetEntityTableByEnitityType(__MappedType);

            Type __MapType = typeof(TMapEntity);
            cEntityTable __MapTable = GetEntityTableByEnitityType(__MapType);

            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelectMapped(__MapTable.TableName, __MapTable.TableName + "." + _ColumnName + "=:" + _ColumnName, __MappedTable.TableName, __MappedTable.TableName + ".ID=" + __MapTable.TableName + "." + __MappedTable.TableForeing_ColumnName_For_InOtherTable);

            __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelectCount(__Sql, __CountAlias);
            __Sql.SetParameter(_ColumnName, _Value);
            DataTable __DataTable = Database.DefaultConnection.Query(__Sql);
            return Convert.ToInt32(__DataTable.Rows[0][__CountAlias].ToString());
        }

        public int DeleteEntityByColumnValue<TEntity>(string _ColumnName, object _Value) where TEntity : cBaseEntity
        {
            List<TEntity> __Result = new List<TEntity>();
            Type __Type = typeof(TEntity);
            cEntityTable __Table = GetEntityTableByEnitityType(__Type);
            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLDeleteByCondition(__Table.TableName, _ColumnName + "=:" + _ColumnName);
            __Sql.SetParameter(_ColumnName, _Value);
            return Database.DefaultConnection.Execute(__Sql);
        }

        public int DeleteEntityByDoubleColumnValue<TEntity>(string _ColumnName, object _Value, string _ColumnName2, object _Value2)
            where TEntity : cBaseEntity
        {
            List<TEntity> __Result = new List<TEntity>();
            Type __Type = typeof(TEntity);
            cEntityTable __Table = GetEntityTableByEnitityType(__Type);
            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLDeleteByCondition(__Table.TableName, _ColumnName + "=:" + _ColumnName + " and " + _ColumnName2 + "=:" + _ColumnName2);
            __Sql.SetParameter(_ColumnName, _Value);
            __Sql.SetParameter(_ColumnName2, _Value2);
            return Database.DefaultConnection.Execute(__Sql);
        }

        public List<TEntity> GetEntityByColumnValue<TEntity>(string _ColumnName, object _Value) where TEntity : cBaseEntity
        {
            List<TEntity> __Result = new List<TEntity>();
            Type __Type = typeof(TEntity);
            cEntityTable __Table = GetEntityTableByEnitityType(__Type);
            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelect(__Table.TableName, _ColumnName + "=:" + _ColumnName);
            __Sql.SetParameter(_ColumnName, _Value);
            DataTable __DataTable = Database.DefaultConnection.Query(__Sql);
            foreach (DataRow __Row in __DataTable.Rows)
            {
                TEntity __Entity = (TEntity)Database.App.Factories.HookedObjectFactory.PropertyHookedObjectFactory.GetInstance(__Type);
                __Row.Fill(__Entity);
                __Entity.GetType().SetPropertyValue(__Entity, "Database", Database);
                __Entity.GetType().SetPropertyValue(__Entity, "App", Database.App);
                __Entity.GetType().SetPropertyValue(__Entity, "IsValid", true);
                __Result.Add(__Entity);
            }
            return __Result;
        }

        public object GetColumnValueInEntiyByColumnValue<TEntity>(string _GettingColumnName, string _ColumnName, object _Value) where TEntity : cBaseEntity
        {
            List<TEntity> __Result = new List<TEntity>();
            Type __Type = typeof(TEntity);
            cEntityTable __Table = GetEntityTableByEnitityType(__Type);
            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelect(__Table.TableName, _ColumnName + "=:" + _ColumnName);
            __Sql.SetParameter(_ColumnName, _Value);
            DataTable __DataTable = Database.DefaultConnection.Query(__Sql);
            if (__DataTable.Rows.Count == 1)
            {
                return __DataTable.Rows[0][_GettingColumnName];
            }
            else if (__DataTable.Rows.Count == 0)
            {
                return null;
            }
            throw new Exception("Birden fazla deger dönüyor!");
        }

        public List<TEntity> GetEntityByColumnValue<TEntity>(string _ColumnName, object _Value, int _StartRowNumber, int _EndRowNumber) where TEntity : cBaseEntity
        {
            List<TEntity> __Result = new List<TEntity>();
            Type __Type = typeof(TEntity);
            cEntityTable __Table = GetEntityTableByEnitityType(__Type);
            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelect(__Table.TableName, _ColumnName + "=:" + _ColumnName);
            __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelectRowRange(__Sql.FullSQLString);
            __Sql.SetParameter(_ColumnName, _Value);
            __Sql.SetParameter(cSqlServerRowOperationSQLCatalog.StartRowNumber, _StartRowNumber);
            __Sql.SetParameter(cSqlServerRowOperationSQLCatalog.EndRowNumber, _EndRowNumber);

            DataTable __DataTable = Database.DefaultConnection.Query(__Sql);
            foreach (DataRow __Row in __DataTable.Rows)
            {
                TEntity __Entity = (TEntity)Database.App.Factories.HookedObjectFactory.PropertyHookedObjectFactory.GetInstance(__Type);
                __Row.Fill(__Entity);
                __Entity.GetType().SetPropertyValue(__Entity, "Database", Database);
                __Entity.GetType().SetPropertyValue(__Entity, "App", Database.App);
                __Entity.GetType().SetPropertyValue(__Entity, "IsValid", true);
                __Result.Add(__Entity);
            }
            return __Result;
        }


        public List<TMappedEntity> GetEntityByColumnValueForMapped<TMapEntity, TMappedEntity>(string _ColumnName, object _Value, int _StartRowNumber, int _EndRowNumber)
            where TMapEntity : cBaseEntity
            where TMappedEntity : cBaseEntity
        {
            List<TMappedEntity> __Result = new List<TMappedEntity>();

            Type __MappedType = typeof(TMappedEntity);
            cEntityTable __MappedTable = GetEntityTableByEnitityType(__MappedType);

            Type __MapType = typeof(TMapEntity);
            cEntityTable __MapTable = GetEntityTableByEnitityType(__MapType);

            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelectMapped(__MapTable.TableName, __MapTable.TableName + "." + _ColumnName + "=:" + _ColumnName, __MappedTable.TableName, __MappedTable.TableName + ".ID=" + __MapTable.TableName + "." + __MappedTable.TableForeing_ColumnName_For_InOtherTable);

            __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelectRowRange(__Sql.FullSQLString);
            __Sql.SetParameter(_ColumnName, _Value);
            __Sql.SetParameter(cSqlServerRowOperationSQLCatalog.StartRowNumber, _StartRowNumber);
            __Sql.SetParameter(cSqlServerRowOperationSQLCatalog.EndRowNumber, _EndRowNumber);

            DataTable __DataTable = Database.DefaultConnection.Query(__Sql);
            foreach (DataRow __Row in __DataTable.Rows)
            {
                TMappedEntity __Entity = (TMappedEntity)Database.App.Factories.HookedObjectFactory.PropertyHookedObjectFactory.GetInstance(__MappedType);
                __Row.Fill(__Entity);
                __Entity.GetType().SetPropertyValue(__Entity, "Database", Database);
                __Entity.GetType().SetPropertyValue(__Entity, "App", Database.App);
                __Entity.GetType().SetPropertyValue(__Entity, "IsValid", true);
                __Result.Add(__Entity);
            }
            return __Result;
        }

		public List<dynamic> GetEntityByColumnValueForMappedToDynamicObjectList<TMapEntity, TMappedEntity>(Action<dynamic> _Action, string _ColumnName, object _Value)
			where TMapEntity : cBaseEntity
			where TMappedEntity : cBaseEntity
		{

			Type __MappedType = typeof(TMappedEntity);
			cEntityTable __MappedTable = GetEntityTableByEnitityType(__MappedType);

			Type __MapType = typeof(TMapEntity);
			cEntityTable __MapTable = GetEntityTableByEnitityType(__MapType);

			cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelectMapped(__MapTable.TableName, __MapTable.TableName + "." + _ColumnName + "=:" + _ColumnName, __MappedTable.TableName, __MappedTable.TableName + ".ID=" + __MapTable.TableName + "." + __MappedTable.TableForeing_ColumnName_For_InOtherTable);
			__Sql.SetParameter(_ColumnName, _Value);

			DataTable __DataTable = Database.DefaultConnection.Query(__Sql);


			List<object> __Result = new List<object>();
			foreach (DataRow __Row in __DataTable.Rows)
			{

				ExpandoObject __Dynamic = new ExpandoObject();
				IDictionary<string, object> __UnderlyingObject = __Dynamic;

				for (int i = 0; i < __DataTable.Columns.Count; i++)
				{
					__UnderlyingObject.Add(__DataTable.Columns[i].ColumnName, __Row[__DataTable.Columns[i].ColumnName]);
				}

				__Result.Add(__Dynamic);
				_Action?.Invoke(__Dynamic);
			}
			return __Result;
		}


		public List<TMappedEntity> GetEntityByColumnValueForMapped<TMapEntity, TMappedEntity>(string _ColumnName, object _Value)
           where TMapEntity : cBaseEntity
           where TMappedEntity : cBaseEntity
        {
            List<TMappedEntity> __Result = new List<TMappedEntity>();

            Type __MappedType = typeof(TMappedEntity);
            cEntityTable __MappedTable = GetEntityTableByEnitityType(__MappedType);

            Type __MapType = typeof(TMapEntity);
            cEntityTable __MapTable = GetEntityTableByEnitityType(__MapType);

            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelectMapped(__MapTable.TableName, __MapTable.TableName + "." + _ColumnName + "=:" + _ColumnName, __MappedTable.TableName, __MappedTable.TableName + ".ID=" + __MapTable.TableName + "." + __MappedTable.TableForeing_ColumnName_For_InOtherTable);
            __Sql.SetParameter(_ColumnName, _Value);

            DataTable __DataTable = Database.DefaultConnection.Query(__Sql);
            foreach (DataRow __Row in __DataTable.Rows)
            {
                TMappedEntity __Entity = (TMappedEntity)Database.App.Factories.HookedObjectFactory.PropertyHookedObjectFactory.GetInstance(__MappedType);
                __Row.Fill(__Entity);
                __Entity.GetType().SetPropertyValue(__Entity, "Database", Database);
                __Entity.GetType().SetPropertyValue(__Entity, "App", Database.App);
                __Entity.GetType().SetPropertyValue(__Entity, "IsValid", true);
                __Result.Add(__Entity);
            }
            return __Result;
        }


        public List<TOutEntity> GetEntityByColumnValue<TOutEntity, TForeignKEyEntityColumn>(TForeignKEyEntityColumn _Column, object _Value)
            where TOutEntity : cBaseEntity
            where TForeignKEyEntityColumn : cBaseEntity
        {
            cEntityTable __ColumnTable = GetEntityTableByEnitityType(_Column.GetType());
            return GetEntityByColumnValue<TOutEntity>(__ColumnTable.TableForeing_ColumnName_For_InOtherTable, _Value);
        }

        public TEntity GetEntityByID<TEntity>(long _ID) where TEntity : cBaseEntity
        {
            List<TEntity> __Result = GetEntityByColumnValue<TEntity>(cEntityColumn.ID_ColumnName, _ID);
            return __Result.FirstOrDefault();
        }

        public object GetEntityByID_Or_CreateNew(Type _Type, long _ID)
        {
            if (Database.GetEntityType().IsAssignableFrom(_Type))
            {
                Type __Type = GetType();
                MethodInfo __MethodInfo = __Type.GetMethod("GetEntityByID_Or_CreateNew", new Type[] { typeof(int) });
                __MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _Type });
                object __Temp = (object)__MethodInfo.Invoke(this, new object[] { _ID });
                return __Temp;
            }
            else
            {
                throw new Exception("cEntityManager->GetEntityByColumnValue");
            }
        }

        public TEntity GetEntityByID_Or_CreateNew<TEntity>(long _ID = -1) where TEntity : cBaseEntity
        {
            TEntity __Entity = GetEntityByID<TEntity>(_ID);
            if (__Entity == null)
            {
                Type __Type = typeof(TEntity);
                __Entity = (TEntity)Database.App.Factories.HookedObjectFactory.PropertyHookedObjectFactory.GetInstance(__Type);
                __Entity.GetType().SetPropertyValue(__Entity, "Database", Database);
                __Entity.GetType().SetPropertyValue(__Entity, "App", Database.App);
                __Entity.GetType().SetPropertyValue(__Entity, "IsValid", false);
                __Entity.SetDefaultValue();
                __Entity.ID = _ID;
            }
            return __Entity;
        }

        public TEntity CreateNew<TEntity>() where TEntity : cBaseEntity
        {
            Type __Type = typeof(TEntity);
            TEntity __Entity = (TEntity)Database.App.Factories.HookedObjectFactory.PropertyHookedObjectFactory.GetInstance(__Type);
            __Entity.GetType().SetPropertyValue(__Entity, "Database", Database);
            __Entity.GetType().SetPropertyValue(__Entity, "App", Database.App);
            __Entity.GetType().SetPropertyValue(__Entity, "IsValid", false);
            __Entity.SetDefaultValue();
            return __Entity;
        }

        public void ReloadReferencedEntity()
        {
            foreach (cEntityTable __EntityTable in EntityTableList)
            {
                __EntityTable.ReferencedEntity();
            }
        }

        public cEntityTable GetEntityTableByEnitityType<TEntity>() where TEntity : cBaseEntity
        {
            return GetEntityTableByEnitityType(typeof(TEntity));
        }


        public cEntityTable GetEntityTableByEnitityType(Type _EntityType)
        {
            foreach (cEntityTable __EntityTable in EntityTableList)
            {
                if (__EntityTable.EntityType == _EntityType)
                {
                    return __EntityTable;
                }
            }
            throw new Exception("cEntityManager->GetEntityTableByName()");
        }
    }
}