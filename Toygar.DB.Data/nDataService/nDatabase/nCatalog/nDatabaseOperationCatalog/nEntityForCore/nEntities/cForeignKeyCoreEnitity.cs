using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities
{
    public class cForeignKeyCoreEnitity : cBaseCoreEntity<cColumnCoreEnitity>
    {
        [CoreDBField("")]
        public string ParentColumnName { get; set; }
        [CoreDBField("")]
        public string ReferencedColumnName { get; set; }
        [CoreDBField("")]
        public string ReferencedTableName { get; set; }
        [CoreDBField("")]
        public string ParentTableName { get; set; }
        [CoreDBField("")]
        public string ForeignKeyName { get; set; }
    }
}
