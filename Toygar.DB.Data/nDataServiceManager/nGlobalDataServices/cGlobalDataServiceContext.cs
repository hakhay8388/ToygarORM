using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Toygar.DB.Data.nConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataServiceManager.nGlobalDataServices
{
    public class cGlobalDataServiceContext: cCoreServiceContext
    {
        public cDataConfiguration Configuration { get { return App.Cfg<cDataConfiguration>(); } }
        public cGlobalDataServiceContext(cApp _App)
            :base(_App)
        {
        }
    }
}
