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
    public class cNotEqAny<TOwnerEntity, TEntity> : cBaseCompareOperator<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        public cNotEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand)
            : base(_QueryFilterOperand, new object[] { })
        {
        }

        public cNotEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, params object[] _Value)
            : base(_QueryFilterOperand, _Value)
        {
        }

        public cNotEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, IQuery _Value)
            : base(_QueryFilterOperand, _Value)
        {
        }

        public cNotEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, params Expression<Func<object>>[] _PropertyExpression)
            : base(_QueryFilterOperand, _PropertyExpression)
        {
        }

        public cNotEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Type _Alias, Expression<Func<object>> _PropertyExpression)
      : base(_QueryFilterOperand, _Alias, _PropertyExpression)
        {
        }

        public cNotEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Type _AliaslessEntityType, Type _ReleatedEntityType)
         : base(_QueryFilterOperand, _AliaslessEntityType, _ReleatedEntityType)
        {
        }

        public override string ToElementString(params object[] _Params)
        {
            if (IsConstValue)
            {
                string __Items = "";
                foreach (var __Item in Parameters)
                {
                    if (!string.IsNullOrEmpty(__Items)) __Items += ", ";
                    __Items += ":" + __Item.ParamName;
                }
                return QueryFilterOperand.FullName + " NOT IN ( " + __Items + ") ";
            }
            else
            {
                string __Items = "";
                foreach (var __Item in Parameters)
                {
                    if (!string.IsNullOrEmpty(__Items)) __Items += " , ";
                    __Items += "(" + __Item.ParamName + ")";
                }
                return QueryFilterOperand.FullName + " NOT IN ( " + __Items + ") ";
            }
        }
    }
}
