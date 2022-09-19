using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nCase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nCaseWhenElement
{
    public class cCaseWhen_QueryElement<TEntity> : cBaseQueryElement, ISelectableColumn where TEntity : cBaseEntity
    {
        cCase<TEntity> Case { get; set; }

        public cCaseWhen_QueryElement(IBaseQuery _Query, cCase<TEntity> _Case)
            : base(_Query)
        {
            Case = _Case;
        }

        public override string ToElementString(params object[] _Params)
        {
            string __Result = " " + Case.ToSql().FullSQLString;
            Query.Parameters = Query.Parameters.Union(Case.OwnerQuery.Parameters).ToList();
            return __Result;
        }

        public string GetColumnName()
        {
            return Case.ColumnName;
        }
    }
}
