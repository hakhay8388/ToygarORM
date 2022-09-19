using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities
{
    public class cColumnCoreEnitity : cBaseCoreEntity<cColumnCoreEnitity>
    {
        [CoreDBField("")]
        public string TableName { get; set; }
        [CoreDBField("")]
        public string ColumnName { get; set; }
        [CoreDBField("")]
        public string DataTypeName { get; set; }
        [CoreDBField(0)]
        public int DataType { get; set; }
        [CoreDBField(0)]
        public int Length { get; set; }
        [CoreDBField(0)]
        public int Precision { get; set; }
        [CoreDBField("")]
        public string Scale { get; set; }
        [CoreDBField(true)]
        public bool Nullable { get; set; }
    }
}
