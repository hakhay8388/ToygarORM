using Toygar.Base.Boundary.nCore.nObjectLifeTime;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.Base.Core.nApplication.nBootstrapper.nConventionOverrider
{
    public class cOverrideTypeItem
    {
        public Type From { get; set; }
        public Type To { get; set; }
        public LifeTime LifetimeManager { get; set; }
        public cOverrideTypeItem(Type _From, Type _To, LifeTime _LifetimeManager)
        {
            From = _From;
            To = _To;
            LifetimeManager = _LifetimeManager;
        }
    }
}
