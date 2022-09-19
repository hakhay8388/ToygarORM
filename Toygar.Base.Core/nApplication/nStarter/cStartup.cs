//using Toygar.Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Toygar.Base.Core.nAttributes;
using Toygar.Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.Base.Core.nApplication.nStarter
{
    [ToygarRegister]
    public class cStartup<TStarter> : cCoreObject, IStarter where TStarter : IStarter
    {
        public TStarter StarterInstance { get; set; }
        public cStartup(cApp _App)
            :base(_App)
        {
            StarterInstance = _App.Factories.ObjectFactory.ResolveInstance<TStarter>();
        }
        public void Start(cApp _App)
        {
            CultureInfo.DefaultThreadCurrentCulture = _App.Configuration.UICulture;
            CultureInfo.DefaultThreadCurrentUICulture = _App.Configuration.UICulture;

            //Ön yükleme yapılacak


            StarterInstance.Start(_App);

        }

     
    }
}
