using Bootstrapper.Core.nCore;
using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nDataManager
{
    public class cBaseDataManager : cCoreService<cGlobalDataServiceContext>
    {
        public IDataServiceManager DataServiceManager { get; set; }
        public cBaseDataManager(cGlobalDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager)
          : base(_CoreServiceContext)
        {
            DataServiceManager = _DataServiceManager;
        }
    }
}
