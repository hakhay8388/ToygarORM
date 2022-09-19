using System;
using System.IO;

namespace Toygar.Base.Core.nApplication.nCoreLoggers.nCoreLogger
{
    public class cCoreLogger : cBaseLogger
    {
        public cCoreLogger(cApp _App)
            : base(_App)
        {
        }
        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cCoreLogger>(this);
        }

        protected override string LogPath()
        {
            return App.Configuration.GeneralLogPath;
        }

		protected override bool IsEnabled()
		{
			return App.Configuration.LogGeneralEnabled;
		}
	}
}
