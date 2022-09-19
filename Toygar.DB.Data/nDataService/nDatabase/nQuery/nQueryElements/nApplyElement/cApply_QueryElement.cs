using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nApply;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nApplyElement
{
    public class cApply_QueryElement<TEntity> : cBaseQueryElement
        where TEntity : cBaseEntity
    {
        public IApply Apply { get; set; }
        public cApply_QueryElement(IBaseQuery _Query, IApply _Apply)
            : base(_Query)
        {
            Apply = _Apply;
        }

        public override string ToElementString(params object[] _Params)
        {
            string __Result = " " + Apply.ToSql().FullSQLString;
            Query.Parameters = Query.Parameters.Union(Apply.Parameters).ToList();
            return __Result;
        }
    }
}
