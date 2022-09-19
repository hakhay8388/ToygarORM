using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements
{
    public class cSelectCount_QueryElement<TEntity> : cBaseQueryElement, ISelectableColumn where TEntity : cBaseEntity
    {
        public string CountValueAlias { get; set; }
        public cSelectCount_QueryElement(IBaseQuery _Query)
            : base(_Query)
        {
            CountValueAlias = AliasGenerator.GetNewAlias("CountValue");
        }

        public cSelectCount_QueryElement(IBaseQuery _Query, string _CustomCountColumnAlias)
           : base(_Query)
        {
            CountValueAlias = _CustomCountColumnAlias;
        }

        public override string ToElementString(params object[] _Params)
        {
            return "Count(*) " + CountValueAlias;
        }

        public string GetColumnName()
        {
            return CountValueAlias;
        }
    }
}
