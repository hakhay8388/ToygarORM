using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nSourceQueryElement
{
    public class cSubQuerySource_QueryElement<TEntity> : cBaseQueryElement where TEntity : cBaseEntity
    {
        public IQuery SubQuery { get; set; }
        public cSubQuerySource_QueryElement(IBaseQuery _Query, IQuery _SubQuery)
            : base(_Query)
        {
            SubQuery = _SubQuery;
        }

        public override string ToElementString(params object[] _Params)
        {
            string __Result = "(" + SubQuery.ToSql().FullSQLString + ") " + SubQuery.DefaultExternalAlias;
            Query.Parameters = Query.Parameters.Union(SubQuery.Parameters).ToList();
            return __Result;
        }
    }
}
