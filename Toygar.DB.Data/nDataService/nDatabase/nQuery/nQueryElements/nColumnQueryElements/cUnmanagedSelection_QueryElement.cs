using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements
{
    public class cUnmanagedSelection_QueryElement<TEntity> : cBaseQueryElement, ISelectableColumn where TEntity : cBaseEntity
    {
        public string Value { get; set; }

        public cUnmanagedSelection_QueryElement(IBaseQuery _Query, string _Value)
            : base(_Query)
        {
            Value = _Value;
        }

        public override string ToElementString(params object[] _Params)
        {
            return " " + Value + " ";
        }

        public string GetColumnName()
        {
            return Value;
        }
    }
}
