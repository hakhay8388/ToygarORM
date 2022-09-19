using Toygar.Base.Core.nApplication.nCoreLoggers;
////using Toygar.Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Toygar.Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.Base.Core.nApplication.nFactories.nFormFactory
{
    public class cFormFactory : cCoreObject
    {
        public cFormFactory(cApp _App)
            :base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cFormFactory>(this);
        }

        private TForm CreateFrom<TForm>()
        {
            TForm __Result = typeof(TForm).ResolveInstance<TForm>(App);
            //App.Factories.ObjectFactory.ResolveInnerObject(__Result);
            /*MethodInfo __MethodInfo = typeof(TForm).SearchMethod("Init");
            if (__MethodInfo != null)
            {
                __MethodInfo.Invoke(__Result, new object[] { });
            }*/
            return __Result;
        }

    }
}
