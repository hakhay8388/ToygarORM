using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators
{
    public class cAnd<TOwnerEntity, TEntity> : cBaseFilterElement<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        public IQueryElement QueryElement { get; set; }
        public cAnd(IQueryElement _QueryElement)
            : base(_QueryElement.Query)
        {
            QueryElement = _QueryElement;
        }

        public override string ToElementString(params object[] _Params)
        {
            return " AND ";
        }
    }
}
