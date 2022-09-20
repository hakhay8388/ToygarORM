using Toygar.Base.Boundary.nCore.nBootType;
using Toygar.Base.Boundary.nCore.nObjectLifeTime;
using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nAttributes;
using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nConfiguration;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SharpRaven.Data.Context;
using System.Data;
using Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nEntityServices.nEntities;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nDataManagers;
using Toygar.DB.Data.nConfiguration.nDBItemConfig;
using Toygar.DB.Data.nDataServiceManager.nGlobalDataServices;

namespace Toygar.DB.Data.nDataServiceManager
{
    struct TDataServiceKey
    {
        public string HostName { get; set; }

        public string EntityTypeName { get; set; }

        public TDataServiceKey(string _HostName, string _EntityTypeName)
        {
            this.HostName = _HostName;
            this.EntityTypeName = _EntityTypeName;
        }
    }

    [ToygarRegister(typeof(IDataServiceManager), true, true, true, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDataServiceManager : IDataServiceManager
    {
        Dictionary<TDataServiceKey, IDataService> DataServices { get; set; }
        IGlobalDataService GlobalDataService { get; set; }
        cProfileDataManager ProfileDataManager { get; set; }

        public cApp App { get; set; }

        public cDataServiceManager(cApp _App)
        {
            App = _App;
            DataServices = new Dictionary<TDataServiceKey, IDataService>();
        }


		public List<IDataService> GetAllDataService()
		{
			List<IDataService> __Result = new List<IDataService>();
			foreach (var __Item in DataServices)
			{
				if (!typeof(IGlobalDataService).IsAssignableFrom(__Item.Value.GetType()))
				{
					__Result.Add(__Item.Value);
				}
			}
			return __Result;
		}

        public IDataService GetDataService<TServiceBaseEntity>(string _HostName)
            where TServiceBaseEntity : cBaseEntity
        {
            string __EnttiyTypeName = typeof(TServiceBaseEntity).FullName;

            lock (string.Intern(__EnttiyTypeName))
            {

                cDataConfiguration __Cfg = App.Cfg<cDataConfiguration>();

                IGlobalDataService __GlobalDataService = GetGlobalDataService();

                dynamic __ProfileEntity = ProfileDataManager.GetProfileWithDBSettingByEntityTypeAndHostName<TServiceBaseEntity>(_HostName);
                if (__ProfileEntity != null)
                {
                    TDataServiceKey __DataServiceKey = new TDataServiceKey(__ProfileEntity.HostName, __EnttiyTypeName);
                    if (DataServices.ContainsKey(__DataServiceKey))
                    {
                        return DataServices[__DataServiceKey];
                    }
                    else
                    {
                        lock (string.Intern(__DataServiceKey.HostName))
                        {
                            lock (string.Intern(__DataServiceKey.EntityTypeName))
                            {


                                Type __Type = typeof(cDataService<>).MakeGenericType(App.Handlers.AssemblyHandler.FindFirstType(__DataServiceKey.EntityTypeName));

                                IDataService __DataService = (IDataService)App.Factories.ObjectFactory.ResolveInstance(__Type);

                                __DataService.LoadDB(__Cfg.DBVendor, __ProfileEntity.Server, __ProfileEntity.UserId, __ProfileEntity.Password, __ProfileEntity.DBName, Convert.ToInt32(__ProfileEntity.MaxConnectionCount));

                                try
                                {
                                    DataServices.Add(__DataServiceKey, __DataService);
                                }
                                catch (Exception _Ex)
                                {
                                    App.Loggers.CoreLogger.LogError(_Ex);
                                    return DataServices[__DataServiceKey];
                                }

                                if (__Cfg.LoadDefaultDataOnStart)
                                {
                                    try
                                    {
                                        IDefaultDataLoader __DefaultDataLoader = App.Factories.ObjectFactory.ResolveInstance<IDefaultDataLoader>();
                                        if (__DefaultDataLoader != null) __DefaultDataLoader.Load(DataServices[__DataServiceKey]);
                                    }
                                    catch (Exception _Ex)
                                    {
                                        App.Loggers.CoreLogger.LogError(_Ex);
                                    }
                                }

                                return __DataService;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Profile Entity not exists!");
                }
            }
        }

        public IGlobalDataService GetGlobalDataService()
        {
            if (GlobalDataService == null)
            {
                GlobalDataService = App.Factories.ObjectFactory.ResolveInstance<IGlobalDataService>();
                ProfileDataManager = App.Factories.ObjectFactory.ResolveInstance<cProfileDataManager>();

                cDataConfiguration __Cfg = App.Cfg<cDataConfiguration>();
                foreach (cDBItemConfig __DBItemConfig in __Cfg.DBItemConfigs)
                {
                    cProfileEntity __ProfileEntity = null;
                    GlobalDataService.Perform(() =>
                    {
                        __ProfileEntity = ProfileDataManager.GetProfileCreateIfNotExists(__DBItemConfig.HostName, __DBItemConfig.EntityType);
                    });

                    GlobalDataService.Perform(() =>
                    {
                        cDBSettingEntity __DBSettingEntity = ProfileDataManager.GetDBSettingCreateIfNotExists(__ProfileEntity, __DBItemConfig.EntityType, __DBItemConfig.UserId, __DBItemConfig.Password, __DBItemConfig.Server, __DBItemConfig.DBName, __DBItemConfig.MaxConnectCount);
                    });
                }
            }

            return GlobalDataService;
        }
    }
}
