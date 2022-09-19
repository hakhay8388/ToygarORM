using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase.nQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataServiceManager;
using Toygar.Base.Core.nAttributes;
using Toygar.Base.Boundary.nCore.nObjectLifeTime;
using Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nEntityServices.nEntities;
using Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nDataManagers.nLoaders;
using Toygar.DB.Data.nDataFileEntity;

namespace Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nDataManagers
{
    [ToygarRegister(typeof(IGlobalDefaultDataLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDefaultDataLoaderManager : cBaseDataManager , IGlobalDefaultDataLoader
    {
        public cProfileDataManager ProfileDataManager { get; set; }
        public cAdminDataLoader AdminDataLoader { get; set; }
        public cDefaultDataLoaderManager(cGlobalDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, cAdminDataLoader _AdminDataLoader, cProfileDataManager _ProfileDataManager)
          : base(_CoreServiceContext, _DataServiceManager)
        {
            AdminDataLoader = _AdminDataLoader;
            ProfileDataManager = _ProfileDataManager;
        }

        public void Load(IDataService _DataService)
        {
            _DataService.Perform<IDataService>(LoadDefaultData, _DataService);
        }

        public void LoadDefaultData(IDataService _DataService)
        {
            AdminDataLoader.Init(_DataService);
        }

        public cDBConnectionSettingEntity GetConnectionSettingByHostName(string _HostName)
        {
            cProfileEntity __ProfileEntity = ProfileDataManager.GetProfileByHostName(_HostName);
            if (__ProfileEntity != null)
            {
                cDBConnectionSettingEntity __Result = new cDBConnectionSettingEntity();
                __Result.GlobalDBName = __ProfileEntity.DBSetting.DBName;
                __Result.UserName = __ProfileEntity.DBSetting.UserId;
                __Result.Password = __ProfileEntity.DBSetting.Password;
                __Result.Server = __ProfileEntity.DBSetting.Server;
                __Result.MaxConnectCount = __ProfileEntity.DBSetting.MaxConnectionCount;
                __Result.EntityType = __ProfileEntity.DBSetting.EntityType;
                return __Result;
            }
            return null;
        }
    }
}
