using Toygar.Base.Core.nAttributes;
using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nConfiguration;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase.nDifference;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase;
using Toygar.Base.Boundary.nCore.nBootType;
using System.Reflection;

namespace Toygar.DB.Data.nDataService
{
	public abstract class cBaseDataService<TServiceContext, TBaseEntity> : cCoreService<TServiceContext>, IDataService
		where TServiceContext : cCoreServiceContext
		where TBaseEntity : cBaseEntity
	{

		public cDatabaseContext GlobalDatabaseContext { get; set; }
		public cDatabaseContext DatabaseContext { get; set; }
		public IDatabase Database { get; set; }

		public IDatabase DatabaseManager { get; set; }

		public cBaseDataService(TServiceContext _DataServiceContext)
			: base(_DataServiceContext)
		{
		}

        public void SetDBParams(string _Database)
        {
            Database.Catalogs.DatabaseOperationsSQLCatalog.SetDbLevelParams(_Database);
        }

        public virtual void LoadDB(EDBVendor _DBVendor, string _Server, string _UserName, string _Password, string _Database, int _MaxConnectionCount)
		{
			GlobalDatabaseContext = new cDatabaseContext(App, _DBVendor, _Server, _UserName, _Password, "", _MaxConnectionCount);
			DatabaseContext = new cDatabaseContext(App, _DBVendor, _Server, _UserName, _Password, _Database, _MaxConnectionCount);


			AssignDatabaseManager(GlobalDatabaseContext);
			if (!DatabaseManager.Catalogs.DatabaseOperationsSQLCatalog.IsDatabaseExists(_Database))
			{
				DatabaseManager.Catalogs.DatabaseOperationsSQLCatalog.CreateDatabase(_Database);
			}
			AssignDatabase(DatabaseContext, _Database == GlobalDatabaseContext.Configuration.GlobalDBName);
			SetDBParams(_Database);
		}

		public virtual void LoadDB(cDatabaseContext _DatabaseContext)
		{
			LoadDB(_DatabaseContext.Configuration.DBVendor, _DatabaseContext.Configuration.GlobalDBServer, _DatabaseContext.Configuration.GlobalDBUserName, _DatabaseContext.Configuration.GlobalDBPassword, _DatabaseContext.Configuration.GlobalDBName, _DatabaseContext.Configuration.MaxConnectCount);
		}

        protected virtual void AssignDatabase(cDatabaseContext _DatabaseContext, bool _IsGlobalDB = false)
		{
			if (_DatabaseContext.DBVendor == EDBVendor.MSSQL)
			{
				Database = new cSqlServerDatabase<TBaseEntity>(_DatabaseContext, false);
				Database.DataService = this;
			}
			if (Database.ControlDBConnection() && (_IsGlobalDB || Database.DBInfo.DBVersion < _DatabaseContext.Configuration.DBVersion) &&
				(
					_DatabaseContext.Configuration.BootType == EBootType.Console
					|| App.Cfg<cDataConfiguration>().BootType == EBootType.Batch
					|| _DatabaseContext.Configuration.BootType == EBootType.Web
				)
			)
			{
				Perform<cBaseDataService<TServiceContext, TBaseEntity>, TBaseEntity>(SynchronizeDB, this);
				Database.LoadVersion();
			}
		}

		public void AssignDatabaseManager(cDatabaseContext _DatabaseContext)
		{
			if (_DatabaseContext.DBVendor == EDBVendor.MSSQL)
			{
				DatabaseManager = new cSqlServerDatabase<TBaseEntity>(_DatabaseContext, true);
			}
			if (DatabaseManager.ControlDBConnection() &&
				(
					_DatabaseContext.Configuration.BootType == EBootType.Console
					|| App.Cfg<cDataConfiguration>().BootType == EBootType.Batch
					|| _DatabaseContext.Configuration.BootType == EBootType.Web
				))
			{
			}
		}

		public TBaseEntity SynchronizeDB(cBaseDataService<TServiceContext, TBaseEntity> _DataService)
		{
			Database.DifferenceManager.CalculateDifferences();
			Database.DifferenceManager.Synchronize();
			Database.DBInfo.DBVersion = DatabaseContext.Configuration.DBVersion;
			return null;
		}

		public void Perform(Func<bool> _ServiceMethod)
		{
			Console.WriteLine("Perform Begin : " + _ServiceMethod.Method.ToString());
			InvokeTransactionalAction(_ServiceMethod);
			Console.WriteLine("Perform End : " + _ServiceMethod.Method.ToString());
		}

		public TOutput Perform<TInput, TOutput>(Func<TInput, TOutput> _ServiceMethod, TInput _Input)
		{
			Console.WriteLine("Perform Begin : " + _ServiceMethod.Method.ToString());
			TOutput __Output = default(TOutput);

			InvokeTransactionalAction(() =>
			{
				__Output = _ServiceMethod.Invoke(_Input);
				return true;
			});
			Console.WriteLine("Perform End : " + _ServiceMethod.Method.ToString());
			return __Output;
		}

		public void Perform<TInput>(Action<TInput> _ServiceMethod, TInput _Input)
		{
			Console.WriteLine("Perform Begin : " + _ServiceMethod.Method.ToString());

			InvokeTransactionalAction(() =>
			{
				_ServiceMethod.Invoke(_Input);
				return true;
			});
			Console.WriteLine("Perform End : " + _ServiceMethod.Method.ToString());
		}


		public void Perform(Action _ServiceMethod)
		{
			Console.WriteLine("Perform Begin : " + _ServiceMethod.Method.ToString());

			InvokeTransactionalAction(() =>
			{
				_ServiceMethod.Invoke();
				return true;
			});
			Console.WriteLine("Perform End : " + _ServiceMethod.Method.ToString());
		}

        public abstract void InvokeTransactionalAction(Func<bool> _ServiceMethod);

        
     
    }
}
