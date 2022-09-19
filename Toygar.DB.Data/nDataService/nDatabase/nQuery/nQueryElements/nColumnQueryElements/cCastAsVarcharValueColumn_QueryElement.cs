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
    public class cCastAsVarcharValueColumn_QueryElement<TEntity> : cBaseQueryElement, ISelectableColumn where TEntity : cBaseEntity
    {
        public string CastAsVarcharValueAlias { get; set; }
        public string EntityColumnName { get; set; } 
        public string Alias { get; set; }
        public cCastAsVarcharValueColumn_QueryElement(IBaseQuery _Query, string _Column1, string _ColumnAs)
            : base(_Query)
        {
            CastAsVarcharValueAlias = AliasGenerator.GetNewAlias("CastAsVarchar");
            EntityColumnName = _Column1; 
            Alias = _ColumnAs;
        }

        public override string ToElementString(params object[] _Params)
        {
            string __ColumAs = Alias == "" ? CastAsVarcharValueAlias : Alias;
            return this.Query.Database.Catalogs.DataToolOperationSQLCatalog.CastAsVarchar(EntityColumnName, __ColumAs);


        }

        public string GetColumnName()
        {
            return CastAsVarcharValueAlias;
        }
    }
}
