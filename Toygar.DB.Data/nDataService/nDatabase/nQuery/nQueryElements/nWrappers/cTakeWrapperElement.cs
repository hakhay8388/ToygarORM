using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nTake;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nWrappers
{
    public class cTakeWrapperElement<TEntity> : cBaseQueryElement
        where TEntity : cBaseEntity
    {
        public cTake<TEntity> Take { get; set; }
        public cTakeWrapperElement(IQuery _Query, cTake<TEntity> _Take)
            : base(_Query)
        {
            Take = _Take;
        }

        public override string ToElementString(params object[] _Params)
        {
            return Take.Wrap((cSql)_Params[0]);
        }
    }
}
