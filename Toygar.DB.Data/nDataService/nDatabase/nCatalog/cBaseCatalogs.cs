using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDataToolCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nTableOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog
{
    public abstract class cBaseCatalogs : cBaseDatabaseComponent
    {
        public cBaseDatabaseOperationsSQLCatalog DatabaseOperationsSQLCatalog { get; set; }
        public cBaseTableOperationSQLCatalog TableOperationSQLCatalog { get; set; }
        public cBaseRowOperationSQLCatalog RowOperationSQLCatalog { get; set; }
        public cBaseDataToolOperationSQLCatalog DataToolOperationSQLCatalog { get; set; }


        public cBaseCatalogs(IDatabase _Database)
            :base(_Database)
        {
            InitCatalogs();
        }

        protected abstract cBaseDatabaseOperationsSQLCatalog GetDatabaseOperationsSQLCatalog();
        protected abstract cBaseTableOperationSQLCatalog GetTableOperationSQLCatalog();
        protected abstract cBaseRowOperationSQLCatalog GetRowOperationSQLCatalog();
        protected abstract cBaseDataToolOperationSQLCatalog GetDataToolOperationSQLCatalog();

        public void InitCatalogs()
        {
            DatabaseOperationsSQLCatalog = GetDatabaseOperationsSQLCatalog();
            TableOperationSQLCatalog = GetTableOperationSQLCatalog();
            RowOperationSQLCatalog = GetRowOperationSQLCatalog();
            DataToolOperationSQLCatalog = GetDataToolOperationSQLCatalog();
        }
    }
}
