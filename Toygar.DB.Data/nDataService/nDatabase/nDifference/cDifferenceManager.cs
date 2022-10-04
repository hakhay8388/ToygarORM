using Bootstrapper.Core.nCore;
using Toygar.DB.Data.nDataService.nDatabase.nDifference.nDiff_Table;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable;
using Toygar.DB.Data.nDataService.nDatabase;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.DB.Data.nDataService.nDatabase.nSql;

namespace Toygar.DB.Data.nDataService.nDatabase.nDifference
{
    public class cDifferenceManager : cBaseDatabaseComponent
    {
        cDiff_TableManager Diff_TableManager { get; set; }
        public cDifferenceManager(IDatabase _Database)
            :base(_Database)
        {
            Diff_TableManager = new cDiff_TableManager(this);
            CalculateDifferences();
        }

        public void CalculateDifferences()
        {
            Diff_TableManager.Calculate();
        }

        public void Synchronize()
        {
            Diff_TableManager.Synchronize();
        }
    }
}
