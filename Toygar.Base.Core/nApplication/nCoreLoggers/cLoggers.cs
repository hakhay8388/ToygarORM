using Toygar.Base.Core.nApplication.nCoreLoggers.nCoreLogger;
using Toygar.Base.Core.nApplication.nCoreLoggers.nSqlLogger;
using Toygar.Base.Core.nAttributes;
using Toygar.Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.Base.Core.nApplication.nCoreLoggers
{
    public class cLoggers :cCoreObject
    {
        public cCoreLogger CoreLogger {get;set;}

        public cCoreSqlLogger SqlLogger { get; set; }


		public cLoggers(cApp _App)
            :base(_App)
        {
            CoreLogger = new cCoreLogger(_App);
            SqlLogger = new cCoreSqlLogger(_App);

		}

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cLoggers>(this);

            CoreLogger.Init();
            SqlLogger.Init();
		}
    }
}
