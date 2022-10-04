using Bootstrapper.Boundary.nCore.nBootType;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nAttributes;
using Toygar.DB.Data.nConfiguration;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nEntityServices.nEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nDataManager;

namespace Toygar.DB.Data.nDataServiceManager.nGlobalDataServices
{
    [Register(typeof(IGlobalDataService), false, false, false, false, LifeTime.PerResolveLifetimeManager)]
    public class cGlobalDataService : cBaseDataService<cGlobalDataServiceContext, cBaseGlobalEntity>, IGlobalDataService
    {
        public cGlobalDataService(cGlobalDataServiceContext _CoreServiceContext)
            : base(_CoreServiceContext)
        {
            LoadDB(ServiceContext.Configuration.DBVendor, ServiceContext.Configuration.GlobalDBServer, ServiceContext.Configuration.GlobalDBUserName, ServiceContext.Configuration.GlobalDBPassword, ServiceContext.Configuration.GlobalDBName, ServiceContext.Configuration.MaxConnectCount);
        }

        /*public string GetHostName()
        {
            string __HostName = "";
            if (App.Cfg<cDataConfiguration>().BootType == EBootType.Console
              || App.Cfg<cDataConfiguration>().BootType == EBootType.Batch)
            {
                __HostName = App.Cfg<cDataConfiguration>().TargetHostName;
            }
            else
            {
                __HostName = App.Handlers.ContextHandler.CurrentContextItem.Context.Request.Host.Host;
                __HostName = App.Handlers.StringHandler.GetRootDomain(__HostName);
            }
            return __HostName;
        }*/

        public void LockPofile<TServiceBaseEntity>(string _HostName, Action _ServiceMethod)
             where TServiceBaseEntity : cBaseEntity
        {
            cProfileDataManager __ProfileDataManager = App.Factories.ObjectFactory.ResolveInstance<cProfileDataManager>();

            cProfileEntity __Profile = __ProfileDataManager.GetProfileByEntityTypeAndHostName<TServiceBaseEntity>(_HostName);

            App.Loggers.SqlLogger.LogInfo("Profile Lock Begin (Locked Profile)");

            this.Perform(() =>
            {
                __Profile.LockAndRefresh();
                _ServiceMethod.Invoke();
            });

            App.Loggers.SqlLogger.LogInfo("Profile Lock End (Unlocked Profile)");
        }

        public bool IsProfileLocked<TServiceBaseEntity>(string _HostName)
            where TServiceBaseEntity : cBaseEntity
        {
            /*string __HostName = "";
            if (App.Cfg<cDataConfiguration>().BootType == EBootType.Console
              || App.Cfg<cDataConfiguration>().BootType == EBootType.Batch)
            {
                __HostName = App.Cfg<cDataConfiguration>().TargetHostName;
            }
            else
            {
                __HostName = App.Handlers.ContextHandler.CurrentContextItem.Context.Request.Host.Host;
                __HostName = App.Handlers.StringHandler.GetRootDomain(__HostName);
            }*/

            cProfileDataManager __ProfileDataManager = App.Factories.ObjectFactory.ResolveInstance<cProfileDataManager>();
            cProfileEntity __Profile = __ProfileDataManager.GetProfileByEntityTypeAndHostName<TServiceBaseEntity>(_HostName);
            return __Profile.IsLocked();
        }

        public void LockPofileByHostName<TServiceBaseEntity>(string _HostName, Action _ServiceMethod)
            where TServiceBaseEntity : cBaseEntity
        {
            cProfileDataManager __ProfileDataManager = App.Factories.ObjectFactory.ResolveInstance<cProfileDataManager>();

            cProfileEntity __Profile = __ProfileDataManager.GetProfileByEntityTypeAndHostName<TServiceBaseEntity>(_HostName);

            App.Loggers.SqlLogger.LogInfo("Profile Lock Begin (Locked Profile)");

            this.Perform(() =>
            {
                __Profile.LockAndRefresh();
                _ServiceMethod.Invoke();
            });

            App.Loggers.SqlLogger.LogInfo("Profile Lock End (Unlocked Profile)");
        }

        public bool IsProfileLockedByHost<TServiceBaseEntity>(string _HostName)
            where TServiceBaseEntity : cBaseEntity
        {
            cProfileDataManager __ProfileDataManager = App.Factories.ObjectFactory.ResolveInstance<cProfileDataManager>();
            cProfileEntity __Profile = __ProfileDataManager.GetProfileByEntityTypeAndHostName<TServiceBaseEntity>(_HostName);
            return __Profile.IsLocked();
        }

        public override void InvokeTransactionalAction(Func<bool> _ServiceMethod)
        {
            try
            {
                List<MethodBase> __Methods = App.Handlers.StackHandler.GetMethods("InvokeTransactionalAction", 0);

                if (__Methods.Where(__Item => __Item.DeclaringType.Name == this.GetType().Name).ToList().Count < 2)
                {
                    if (_ServiceMethod())
                    {
                        Database.DefaultConnection.Commit();
                    }
                    else
                    {
                        Database.DefaultConnection.Rollback();
                    }
                }
                else
                {
                    throw new Exception("ic ice aclilan transation mevcut..!");
                }
            }
            catch (Exception ex)
            {
                App.Loggers.SqlLogger.LogError(ex);
                Database.DefaultConnection.Rollback();
                throw;
            }
        }

    }
}
