using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nDifference.nDiff_Table
{
    public class cDiff_Table
    {
        public cEntityTable EntityTable { get; set; }
        public cTable DBTable { get; set; }
        public cDiff_Table(cEntityTable _EntityTable, cTable _DBTable)
        {
            EntityTable = _EntityTable;
            DBTable = _DBTable;
        }
    }
}
