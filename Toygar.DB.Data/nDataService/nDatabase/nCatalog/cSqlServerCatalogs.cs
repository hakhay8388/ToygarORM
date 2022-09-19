using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDataToolCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nTableOperationCatalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog
{
    public class cSqlServerCatalogs : cBaseCatalogs
    {
        public cSqlServerCatalogs(IDatabase _Database)
            :base(_Database)
        {
        }

        protected override cBaseDatabaseOperationsSQLCatalog GetDatabaseOperationsSQLCatalog()
        {
            return new cSqlServerDatabaseOperationsSQLCatalog(Database);
        }

        protected override cBaseTableOperationSQLCatalog GetTableOperationSQLCatalog()
        {
            return new cSqlServerTableOperationSQLCatalog(Database);
        }

        protected override cBaseRowOperationSQLCatalog GetRowOperationSQLCatalog()
        {
            return new cSqlServerRowOperationSQLCatalog(Database);
        }

        protected override cBaseDataToolOperationSQLCatalog GetDataToolOperationSQLCatalog()
        {
            return new cSqlServerDataToolOperationSQLCatalog(Database);
        }
    }
}
