using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators
{
    public class cUnmanagedCondition<TOwnerEntity, TEntity> : cBaseFilterElement<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        public string Value { get; set; }
        public cUnmanagedCondition(IQueryElement _QueryElement, string _Value)
            : base(_QueryElement.Query)
        {
            Value = _Value;
        }

        public override string ToElementString(params object[] _Params)
        {
            return Value;
        }
    }
}
