using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities
{
    public class cDefaultConstraintCoreEnitity : cBaseCoreEntity<cColumnCoreEnitity>
    {
        [CoreDBField("")]
        public string ConstraintName { get; set; }

        [CoreDBField("")]
        public string TableName { get; set; }
    }
}
