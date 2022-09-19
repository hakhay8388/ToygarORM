using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nGroupByQueryElement
{
    public class cGroupBy_QueryElement<TEntity, TAlias> : cBaseQueryElement
        where TEntity : cBaseEntity
        where TAlias : cBaseEntity
    {
        public cGroupBy<TEntity, TAlias> GroupBy { get; set; }
        public cGroupBy_QueryElement(IQuery _Query, cGroupBy<TEntity, TAlias> _GroupBy)
            : base(_Query)
        {
            GroupBy = _GroupBy;
        }

        public override string ToElementString(params object[] _Params)
        {
            string __Result = GroupBy.ToSql().FullSQLString;
            Query.Parameters = Query.Parameters.Union(GroupBy.Parameters).ToList();
            return __Result;
        }
    }
}
