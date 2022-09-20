using Toygar.Base.Boundary.nCore.nBootType;
using Toygar.Base.Boundary.nData;
using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nApplication.nConfiguration;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System.Collections.Generic;
using System.IO;
using Toygar.DB.Data.nConfiguration.nDBItemConfig;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;

namespace Toygar.DB.Data.nConfiguration
{
    public class cDataConfiguration: cConfiguration
    {

        public string GlobalDBUserName { get; set; }
        public string GlobalDBPassword { get; set; }
        public string GlobalDBServer { get; set; }
        public int MaxConnectCount { get; set; }
        public EDBVendor DBVendor { get; set; }
        public string GlobalDBName { get; set; }
        public int DBVersion { get; set; }
        public List<cDBItemConfig> DBItemConfigs { get; set; }

        public bool SimulateDBSynchronize { get; set; }
        public bool LoadDefaultDataOnStart { get; set; }
        public List<cSql> ExecutedSqlList { get; set; }
        public cDataConfiguration(EBootType _BootType)
            :base(_BootType)
        {
            DBItemConfigs = new List<cDBItemConfig>();
            InitDefault();
        }

        public void Add<TEntityType>(string _HostName, string _UserName, string _Password , string _Server, string _DBName, int _MaxConnectCount , EDBVendor _DBVendor)
            where TEntityType : cBaseEntity
        {
            DBItemConfigs.Add(new cDBItemConfig() { 
                HostName = _HostName
                , UserId = _UserName
                , Password = _Password
                , Server = _Server
                , MaxConnectCount = _MaxConnectCount
                , DBVendor = _DBVendor
                , DBName = _DBName
                , EntityType = typeof(TEntityType).FullName
            });
        }

        public cDBItemConfig Find<TEntityType>(string _HostName)
            where TEntityType : cBaseEntity
        {
            return DBItemConfigs.Find(__Item => __Item.HostName == _HostName && __Item.EntityType == typeof(TEntityType).FullName);
        }

        public void WriteExecutedSqlListToFile()
        {
            string __LogPath = Path.Combine(GeneralLogPath, "SqlExcutionLog.log");

            App.Handlers.FileHandler.AppendString("", __LogPath);
            for (int i = 0; i < ExecutedSqlList.Count; i++)
            {
                App.Handlers.FileHandler.AppendString("\n", __LogPath);
                App.Handlers.FileHandler.AppendString(ExecutedSqlList[i].FullSQLString, __LogPath);
                if (ExecutedSqlList[i].Parameters.Count > 0)
                {
                    App.Handlers.FileHandler.AppendString("\n", __LogPath);
                    App.Handlers.FileHandler.AppendString("//////////// PARAMETERS /////////// ", __LogPath);
                    App.Handlers.FileHandler.AppendString("\n", __LogPath);
                    foreach (var __Item in ExecutedSqlList[i].Parameters)
                    {
                        App.Handlers.FileHandler.AppendString("\t" + __Item.Key + "\t:\t" + __Item.Value, __LogPath);
                        App.Handlers.FileHandler.AppendString("\n", __LogPath);
                    }
                    App.Handlers.FileHandler.AppendString("/////////////////////////////////// ", __LogPath);
                    App.Handlers.FileHandler.AppendString("\n", __LogPath);
                }

            }

            App.Handlers.ProcessHandler.OpenModalProcess("notepad.exe", __LogPath);
        }

        public override void Init()
        {
            base.Init();
            ExecutedSqlList = new List<cSql>();
            LoadVersion();            
        }

        private void LoadVersion()
        {
            //VersionEntity = FileDataService.FindByID<cVersionEntity>(1);
        }
    }
}
