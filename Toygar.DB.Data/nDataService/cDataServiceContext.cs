using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Toygar.DB.Data.nConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService
{
    public class cDataServiceContext : cCoreServiceContext
    {
        public cDataConfiguration Configuration { get { return App.Cfg<cDataConfiguration>(); } }
        public cDataServiceContext(cApp _App)
            :base(_App)
        {
        }
    }
}
