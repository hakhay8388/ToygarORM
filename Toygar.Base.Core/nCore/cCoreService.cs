using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nApplication.nBootstrapper;
using Toygar.Base.Core.nApplication.nConfiguration;
using Toygar.Base.Core.nApplication.nFactories;
using Toygar.Base.Core.nApplication.nFactories.nFormFactory;
using Toygar.Base.Core.nApplication.nFactories.nHookedObjectFactory;
using Toygar.Base.Core.nAttributes;
using Toygar.Base.Core.nHandlers;
using Toygar.Base.Core.nApplication.nCoreLoggers;
//using Toygar.Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;

namespace Toygar.Base.Core.nCore
{
    public class cCoreService<TServiceContext> : cCoreObject where TServiceContext : cCoreServiceContext
    {
        public TServiceContext ServiceContext { get; set; }
        public cCoreService(TServiceContext _ServiceContext)
            :base(_ServiceContext.App)
        {
            ServiceContext = _ServiceContext;
        }

    }
}
