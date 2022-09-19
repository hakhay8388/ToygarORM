using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nApplication.nBootstrapper;
using Toygar.Base.Core.nApplication.nConfiguration;
using Toygar.Base.Core.nApplication.nCoreLoggers;
using Toygar.Base.Core.nApplication.nFactories;
using Toygar.Base.Core.nHandlers;
using Toygar.Base.Core.nUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.Base.Core.nCore
{
    public abstract class cCoreObject 
    {
        public cApp App { get; set; }

        public cCoreObject(cApp _App)
        {
            App = _App;
        }

        public virtual void Init()
        {
        }

        public new Type GetType()
        {
            Type __Type = base.GetType();
            if (__Type.FullName.Contains("__Proxy__"))
            {
                __Type = __Type.BaseType;
            }
            return __Type;
        }
    }
}
