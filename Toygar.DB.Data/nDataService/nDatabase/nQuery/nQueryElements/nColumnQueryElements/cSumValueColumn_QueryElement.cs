using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements
{
    public class cSumValueColumn_QueryElement<TEntity> : cBaseQueryElement, ISelectableColumn where TEntity : cBaseEntity
    {
        public string SumValueAlias { get; set; }
        public string EntityColumnName { get; set; }
        public bool UseDefaultAlias { get; set; }
        public string Alias { get; set; }
        public cSumValueColumn_QueryElement(IBaseQuery _Query, cEntityColumn _EntityColumn)
            : this(_Query, _EntityColumn.ColumnName)
        {
            SumValueAlias = AliasGenerator.GetNewAlias("SumValue");
        }

        public cSumValueColumn_QueryElement(IBaseQuery _Query, string _EntityColumn, string _Alias = "", bool _UseDefaultAlias = true)
            : base(_Query)
        {
            SumValueAlias = AliasGenerator.GetNewAlias("SumValue");
            EntityColumnName = _EntityColumn;
            UseDefaultAlias = _UseDefaultAlias;
            Alias = _Alias;
        }

        public override string ToElementString(params object[] _Params)
        {
            if (UseDefaultAlias)
            {
                return " Sum(" + Query.DefaultAlias + "." + EntityColumnName + ") " + (Alias == "" ? SumValueAlias : Alias) + " ";
            }
            else
            {
                return " Sum(" + EntityColumnName + ") " + (Alias == "" ? SumValueAlias : Alias) + " ";
            }

        }

        public string GetColumnName()
        {
            return SumValueAlias;
        }
    }
}
