using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nCore;
using Toygar.Base.Core.nUtils.nHashUtils;
using Toygar.Base.Core.nUtils.nImpersonatedUserUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.Base.Core.nUtils
{
    public class cUtils : cCoreObject
    {
        public cHashUtils HashUtils { get; set; }
        public cImpersonatedUserUtils ImpersonatedUserUtils { get; set; }
        public cUtils(nApplication.cApp _App)
            :base(_App)
        {
            HashUtils = new cHashUtils(_App);
            ImpersonatedUserUtils = new cImpersonatedUserUtils(_App);

        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cUtils>(this);
            HashUtils.Init();
            ImpersonatedUserUtils.Init();
        }
    }
}
