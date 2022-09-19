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
    public class cThen<TOwnerEntity, TEntity> : cBaseCompareOperator<TOwnerEntity, TEntity>
           where TOwnerEntity : cBaseEntity
           where TEntity : cBaseEntity
    {
        public cThen(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand)
           : base(_QueryFilterOperand, new object[] { })
        {
        }

        public cThen(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, object _Value)
            : base(_QueryFilterOperand, _Value)
        {
        }

        public cThen(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, IQuery _Value)
            : base(_QueryFilterOperand, _Value)
        {
        }

        public cThen(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Expression<Func<object>> _PropertyExpression)
            : base(_QueryFilterOperand, _PropertyExpression)
        {
        }

        public cThen(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Type _Alias, Expression<Func<object>> _PropertyExpression)
           : base(_QueryFilterOperand, _Alias, _PropertyExpression)
        {
        }

        public cThen(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Type _AliaslessEntityType, Type _ReleatedEntityType)
         : base(_QueryFilterOperand, _AliaslessEntityType, _ReleatedEntityType)
        {
        }

        public override string ToElementString(params object[] _Params)
        {
            if (IsConstValue)
            {
                return " THEN :" + Parameters[0].ParamName;
            }
            else
            {
                return " THEN " + Parameters[0].ParamName;
            }
        }
    }
}
