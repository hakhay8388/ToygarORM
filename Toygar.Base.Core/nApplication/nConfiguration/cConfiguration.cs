using System;
using System.Collections.Generic;
using System.Reflection;
using Toygar.Base.Core.nApplication.nConfiguration.nStartParameter;
using Toygar.Base.Core.nExceptions;
using System.Globalization;
using System.Linq.Expressions;
using Toygar.Base.Core.nCore;
using System.IO;
using Toygar.Base.Boundary.nCore.nBootType;

namespace Toygar.Base.Core.nApplication.nConfiguration
{
    public class cConfiguration : cCoreObject
    {
        public List<string> DomainNames { get; private set; }
        public string UICultureName { get; set; }

        private cStartParameterController StartParameterController = null;

        public string BinPath { get; private set; }

        public string DefaultDataPath { get; set; }
        public string ConfigurationDataPath { get; set; }

        public EBootType BootType { get; set; }


        public bool LogToFile { get; private set; }
        public bool LogToConsole { get; private set; }
        public bool LogDebugEnabled { get; private set; }
        public bool LogInfoEnabled { get; private set; }
        public bool LogExceptionEnabled { get; private set; }
        public bool LogExecutedSqlEnabled { get; set; }
        public bool LogGeneralEnabled { get; set; }


        public CultureInfo UICulture
        {
            get
            {
                try
                {
                    return string.IsNullOrEmpty(UICultureName) ? CultureInfo.InvariantCulture : new CultureInfo(UICultureName);
                }
                catch(Exception _Ex)
                {
					App.Loggers.CoreLogger.LogError(_Ex);
					return CultureInfo.InvariantCulture;
                }
            }
        }

        public string HomePath{ get; private set; }
		public string LogPath { get; private set; }
		public string GeneralLogPath { get; private set; }
		public string ExecutedSqlLogPath { get; private set; }




		public cConfiguration(EBootType _BootType)
            :base(null)
        {
            BootType = _BootType;
            StartParameterController = new cStartParameterController(this);
        }

        public override void Init()
        {
            base.Init();
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.GeneralLogPath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.ExecutedSqlLogPath, true);


            App.Handlers.FileHandler.MakeDirectory(App.Configuration.DefaultDataPath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.ConfigurationDataPath, true);

		}

        public void InitDefault()
        {
            DomainNames = new List<string>() { "Toygar.Base", "Toygar.Base", "Toygar.DB" };
            UICultureName = "tr-TR";
            HomePath = AppDomain.CurrentDomain.BaseDirectory;
            BinPath = AppDomain.CurrentDomain.BaseDirectory;

            LogPath = GetVariableName(() => LogPath);
            LogPath = Path.Combine(HomePath, LogPath);


            ///////// Log Nereye basılacak Ayarı //////
            LogToFile = true;
            LogToConsole = false;
            ///////////////////////////////////////////

            ///////// Hangi Tip loglar basılacak ayarı //////
            LogDebugEnabled = true;
            LogInfoEnabled = true;
            LogExceptionEnabled = true;
            /////////////////////////////////////////////////

            ///////// Hangi loger mekanizmaları aktif olsun //////
            LogExecutedSqlEnabled = true;
            LogGeneralEnabled = true;
            /////////////////////////////////////////////////


            SetPaths();
        }

        public void InnerInit(cApp _App)
        {
            App = _App;
            App.Factories.ObjectFactory.RegisterInstance(GetType(), this);
        }


        private void SetPaths()
        {
            GeneralLogPath = GetVariableName(() => GeneralLogPath);
            GeneralLogPath = Path.Combine(LogPath, GeneralLogPath);

            ExecutedSqlLogPath = GetVariableName(() => ExecutedSqlLogPath);
            ExecutedSqlLogPath = Path.Combine(LogPath, ExecutedSqlLogPath);

            DefaultDataPath = GetVariableName(() => DefaultDataPath);
            DefaultDataPath = Path.Combine(HomePath, DefaultDataPath);

            ConfigurationDataPath = GetVariableName(() => ConfigurationDataPath);
            ConfigurationDataPath = Path.Combine(HomePath, ConfigurationDataPath); 


        }

        protected string GetVariableName<T>(Expression<Func<T>> _Expr)
        {
            var __Body = (MemberExpression)_Expr.Body;
            return __Body.Member.Name;
        }

        private void OverrideConfiguration()
        {
            Console.WriteLine("Parameters Overriding....");
            Type __Type = GetType();
            foreach (var __Item in StartParameterController.ParameterList)
            {
                PropertyInfo __FieldInfo = __Type.SearchProperty(__Item.Key.ToString());
                if (__FieldInfo != null)
                {
                    try
                    {
                        __FieldInfo.SetValue(this, Convert.ChangeType(__Item.Value, __FieldInfo.PropertyType));
                        Console.WriteLine(__Item.Key.ToString() + " : " + __Item.Value + " -> Override success...");
                    }
                    catch(Exception _Ex)
                    {
						App.Loggers.CoreLogger.LogError(_Ex);
						throw new cCoreException(App, "Parametre hatası : " + __Item.Value);
                    }

                }

            }
        }
        public string TryGetParameter(String _ParameterName)
        {
            return StartParameterController.ParameterList[_ParameterName];
        }
    }
}
