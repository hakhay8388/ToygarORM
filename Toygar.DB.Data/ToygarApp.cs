using SharpRaven.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Toygar.Base.Boundary.nCore.nBootType;
using Toygar.Base.Core.nApplication;
using Toygar.DB.Data.nConfiguration;
using Toygar.DB.Data.nDataServiceManager;

namespace Toygar.DB.Data
{
    public  static class ToygarApp
    {
        private static cApp App { get; set; }
        public static  cDataConfiguration CreateConfiguration(EBootType _EBootType)
        {
            cDataConfiguration __DataConfiguration = new cDataConfiguration(_EBootType);
            return __DataConfiguration;
        }

        public static cDataConfiguration CreateConfiguration<TConfiguration>(EBootType _EBootType)
            where TConfiguration : cDataConfiguration
        {
            TConfiguration __DataConfiguration = (TConfiguration)typeof(TConfiguration).CreateInstance(new object[] { _EBootType });
            return __DataConfiguration;
        }

        public static void Init(cDataConfiguration _DataConfiguration)
        {
            App = cApp.Start<cStarter>(_DataConfiguration);
        }

        public static IDataServiceManager GetDataServiceManager()
        {
            return App.Factories.ObjectFactory.ResolveInstance<IDataServiceManager>();
        }

    }
}
