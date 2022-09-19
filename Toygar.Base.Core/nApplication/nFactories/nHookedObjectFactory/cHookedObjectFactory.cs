using Toygar.Base.Core.nApplication.nCoreLoggers;
//using Toygar.Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Toygar.Base.Core.nApplication.nFactories.nHookedObjectFactory.nPropertyHookedObjectFactory;
using Toygar.Base.Core.nAttributes;
using Toygar.Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.Base.Core.nApplication.nFactories.nHookedObjectFactory
{
    public class cHookedObjectFactory : cCoreObject
    {
        public cPropertyHookedObjectFactory PropertyHookedObjectFactory { get; set; }
        public cHookedObjectFactory(cApp _App)
            :base(_App)
        {
            PropertyHookedObjectFactory = new cPropertyHookedObjectFactory(_App);
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cHookedObjectFactory>(this);
        }
    }
}
