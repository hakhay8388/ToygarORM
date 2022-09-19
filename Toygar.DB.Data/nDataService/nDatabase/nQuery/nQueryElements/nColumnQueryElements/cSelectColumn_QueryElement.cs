using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements
{
    public class cSelectColumn_QueryElement<TEntity> : cBaseQueryElement, ISelectableColumn where TEntity : cBaseEntity
    {
        public string EntityColumnName { get; set; }

        public string ColumnAs { get; set; }
        public cSelectColumn_QueryElement(IBaseQuery _Query, cEntityColumn _EntityColumn, string _ColumnAs = "")
            : this(_Query, _EntityColumn.ColumnName)
        {
            ColumnAs = _ColumnAs;
        }

        public cSelectColumn_QueryElement(IBaseQuery _Query, string _EntityColumn, string _ColumnAs = "")
            : base(_Query)
        {
            EntityColumnName = _EntityColumn;
            ColumnAs = _ColumnAs;
        }

        public override string ToElementString(params object[] _Params)
        {
            if (ColumnAs.IsNullOrEmpty())
            {
                return Query.DefaultAlias + "." + EntityColumnName;
            }
            else
            {
                return Query.DefaultAlias + "." + EntityColumnName + " AS " + ColumnAs;
            }
        }

        public string GetColumnName()
        {
            return EntityColumnName;
        }
    }
}
