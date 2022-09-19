using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities
{
    public class cIdentityCoreEnitity : cBaseCoreEntity<cIdentityCoreEnitity>
    {
        [CoreDBField("")]
        public string TableName { get; set; }
        [CoreDBField("")]
        public string ColumnName { get; set; }
        [CoreDBField(1)]
        public int SeedValue { get; set; }
        [CoreDBField(1)]
        public int IncrementValue { get; set; }
        [CoreDBField(0)]
        public int LastValue { get; set; }
        [CoreDBField(0)]
        public int IsNotForReplication { get; set; }
    }
}
