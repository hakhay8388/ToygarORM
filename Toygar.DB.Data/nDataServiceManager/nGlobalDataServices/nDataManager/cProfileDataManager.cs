using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase.nQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataServiceManager;
using Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nEntityServices.nEntities;

namespace Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nDataManagers
{
    public class cProfileDataManager : cBaseDataManager
    {
        public cProfileDataManager(cGlobalDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager)
          : base(_CoreServiceContext, _DataServiceManager)
        {
        }


        public cProfileEntity GetProfileByHostName(string _HostName)
        {
            IDataService __DataService = DataServiceManager.GetGlobalDataService();
            cProfileEntity __Result = __DataService.Database.Query<cProfileEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.HostName).Eq(_HostName)
                .ToQuery()
                .ToList()
                .FirstOrDefault();

            return __Result;
        }

        public void CreateProfile(
            string _HostName
            , DateTime _EndDate
            , string _DBUserId
            , string _DBPassword
            , string _DBServer
            , string _DBName
            , int _DBMaxConnectionCount
            , string _EntityType
            )
        {
            IDataService __DataService = DataServiceManager.GetGlobalDataService();

            cProfileEntity __ProfileEntity = GetProfileByHostName(_HostName);
            if (__ProfileEntity == null)
            {
                __ProfileEntity = __DataService.Database.CreateNew<cProfileEntity>();
                __ProfileEntity.HostName = _HostName;
                __ProfileEntity.EndDate = _EndDate;
                __ProfileEntity.Save();

                if (!__ProfileEntity.DBSetting.IsValid)
                {
                    __ProfileEntity.DBSetting.Password = _DBPassword;
                    __ProfileEntity.DBSetting.UserId = _DBUserId;
                    __ProfileEntity.DBSetting.Server = _DBServer;
                    __ProfileEntity.DBSetting.DBName = _DBName;
                    __ProfileEntity.DBSetting.MaxConnectionCount = _DBMaxConnectionCount;
                    __ProfileEntity.DBSetting.EntityType = _EntityType;
                    __ProfileEntity.DBSetting.Save(__ProfileEntity);

                }
            }
        }
    }
}
