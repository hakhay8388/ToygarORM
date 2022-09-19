using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.DB.Data.nDataService.nDatabase;
using Toygar.DB.Data.nDataService.nDatabase.nConnection;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nIDController;
using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nDataService.nDatabase.nDifference;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata;
using System.Collections;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using Toygar.DB.Data.nDataService.nDatabase.nQuery;
using System.Linq.Expressions;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryDemonstratorInterfaces;
using Toygar.DB.Data.nDataService.nDatabase.nDBInfo;
using System.Reflection;
using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataUtils;

namespace Toygar.DB.Data.nDataService.nDatabase
{
    public abstract class cBaseDatabase<TBaseEntity> : cCoreService<cDatabaseContext>, IDatabase
        where TBaseEntity : cBaseEntity
    {
        public IDataService DataService { get; set; }
        public cConnectionPoolingManager ConnectionPoolingManager { get; set; }
        public cConnectionPoolingManager CustomConnectionPoolingManager { get; set; }
        public cBaseCatalogs Catalogs { get; set; }
        public cIDController IDController { get; set; }
        public cDBInfo DBInfo { get; set; }

        private cMetadataManager m_MetadataManager { get; set; }
        private cEntityManager m_EntityManager { get; set; }
        private cDifferenceManager m_DifferenceManager { get; set; }

        public bool IsGlobalConnection { get; private set; }


        public cBaseDatabase(cDatabaseContext _DatabaseContext, bool _IsGlobalConnection)
            : base(_DatabaseContext)
        {
            ConnectionPoolingManager = new cConnectionPoolingManager(this, GetConnectionType());
            CustomConnectionPoolingManager = new cConnectionPoolingManager(this, GetConnectionType());
            Catalogs = GetCatalogs();
            IsGlobalConnection = _IsGlobalConnection;
            if (!IsGlobalConnection)
            {
                IDController = new cIDController(this);
                DBInfo = new cDBInfo(this);
            }
        }

        public void LoadVersion()
        {
            DBInfo.LoadVersion();
        }

        protected abstract Type GetConnectionType();
        protected abstract cBaseCatalogs GetCatalogs();

        public cBaseConnection DefaultConnection
        {
            get
            {
                return ConnectionPoolingManager.DefaultConnection;
            }
        }

        public cDifferenceManager DifferenceManager
        {
            get
            {
                if (m_DifferenceManager == null)
                {
                    m_DifferenceManager = new cDifferenceManager(this);
                }
                return m_DifferenceManager;
            }
        }

        public cEntityManager EntityManager
        {
            get
            {
                if (m_EntityManager == null)
                {
                    m_EntityManager = new cEntityManager(this);
                }
                return m_EntityManager;
            }
        }

        public cMetadataManager MetadataManager
        {
            get
            {
                if (m_MetadataManager == null)
                {
                    m_MetadataManager = new cMetadataManager(this);
                }
                return m_MetadataManager;
            }
        }

        public Type GetEntityType()
        {
            return typeof(TBaseEntity);
        }

        
        public IList GetEntityListByColumnValue(Type _Type, string _ColumnName, object _Value)
        {
            return EntityManager.GetEntityByColumnValue(_Type, _ColumnName, _Value);
        }

        public List<TEntity> GetEntityByColumnValue<TEntity>(string _ColumnName, object _Value) where TEntity : cBaseEntity
        {
            return EntityManager.GetEntityByColumnValue<TEntity>(_ColumnName, _Value);
        }

        public TEntity GetEntityByID<TEntity>(long _ID) where TEntity : cBaseEntity
        {
            List<TEntity> __Result = GetEntityByColumnValue<TEntity>(cEntityColumn.ID_ColumnName, _ID);
            return __Result.FirstOrDefault();
        }

        public object GetEntityByID(Type _EntityType, long _ID)
        {
            Type __GenericType = this.GetType();
            MethodInfo __Info = __GenericType.GetMethod("GetEntityByID", 1, new Type[] { typeof(long) });
            __Info = __Info.MakeGenericMethod(new Type[] { _EntityType });
            object __Result = __Info.Invoke(this, new object[] { _ID });
            return __Result;
        }

        public TEntity GetEntityByID_Or_CreateNew<TEntity>(long _ID) where TEntity : cBaseEntity
        {
            return EntityManager.GetEntityByID_Or_CreateNew<TEntity>(_ID);
        }

        public TEntity CreateNew<TEntity>() where TEntity : cBaseEntity
        {
            return EntityManager.GetEntityByID_Or_CreateNew<TEntity>(-1);
        }

        public List<TOutEntity> GetEntityByColumnValue<TOutEntity, TForeignKeyEntityColumn>(TForeignKeyEntityColumn _Column, object _Value)
            where TOutEntity : cBaseEntity
            where TForeignKeyEntityColumn : cBaseEntity
        {
            return EntityManager.GetEntityByColumnValue<TOutEntity, TForeignKeyEntityColumn>(_Column, _Value);
        }

        public ISelectionDemonstrator<TEntity> Query<TEntity>() where TEntity : cBaseEntity
        {
            return new cQuery<TEntity>(this, EQueryType.Select);
        }

        public IBaseFilterForOperands<TEntity, TEntity> Delete<TEntity>() where TEntity : cBaseEntity
        {
            cQuery<TEntity> __Query = new cQuery<TEntity>(this, EQueryType.Delete);
            return __Query.Where();
        }

        public ISelectionDemonstrator<TEntity> Query<TEntity>(IQuery _Query) where TEntity : cBaseEntity
        {
            return new cQuery<TEntity>(this, EQueryType.Select, _Query);
        }

        /*public ISelectionDemonstrator<TEntity> Query<TEntity>(IQuery _Query, Expression<Func<TEntity>> _SubQueryExternalAlias) where TEntity : cBaseEntity
        {
            return new cQuery<TEntity>(this, _Query, _SubQueryExternalAlias);
        }*/

        public ISelectionDemonstrator<TEntity> Query<TEntity>(Expression<Func<TEntity>> _Alias) where TEntity : cBaseEntity
        {
            return new cQuery<TEntity>(this, EQueryType.Select, _Alias);
        }

        public ISelectionDemonstrator<TEntity> Query<TEntity>(Expression<Func<TEntity>> _Alias, IQuery _Query) where TEntity : cBaseEntity
        {
            return new cQuery<TEntity>(this, EQueryType.Select, _Alias, _Query);
        }

        /*public ISelectionDemonstrator<TEntity> Query<TEntity>(Expression<Func<TEntity>> _Alias, IQuery _Query, Expression<Func<TEntity>> _SubQueryExternalAlias) where TEntity : cBaseEntity
        {
            return new cQuery<TEntity>(this, _Alias, _Query, _SubQueryExternalAlias);
        }*/

        public bool ControlDBConnection()
        {
            try
            {
                cBaseConnection __Connection=  CustomConnectionPoolingManager.DefaultConnection;
                __Connection.Release();
                CustomConnectionPoolingManager.RemoveConnection(__Connection);
                return true;
            }
            catch (Exception _Ex)
            {
				App.Loggers.SqlLogger.LogError(_Ex);
				return false;
            }
        }

        public ISelectionDemonstrator<TEntity> Query<TEntity>(cBaseHardCodedValues _HardCodedValues, Expression<Func<TEntity>> _SubQueryExternalAlias) where TEntity : cBaseEntity
        {
            return new cQuery<TEntity>(this, EQueryType.Select, _HardCodedValues, _SubQueryExternalAlias);
        }
    }
}
