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
    public class cIsNotNull<TOwnerEntity, TEntity> : cBaseCompareOperator<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        public cIsNotNull(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand)
            : base(_QueryFilterOperand, new object[] { })
        {
        }

        public override string ToElementString(params object[] _Params)
        {
            return QueryFilterOperand.FullName + " IS NOT NULL ";            
        }
    }
}
