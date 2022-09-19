using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataServiceManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nDataManagers
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
