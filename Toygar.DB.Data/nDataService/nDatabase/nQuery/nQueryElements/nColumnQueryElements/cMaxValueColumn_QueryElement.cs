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
    public class cMaxValueColumn_QueryElement<TEntity> : cBaseQueryElement, ISelectableColumn where TEntity : cBaseEntity
    {
        public string MaxValueAlias { get; set; }
        public string EntityColumnName { get; set; }
        public bool UseDefaultAlias { get; set; }
        public string Alias { get; set; }
        public cMaxValueColumn_QueryElement(IBaseQuery _Query, cEntityColumn _EntityColumn)
            : this(_Query, _EntityColumn.ColumnName)
        {
            MaxValueAlias = AliasGenerator.GetNewAlias("MaxValue");
        }

        public cMaxValueColumn_QueryElement(IBaseQuery _Query, string _EntityColumn, string _Alias = "", bool _UseDefaultAlias = true)
            : base(_Query)
        {
            MaxValueAlias = AliasGenerator.GetNewAlias("MaxValue");
            EntityColumnName = _EntityColumn;
            UseDefaultAlias = _UseDefaultAlias;
            Alias = _Alias;
        }

        public override string ToElementString(params object[] _Params)
        {

            if (UseDefaultAlias)
            { 
                return " Max(" + Query.DefaultAlias + "." + EntityColumnName + ") " + (Alias == "" ? MaxValueAlias : Alias) + " ";
            }
            else
            {
                return " Max(" + EntityColumnName + ") " + (Alias == "" ? MaxValueAlias : Alias) + " ";
            }
            
        }

        public string GetColumnName()
        {
            return MaxValueAlias;
        }
    }
}
