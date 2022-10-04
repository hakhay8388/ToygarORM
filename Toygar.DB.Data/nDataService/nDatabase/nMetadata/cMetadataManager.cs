using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable;
using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.DB.Data.nDataService.nDatabase;
using Toygar.DB.Data.nDataService.nDatabase.nSql;

namespace Toygar.DB.Data.nDataService.nDatabase.nMetadata
{
    public class cMetadataManager : cBaseDatabaseComponent
    {
        public cTableManager TableManager { get; set; }

        public cMetadataManager(IDatabase _Database)
            : base(_Database)
        {
            TableManager = new cTableManager(this);
            if (Database.DefaultConnection.IsOpen)
            {
                Reload();
            }
			else
			{
				TableManager.MetadataManager.Database.App.Loggers.SqlLogger.LogError(new Exception("Connection Pool Hatasi"));
			}
        }

        public void Reload()
        {
            TableManager.Create();
        }

        public void DropForeignKeys()
        {
            TableManager.DropForeignKeys();
        }

        public void DropIndexes()
        {
            TableManager.DropIndexes();
        }

        public void DropTables()
        {
            TableManager.DropTables();
        }
    }
}
