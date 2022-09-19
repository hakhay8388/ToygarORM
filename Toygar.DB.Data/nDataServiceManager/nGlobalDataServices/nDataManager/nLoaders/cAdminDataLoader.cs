using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase.nQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataServiceManager;

namespace Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nDataManagers.nLoaders
{
    public class cAdminDataLoader : cBaseDataLoader
    {
        public cProfileDataManager ProfileDataManager { get; set; }
        public cAdminDataLoader(cGlobalDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, cProfileDataManager _ProfileDataManager)
          : base(_CoreServiceContext, _DataServiceManager)
        {
            ProfileDataManager = _ProfileDataManager;
        }

        public void Init(IDataService _DataService)
        {
            if (ProfileDataManager.GetProfileByHostName("localhost") == null)
            {
                ProfileDataManager.CreateProfile("localhost", DateTime.Now.AddYears(10), ServiceContext.Configuration.GlobalDBUserName, ServiceContext.Configuration.GlobalDBPassword, ServiceContext.Configuration.GlobalDBServer, "TestDB2", ServiceContext.Configuration.MaxConnectCount, "App.QueryTester.nDataServices.nDataService.nEntityServices.nEntities.cBaseQueryTesterEntity");
            }
        }
    }
}
