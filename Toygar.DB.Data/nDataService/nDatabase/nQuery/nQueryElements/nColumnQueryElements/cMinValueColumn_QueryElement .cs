using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements
{
    public class cMinValueColumn_QueryElement <TEntity> : cBaseQueryElement, ISelectableColumn where TEntity : cBaseEntity
    {
        public string MinValueAlias { get; set; }
        public string EntityColumnName { get; set; }
        public cMinValueColumn_QueryElement (IBaseQuery _Query, cEntityColumn _EntityColumn)
            : this(_Query, _EntityColumn.ColumnName)
        {
            MinValueAlias = AliasGenerator.GetNewAlias("MinValue");
        }

        public cMinValueColumn_QueryElement(IBaseQuery _Query, string _EntityColumn)
            : base(_Query)
        {
            MinValueAlias = AliasGenerator.GetNewAlias("MinValue");
            EntityColumnName = _EntityColumn;
        }

        public override string ToElementString(params object[] _Params)
        {
            return " Min(" + Query.DefaultAlias + "." + EntityColumnName + ") " + MinValueAlias + " ";
        }

        public string GetColumnName()
        {
            return MinValueAlias;
        }
    }
}
