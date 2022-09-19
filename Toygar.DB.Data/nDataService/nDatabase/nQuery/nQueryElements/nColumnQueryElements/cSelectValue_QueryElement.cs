using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements
{
    public class cSelectValue_QueryElement<TEntity> : cBaseQueryElement, ISelectableColumn where TEntity : cBaseEntity
    {
        public string ColumnAs { get; set; }
        public object Value { get; set; }

        public cSelectValue_QueryElement(IBaseQuery _Query,  object _Value, string _ColumnAs)
            : base(_Query)
        {
            Value = _Value;
            ColumnAs = _ColumnAs;
        }

        public override string ToElementString(params object[] _Params)
        {
            if (Value.GetType() == typeof(string))
            {
                return "'" + Value.ToString() + "'" + (ColumnAs.IsNullOrEmpty() ? "" : " AS " + ColumnAs);
            }
            else
            {
                return Value.ToString() + (ColumnAs.IsNullOrEmpty() ? "" : " AS " + ColumnAs);
            }            
        }

        public string GetColumnName()
        {
            return ColumnAs;
        }
    }
}
