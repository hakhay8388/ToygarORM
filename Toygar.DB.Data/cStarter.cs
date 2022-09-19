using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nApplication.nStarter;
using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataServiceManager;

namespace Toygar.DB.Data
{
    public class cStarter : cCoreObject, IStarter
    {
        IDataServiceManager DataServiceManager { get; set; }

        public cStarter(cApp _App, IDataServiceManager _DataServiceManager)
            : base(_App)
        {
            DataServiceManager = _DataServiceManager;
        }

        public void Start(cApp _App)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
        }
    }
}
