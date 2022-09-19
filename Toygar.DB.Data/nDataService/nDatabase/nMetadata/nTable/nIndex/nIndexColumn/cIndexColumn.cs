using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable.nIndex.nIndexColumn
{
    public class cIndexColumn
    { 
        public cIndex Index { get; set; }
        public cIndexColumnCoreEnitity IndexColumnEnitity { get; set; }

        public cIndexColumn(cIndex _Index, cIndexColumnCoreEnitity _IndexColumnEnitity)
        {
            Index = _Index;
            IndexColumnEnitity = _IndexColumnEnitity;
        }       
    }
}
