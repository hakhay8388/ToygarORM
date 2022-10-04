using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nFactories.nHookedObjectFactory.nPropertyHookedObjectFactory;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataService.nDatabase;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toygar.DB.Data.nDataService.nDatabase.nDBInfo;
using Toygar.DB.Data.nConfiguration;
using System.Dynamic;
using System.ComponentModel;
using Toygar.Boundary.nData;

namespace Toygar.DB.Data.nDataService.nDatabase.nEntity
{
    public class cBaseEntity : cPropertyHookController
    {
        IDatabase m_Database { get; set; }

        protected IDatabase Database
        {
            get
            {
                return m_Database;
            }
            set
            {
                m_Database = value;
            }
        }

        protected cEntityTable m_EntityTable { get; set; }
        protected cEntityTable EntityTable { get { if (m_EntityTable == null) m_EntityTable = Database.EntityManager.GetEntityTableByEnitityType(this.GetType()); return m_EntityTable; } }
        protected List<cEntityTable> m_ReferencedEntityList { get; set; }
        protected List<cEntityTable> ReferencedEntityList { get { if (m_ReferencedEntityList == null) { m_ReferencedEntityList = EntityTable.GetThisTableReferencedBy(); } return m_ReferencedEntityList; } }
        public string TableName { get { return EntityTable.TableName; } }
        public bool IsValid { get; private set; }

