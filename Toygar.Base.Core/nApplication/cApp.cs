using Toygar.Base.Core.nApplication.nBootstrapper;
using Toygar.Base.Core.nApplication.nBootstrapper.nConventionOverrider;
using Toygar.Base.Core.nApplication.nConfiguration;
using Toygar.Base.Core.nApplication.nCoreLoggers;
using Toygar.Base.Core.nApplication.nFactories;
using Toygar.Base.Core.nApplication.nFactories.nFormFactory;
using Toygar.Base.Core.nApplication.nFactories.nHookedObjectFactory;
using Toygar.Base.Core.nApplication.nStarter;
using Toygar.Base.Core.nAttributes;
using Toygar.Base.Core.nHandlers;
using Toygar.Base.Core.nUtils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Toygar.Base.Boundary.nCore.nBootType;

namespace Toygar.Base.Core.nApplication
{
    public class cApp
    {
        public string ApplicationName { get { return Configuration.HomePath; } }
        public cBootstrapper Bootstrapper { get; set; }
        public cConfiguration Configuration { get; set; }
        public cFactories Factories { get; set; }
        public cHandlers Handlers { get; set; }
        public cLoggers Loggers { get; set; }
        public cUtils Utils { get; set; }
        public CultureInfo Culture { get; set; }
        public CultureInfo UICulture { get; set; }

        public cApp(cConfiguration _Configuration)
        {
            Configuration = _Configuration;
            Bootstrapper = new cBootstrapper(this);
            Factories = new cFactories(this);
            Loggers = new cLoggers(this);
            Handlers = new cHandlers(this);
            Utils = new cUtils(this);

            Init();
            Configuration.GetType().SearchMethod("InnerInit").Invoke(_Configuration, new object[] { this });
            Configuration.Init();
            Configuration.GetType().SearchMethod("OverrideConfiguration").Invoke(_Configuration, new object[] {});
            Bootstrapper.Init();
            Factories.Init();
            Loggers.Init();
            Handlers.Init();
            Utils.Init();
        }

        public TConfiguration Cfg<TConfiguration>() where TConfiguration : cConfiguration
        {
            return (TConfiguration)Configuration;
        }

        public void Init()
        {
            Factories.ObjectFactory.RegisterInstance<cApp>(this);
        }

        public static cApp App { get; set; }
        public static cApp Start<TStarter>(cConfiguration _Configuration, List<cOverrideTypeItem> _Overrides = null) where TStarter : IStarter
        {
            App = new cApp(_Configuration);
            App.Bootstrapper.Init<TStarter>(_Overrides);
            return App;
        }

    }
}
