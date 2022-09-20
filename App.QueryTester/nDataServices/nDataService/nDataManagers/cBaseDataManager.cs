using App.QueryTester.nDataServices.nDataService;
using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataServiceManager;
using System;
using System.Collections.Generic;
using System.Text;
using Toygar.Base.Core.nApplication;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;

namespace App.QueryTester.nDataServices.nDataService.nDataManagers
{
    public class cBaseDataManager<TBaseEntity> : cCoreObject
        where TBaseEntity : cBaseEntity
    {
        public IDataServiceManager DataServiceManager { get; set; }
        public cBaseDataManager(IDataServiceManager _DataServiceManager)
          : base(cApp.App)
        {
            DataServiceManager = _DataServiceManager;
        }
    }
}
