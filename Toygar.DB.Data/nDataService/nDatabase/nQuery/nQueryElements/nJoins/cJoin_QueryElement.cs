using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nJoins
{
    public class cJoin_QueryElement<TEntity, TJoin> : cBaseQueryElement
        where TEntity : cBaseEntity
        where TJoin : cBaseEntity
    {
        public IJoin Join { get; set; }
        public cJoin_QueryElement(IBaseQuery _Query, IJoin _Join)
            : base(_Query)
        {
            Join = _Join;
        }

        public override string ToElementString(params object[] _Params)
        {
            string __Result = " " + Join.ToSql().FullSQLString;
            Query.Parameters = Query.Parameters.Union(Join.Parameters).ToList();
            return __Result;
        }
    }
}
