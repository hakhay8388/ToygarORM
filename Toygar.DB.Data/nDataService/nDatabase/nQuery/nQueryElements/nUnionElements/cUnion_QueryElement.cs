using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nUnionElements
{
    public class cUnion_QueryElement<TEntity> : cBaseQueryElement where TEntity : cBaseEntity
    {
        public IQuery UnionQuery { get; set; }
        public cUnion_QueryElement(cBaseQuery<TEntity> _Query, IQuery _UnionQuery)
            : base(_Query)
        {
            UnionQuery = _UnionQuery;
        }

        public override string ToElementString(params object[] _Params)
        {
            string __Result = UnionQuery.ToSql().FullSQLString + " \n UNION \n ";
            Query.Parameters = Query.Parameters.Union(UnionQuery.Parameters).ToList();
            return __Result;
        }
    }
}
