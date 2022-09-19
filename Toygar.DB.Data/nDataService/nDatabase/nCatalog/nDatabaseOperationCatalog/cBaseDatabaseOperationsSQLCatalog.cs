using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog
{
    public abstract class cBaseDatabaseOperationsSQLCatalog : cBaseCatalogOperations
    {
        public cBaseDatabaseOperationsSQLCatalog(IDatabase _Database)
            :base(_Database)
        {
        }
        public abstract void CreateDatabase(string _DBName);
        public abstract void DropDatabase(string _DBName);
        public abstract bool IsDatabaseExists(string _DBName);

        public abstract cTableCoreEnitity FindTable(string _TableName);
        public abstract List<cTableCoreEnitity> GetTables();
		public abstract List<cTableCoreEnitity> GetLockedTables();

		public abstract List<cColumnCoreEnitity> GetColumnsByTableName(string _Name);
        public abstract List<cIndexCoreEnitity> GetIndexesByTableName(string _Name);
        public abstract List<cIndexColumnCoreEnitity> GetIndexConstraintColumnByIndexName(string _IndexName);
        public abstract List<cForeignKeyCoreEnitity> GetForegnKeyByParentTableName(string _TableName);
        public abstract List<cForeignKeyCoreEnitity> GetForegnKeyByReferencedTableName(string _TableName);
        public abstract List<cIdentityCoreEnitity> GetIdentityByTableName(string _TableName);
        public abstract List<cDefaultConstraintCoreEnitity> GetDefaultConstraintsByTable(string _TableName);

        public abstract string ParseErrorMessageConstraintName(string _ErrorMessage);
        public abstract void SetDbLevelParams(string _DBName);

    }
}
