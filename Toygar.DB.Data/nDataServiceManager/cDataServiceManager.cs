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
using Toygar.DB.Data.nDataFileEntity;
using SharpRaven.Data.Context;
using System.Data;

namespace Toygar.DB.Data.nDataServiceManager
{
    [ToygarRegister(typeof(IDataServiceManager), true, true, true, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDataServiceManager : IDataServiceManager
    {
        Dictionary<string, IDataService> DataServices { get; set; }
        IGlobalDataService GlobalDataService { get; set; }
        IGlobalDefaultDataLoader GlobalDefaultDataLoader { get; set; }

        public cApp App { get; set; }

        public cDataServiceManager(cApp _App)
        {
            App = _App;
            DataServices = new Dictionary<string, IDataService>();
        }

        public string GetDataHost()
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


		public IDataService GetDataService()
        {
            //Burda konteksten IDataService Servis bulundu
            string __HostName = GetDataHost();



            if (DataServices.ContainsKey(__HostName))
            {
                return DataServices[__HostName];
            }
            else
            {
                lock (string.Intern(__HostName))
                {
                    if (DataServices.ContainsKey(__HostName))
                    {
                        return DataServices[__HostName];
                    }
                    else
                    {
                        lock (string.Intern(__HostName))
                        {
                            if (DataServices.ContainsKey(__HostName))
                            {
                                return DataServices[__HostName];
                            }
                            else
                            {
                                cDataConfiguration __Cfg = App.Cfg<cDataConfiguration>();

                                IGlobalDataService __GlobalDataService = GetGlobalDataService();

                                cDBConnectionSettingEntity __DBConnectionSettingEntity = GlobalDefaultDataLoader.GetConnectionSettingByHostName(__HostName);
                                if (__DBConnectionSettingEntity != null)
                                {
                                    Type __Type = typeof(cDataService<>).MakeGenericType(App.Handlers.AssemblyHandler.FindFirstType(__DBConnectionSettingEntity.EntityType));


                                    IDataService __DataService = (IDataService)App.Factories.ObjectFactory.ResolveInstance(__Type);

                                    //IDataService __DataService = App.Factories.ObjectFactory.ResolveInstance<IDataService>();
                                    __DataService.LoadDB(__Cfg.DBVendor, __DBConnectionSettingEntity.Server, __DBConnectionSettingEntity.UserName, __DBConnectionSettingEntity.Password, __DBConnectionSettingEntity.GlobalDBName, __DBConnectionSettingEntity.MaxConnectCount);
                                    try
                                    {
                                        DataServices.Add(__HostName, __DataService);
                                    }
                                    catch (Exception _Ex)
                                    {
                                        App.Loggers.CoreLogger.LogError(_Ex);
                                        return DataServices[__HostName];
                                    }

                                    try
                                    {
                                        IComponentLoader __ComponentLoader = App.Factories.ObjectFactory.ResolveInstance<IComponentLoader>();
                                        if (__ComponentLoader != null) __ComponentLoader.Load(DataServices[__HostName]);
                                    }
                                    catch (Exception _Ex)
                                    {
                                        App.Loggers.CoreLogger.LogError(_Ex);
                                    }

                                    if (__Cfg.LoadDefaultDataOnStart)
                                    {
                                        try
                                        {
                                            IDefaultDataLoader __DefaultDataLoader = App.Factories.ObjectFactory.ResolveInstance<IDefaultDataLoader>();
                                            if (__DefaultDataLoader != null) __DefaultDataLoader.Load(DataServices[__HostName]);
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
                }
            }

            return null;
        }

        public IGlobalDataService GetGlobalDataService()
        {
            if (GlobalDataService == null)
            {
                GlobalDataService = App.Factories.ObjectFactory.ResolveInstance<IGlobalDataService>();
                GlobalDefaultDataLoader = App.Factories.ObjectFactory.ResolveInstance<IGlobalDefaultDataLoader>();
                GlobalDefaultDataLoader.Load(GlobalDataService);
            }

            return GlobalDataService;
        }
    }
}
