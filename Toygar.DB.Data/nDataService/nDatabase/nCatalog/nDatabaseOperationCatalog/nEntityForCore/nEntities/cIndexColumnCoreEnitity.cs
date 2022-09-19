using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities
{
    public class cIndexColumnCoreEnitity : cBaseCoreEntity<cIndexColumnCoreEnitity>
    {
        [CoreDBField("")]
        public string IndexName { get; set; }
        [CoreDBField(0)]
        public long IndexID { get; set; }
        [CoreDBField(0)]
        public long Type { get; set; }
        [CoreDBField("")]
        public string TypeDesc { get; set; }
        [CoreDBField(false)]
        public bool IsUnique { get; set; }
        [CoreDBField(false)]
        public bool IsPrimaryKey { get; set; }
        [CoreDBField(false)]
        public bool IsUniqueConstraint { get; set; }
        [CoreDBField("")]
        public string ColumnName { get; set; }
        [CoreDBField(0)]
        public int ColumnID { get; set; }
        [CoreDBField(0)]
        public int KeyOrdinal { get; set; }
    }
}