        [TDBField(_PrimaryKey: true, _KeyOrderNo: 1, _Nullable: false, _DataType: EDataType.Bigint, _DefaultValue: -1)]
        public virtual long ID { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime CreateDate { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime UpdateDate { get; set; }

        public cBaseEntity()
            : base()
        {
        }


		public dynamic ToDynamic()
		{
			IDictionary<string, object> __Expando = new ExpandoObject();
			PropertyInfo[] __Properties = this.GetType().GetAllProperties();
			foreach (PropertyInfo __PropertyInfo in __Properties)
			{
				bool? __IsVirtual = __PropertyInfo.IsVirtual();
				if (!typeof(cBaseEntity).IsAssignableFrom(__PropertyInfo.PropertyType)
					&& !typeof(IMappedEntity).IsAssignableFrom(__PropertyInfo.PropertyType)
					&& !typeof(IEntityList).IsAssignableFrom(__PropertyInfo.PropertyType)
					&& __IsVirtual != null && __IsVirtual.Value)
				{
					__Expando.Add(__PropertyInfo.Name, __PropertyInfo.GetValue(this));
				}				
			}
			return __Expando as ExpandoObject;
		}

		public void SetInner(Action<cBaseEntity> _Action)
        {
            _Action(this);
        }

        private bool ControlTypeList(params cBaseEntity[] _ForeignKeyObject)
        {
            if (_ForeignKeyObject.Length == ReferencedEntityList.Count)
            {
                foreach (cEntityTable __EntityType in ReferencedEntityList)
                {
                    if (_ForeignKeyObject.Where(__Item => __Item != null && __Item.GetType() == __EntityType.EntityType).ToList().Count != 1)
                    {
                        throw new Exception("cBaseEntity->ControlTypeList->TableName=" + __EntityType.TableName + "->ColumnName=" + __EntityType.TableForeing_ColumnName_For_InOtherTable);
                    }
                }
            }
            else
            {
                throw new Exception("cBaseEntity->ControlTypeList");
            }
            return true;
        }


        public void Lock()
        {
            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLUpdateSingleRow(TableName, "ID=ID");
            __Sql.SetParameter(cEntityColumn.ID_ColumnName, ID);
            Database.DefaultConnection.Execute(__Sql);
        }

        public bool IsLocked()
        {
            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelectLockedRows(TableName, "ID=:ID");
            __Sql.SetParameter(cEntityColumn.ID_ColumnName, ID);
            return Database.DefaultConnection.Query(__Sql).Rows.Count > 0;
        }

        public void Refresh()
        {
            Type __Type = this.GetType();
            if (__Type.FullName.Contains("__Proxy__"))
            {
                __Type = __Type.BaseType;
            }

            object __Entity = Database.GetEntityByID(__Type, ID);

            List<PropertyInfo> __PropertList = __Type.GetAllProperties().ToList();
            foreach (PropertyInfo __PropertyInfo in __PropertList)
            {
                bool? __IsVirtual = __PropertyInfo.IsVirtual();
                if (__IsVirtual != null && __IsVirtual.Value && (__PropertyInfo.PropertyType.IsPrimitiveWithString() || __PropertyInfo.PropertyType.IsAssignableFrom(typeof(DateTime))))
                {
                    TDBField __DBField = __PropertyInfo.GetCustomAttribute<TDBField>();
                    if (__DBField != null)
                    {
                        if (__Entity != null)
                        {
                            object __TempObject = __PropertyInfo.GetValue(__Entity);
                            __Type.SetPropertyValue(this, __PropertyInfo.Name, __TempObject);
                        }
                        else
                        {
                            if (__PropertyInfo.Name == cEntityColumn.ID_ColumnName)
                            {
                                __Type.SetPropertyValue(this, __PropertyInfo.Name, -1);
                                this.IsValid = false;
                            }
                            else
                            {
                                try
                                {
                                    __Type.SetPropertyValue(this, __PropertyInfo.Name, null);
                                }
                                catch (Exception _Ex)
                                {
                                }
                            }

                        }
                    }
                }
            }
        }

        public void LockAndRefresh()
        {
            Lock();
            Refresh();
        }

        public void Save(params cBaseEntity[] _ForeignKeyObject)
        {
            List<PropertyInfo> __PropertList = this.GetType().GetProperties().ToList();
            cSql __Sql = null;
            if (!IsValid)
            {
                CreateDate = DateTime.Now;
                UpdateDate = DateTime.Now;
                long __ID = 1;
                if (!typeof(cDBInfoEntity).IsAssignableFrom(this.GetType()))
                {
                    if (ID == -1)
                    {
                        __ID = Database.IDController.GetNewEntityID(this.GetType());
                    }
                    else
                    {
                        __ID = Database.IDController.GetNewEntityID(this.GetType(), ID);
                    }
                }

                string __Columns = "";
                string __Values = "";
                foreach (PropertyInfo __PropertyInfo in __PropertList)
                {
                    bool? __IsVirtual = __PropertyInfo.IsVirtual();
                    if (__IsVirtual != null && __IsVirtual.Value && (__PropertyInfo.PropertyType.IsPrimitiveWithString() || __PropertyInfo.PropertyType.IsAssignableFrom(typeof(DateTime))))
                    {
                        if (__PropertyInfo.GetCustomAttribute<TDBField>() != null)
                        {
                            __Columns += __Columns.IsNullOrEmpty() ? __PropertyInfo.Name : ", " + __PropertyInfo.Name;
                            __Values += __Values.IsNullOrEmpty() ? ":" + __PropertyInfo.Name : ", :" + __PropertyInfo.Name;
                        }
                    }

                    if (__IsVirtual != null && __IsVirtual.Value && this.GetType().IsAssignableFrom(__PropertyInfo.PropertyType))
                    {
                        if (__PropertyInfo.GetCustomAttribute<TDBField>() == null)
                        {
                            object __TempObject = __PropertyInfo.GetGetMethod().Invoke(this, new object[] { });
                            if (__TempObject != null)
                            {
                                __Columns += __Columns.IsNullOrEmpty() ? __PropertyInfo.Name : ", " + __PropertyInfo.Name;
                                __Values += __Values.IsNullOrEmpty() ? ":" + __PropertyInfo.Name : ", :" + __PropertyInfo.Name;
                            }
                        }
                    }
                }

                if (ControlTypeList(_ForeignKeyObject))
                {
                    foreach (cEntityTable __EntityType in ReferencedEntityList)
                    {
                        __Columns += __Columns.IsNullOrEmpty() ? __EntityType.TableForeing_ColumnName_For_InOtherTable : ", " + __EntityType.TableForeing_ColumnName_For_InOtherTable;
                        __Values += __Values.IsNullOrEmpty() ? ":" + __EntityType.TableForeing_ColumnName_For_InOtherTable : ", :" + __EntityType.TableForeing_ColumnName_For_InOtherTable;
                    }
                }

                __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLInsertSingleRow(TableName, __Columns, __Values);
                ID = __ID;
            }
            else
            {
                UpdateDate = DateTime.Now;
                string __Values = "";
                foreach (PropertyInfo __PropertyInfo in __PropertList)
                {
                    bool? __IsVirtual = __PropertyInfo.IsVirtual();
                    if (__IsVirtual != null && __IsVirtual.Value && (__PropertyInfo.PropertyType.IsPrimitiveWithString() || __PropertyInfo.PropertyType.IsAssignableFrom(typeof(DateTime))))
                    {
                        if (__PropertyInfo.GetCustomAttribute<TDBField>() != null)
                        {
                            __Values += __Values.IsNullOrEmpty() ? __PropertyInfo.Name + "=:" + __PropertyInfo.Name : ", " + __PropertyInfo.Name + "=:" + __PropertyInfo.Name;
                        }
                    }

                    if (__IsVirtual != null && __IsVirtual.Value && this.GetType().IsAssignableFrom(__PropertyInfo.PropertyType))
                    {
                        if (__PropertyInfo.GetCustomAttribute<TDBField>() == null)
                        {
                            object __TempObject = __PropertyInfo.GetGetMethod().Invoke(this, new object[] { });
                            if (__TempObject != null)
                            {
                                __Values += __Values.IsNullOrEmpty() ? __PropertyInfo.Name + "=:" + __PropertyInfo.Name : ", " + __PropertyInfo.Name + "=:" + __PropertyInfo.Name;
                            }
                        }
                    }

                }
                /*bagli degişkenleri guncellenmiyordu Muhammed Adres Detayı Guncellerken*/
                foreach (cBaseEntity __Entity in _ForeignKeyObject)
                {
                    string __TableForeing_ColumnName_For_InOtherTable = Database.EntityManager.GetEntityTableByEnitityType(__Entity.GetType()).TableForeing_ColumnName_For_InOtherTable;
                    __Values += __Values.IsNullOrEmpty() ? __TableForeing_ColumnName_For_InOtherTable + "=:" + __TableForeing_ColumnName_For_InOtherTable : ", " + __TableForeing_ColumnName_For_InOtherTable + "=:" + __TableForeing_ColumnName_For_InOtherTable;
                }
                /*bagli degişkenleri guncellenmiyordu Muhammed Adres Detayı Guncellerken*/
                __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLUpdateSingleRow(TableName, __Values);

            }

            foreach (PropertyInfo __PropertyInfo in __PropertList)
            {
                bool? __IsVirtual = __PropertyInfo.IsVirtual();
                if (__IsVirtual != null && __IsVirtual.Value && (__PropertyInfo.PropertyType.IsPrimitiveWithString() || __PropertyInfo.PropertyType.IsAssignableFrom(typeof(DateTime))))
                {
                    if (__PropertyInfo.GetCustomAttribute<TDBField>() != null)
                    {
                        __Sql.SetParameter(__PropertyInfo.Name, __PropertyInfo.GetGetMethod().Invoke(this, new object[] { }));
                    }
                }
                if (__IsVirtual != null && __IsVirtual.Value && this.GetType().IsAssignableFrom(__PropertyInfo.PropertyType))
                {
                    if (__PropertyInfo.GetCustomAttribute<TDBField>() == null)
                    {
                        object __TempObject = __PropertyInfo.GetGetMethod().Invoke(this, new object[] { });
                        if (__TempObject != null)
                        {
                            __Sql.SetParameter(__PropertyInfo.Name, ((cBaseEntity)__TempObject).ID);
                        }
                    }
                }
            }

            foreach (cBaseEntity __Entity in _ForeignKeyObject)
            {
                __Sql.SetParameter(Database.EntityManager.GetEntityTableByEnitityType(__Entity.GetType()).TableForeing_ColumnName_For_InOtherTable, __Entity.ID);
            }

            if (Database.DefaultConnection.Execute(__Sql) > 0)
            {
                IsValid = true;
            }
        }

        public void Delete()
        {
            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLDeleteSingleRow(TableName);
            __Sql.SetParameter(cEntityColumn.ID_ColumnName, ID);
            if (Database.DefaultConnection.Execute(__Sql) > 0)
            {
                IsValid = false;
            }
        }

        public void SetDefaultValue()
        {
            Type __Type = GetType();
            List<PropertyInfo> __PropertList = __Type.GetAllProperties().ToList();
            foreach (PropertyInfo __PropertyInfo in __PropertList)
            {
                bool? __IsVirtual = __PropertyInfo.IsVirtual();
                if (__IsVirtual != null && __IsVirtual.Value && (__PropertyInfo.PropertyType.IsPrimitiveWithString() || __PropertyInfo.PropertyType.IsAssignableFrom(typeof(DateTime))))
                {
                    TDBField __DBField = __PropertyInfo.GetCustomAttribute<TDBField>();
                    if (__DBField != null && __DBField.DefaultValue != null)
                    {
                        __Type.SetPropertyValue(this, __PropertyInfo.Name, __DBField.DefaultValue);
                    }
                }
            }
        }

        public void SetColumnValue(string _ColumnName, object _Value)
        {
            Type __Type = GetType();
            List<PropertyInfo> __PropertList = __Type.GetAllProperties().ToList().Where(__Item => __Item.Name == _ColumnName).ToList();
            foreach (PropertyInfo __PropertyInfo in __PropertList)
            {
                bool? __IsVirtual = __PropertyInfo.IsVirtual();
                if (__IsVirtual != null && __IsVirtual.Value && (__PropertyInfo.PropertyType.IsPrimitiveWithString() || __PropertyInfo.PropertyType.IsAssignableFrom(typeof(DateTime))))
                {
                    TDBField __DBField = __PropertyInfo.GetCustomAttribute<TDBField>();
                    if (__DBField != null && __DBField.DefaultValue != null)
                    {
                        __Type.SetPropertyValue(this, __PropertyInfo.Name, _Value);
                    }
                }
            }
        }



        public override void HookedFuction(object _Instance, string _PropertyName, object _PropertyInner)
        {
            Type __Type = _Instance.GetType();
            if (__Type.FullName.Contains("__Proxy__"))
            {
                __Type = __Type.BaseType;
            }

            if (_PropertyInner == null && _PropertyName != cEntityColumn.ID_ColumnName)
            {
                if (typeof(cBaseEntity).IsAssignableFrom(__Type))
                {
                    cBaseEntity __This = (cBaseEntity)_Instance;

                    if (__This.Database.GetEntityType().IsAssignableFrom(__Type))
                    {
                        long __ID = __This.ID;

                        __Type = App.Handlers.AssemblyHandler.FindFirstType(__Type.FullName);
                        PropertyInfo __Prob = __Type.GetProperty(_PropertyName);
                        MethodInfo __MethodInfo = __Prob.GetSetMethod();
                        if (!__Type.IsPrimitiveWithString() && !__Type.IsAssignableFrom(typeof(DateTime)))
                        {
                            if (typeof(cBaseEntity).IsAssignableFrom(__Prob.PropertyType))
                            {
                                if (__Type == __Prob.PropertyType)
                                {
                                    cEntityTable __EntityTable = Database.EntityManager.GetEntityTableByEnitityType(__Type);
                                    object __InnerID = Database.EntityManager.GetColumnValueInEntiyByColumnValue(__Prob.PropertyType, _PropertyName, cEntityColumn.ID_ColumnName, __ID);

                                    if (__InnerID != null)
                                    {
                                        IList __InnerTypeInstance = Database.EntityManager.GetEntityByColumnValue(__Prob.PropertyType, cEntityColumn.ID_ColumnName, __InnerID);
                                        if (__InnerTypeInstance.Count == 1)
                                        {
                                            __MethodInfo.Invoke(_Instance, new object[] { __InnerTypeInstance[0] });
                                        }
                                    }
                                }
                                else
                                {
                                    cEntityTable __EntityTable = Database.EntityManager.GetEntityTableByEnitityType(__Type);
                                    IList __InnerTypeInstance = Database.EntityManager.GetEntityByColumnValue(__Prob.PropertyType, __EntityTable.TableForeing_ColumnName_For_InOtherTable, __ID);
                                    if (__InnerTypeInstance.Count == 1)
                                    {
                                        __MethodInfo.Invoke(_Instance, new object[] { __InnerTypeInstance[0] });
                                    }
                                    else
                                    {
                                        object __TempObject = Database.EntityManager.GetEntityByID_Or_CreateNew(__Prob.PropertyType, -1);
                                        __MethodInfo.Invoke(_Instance, new object[] { __TempObject });
                                    }
                                }
                            }
                            else if (typeof(IEntityList).IsAssignableFrom(__Prob.PropertyType))
                            {
                                Type __PropertyType = __Prob.PropertyType.GetGenericArguments()[0];

                                MethodInfo __CreateEntityList_MethodInfo = __Type.GetMethod("CreateEntityList", new Type[] { typeof(IDatabase), typeof(cBaseEntity) });
                                __CreateEntityList_MethodInfo = __CreateEntityList_MethodInfo.MakeGenericMethod(new Type[] { __PropertyType });
                                object __TempObject = __CreateEntityList_MethodInfo.Invoke(__This, new object[] { Database, __This });
                                __MethodInfo.Invoke(_Instance, new object[] { __TempObject });
                            }
                            else if (typeof(IMappedEntity).IsAssignableFrom(__Prob.PropertyType))
                            {
                                Type __MapPropertyType = __Prob.PropertyType.GetGenericArguments()[0];
                                Type __MappedToPropertyType = __Prob.PropertyType.GetGenericArguments()[1];

                                MethodInfo __CreateEntityList_MethodInfo = __Type.GetMethod("CreateMappedEntity", new Type[] { typeof(IDatabase), typeof(cBaseEntity) });
                                __CreateEntityList_MethodInfo = __CreateEntityList_MethodInfo.MakeGenericMethod(new Type[] { __MapPropertyType, __MappedToPropertyType });
                                object __TempObject = __CreateEntityList_MethodInfo.Invoke(__This, new object[] { Database, __This });
                                __MethodInfo.Invoke(_Instance, new object[] { __TempObject });
                            }
                        }
                    }
                }
            }
            else if (_PropertyInner == null && _PropertyName == cEntityColumn.ID_ColumnName)
            {
                __Type.GetProperty(cEntityColumn.ID_ColumnName).GetSetMethod().Invoke(_Instance, new object[] { -1 });
            }
        }
        public TEntity GetOwnerEntity<TEntity>() where TEntity : cBaseEntity
        {
            cEntityTable __OwnerEntityTable = Database.EntityManager.GetEntityTableByEnitityType(typeof(TEntity));
            cEntityTable __ThisEntityTable = Database.EntityManager.GetEntityTableByEnitityType(GetType());

            if (__ThisEntityTable.ControlColumnByName(__OwnerEntityTable.TableForeing_ColumnName_For_InOtherTable))
            {
                object __OwnerID = Database.EntityManager.GetColumnValueInEntiyByColumnValue(GetType(), __OwnerEntityTable.TableForeing_ColumnName_For_InOtherTable, cEntityColumn.ID_ColumnName, this.ID);
                if (__OwnerID != null)
                {
                    TEntity __TempObject = (TEntity)Database.EntityManager.GetEntityByID_Or_CreateNew<TEntity>((long)__OwnerID);
                    return __TempObject;
                }
            }

            return null;
        }
        public cEntityList<TEntity> CreateEntityList<TEntity>(IDatabase _Database, cBaseEntity _OwnerEntity) where TEntity : cBaseEntity
        {
            return new cEntityList<TEntity>(_Database, _OwnerEntity);
        }

        public cMappedEntity<TMapEntity, TMappedEntity> CreateMappedEntity<TMapEntity, TMappedEntity>(IDatabase _Database, cBaseEntity _OwnerEntity)
            where TMapEntity : cBaseEntity
            where TMappedEntity : cBaseEntity
        {

            return new cMappedEntity<TMapEntity, TMappedEntity>(_Database, _OwnerEntity);
        }
    }
}
