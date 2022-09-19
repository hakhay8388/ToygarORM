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
    public class cIfIsNullValueColumn_QueryElement<TEntity> : cBaseQueryElement, ISelectableColumn where TEntity : cBaseEntity
    {
        public string IfIsNullValueAlias { get; set; }
        public string EntityColumnName { get; set; }
        public string EntityColumnName2 { get; set; }
        public string Alias { get; set; }
        public cIfIsNullValueColumn_QueryElement(IBaseQuery _Query, string _Column1, string _Column2, string _ColumnAs)
            : base(_Query)
        {
            IfIsNullValueAlias = AliasGenerator.GetNewAlias("IsNullValue");
            EntityColumnName = _Column1;
            EntityColumnName2 = _Column2;
            Alias = _ColumnAs;
        }

        public override string ToElementString(params object[] _Params)
        {
            string __ColumAs = Alias == "" ? IfIsNullValueAlias : Alias;
            return this.Query.Database.Catalogs.DataToolOperationSQLCatalog.IfIsNull(EntityColumnName, EntityColumnName2, __ColumAs);


        }

        public string GetColumnName()
        {
            return IfIsNullValueAlias;
        }
    }
}
