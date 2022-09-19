using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy.nHaving.nOperators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy.nHaving
{
    public interface IHaving<TOwnerEntity, TEntity, TAlias>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
        where TAlias : cBaseEntity
    {
        IQueryFilterOperand<TOwnerEntity, TEntity> Avg(Expression<Func<TEntity, object>> _PropertyExpression);
        IQueryFilterOperand<TOwnerEntity, TEntity> Avg(string _Name);
        IQueryFilterOperand<TOwnerEntity, TEntity> Avg();
        IQueryFilterOperand<TOwnerEntity, TEntity> Avg<TRelationEntity>() where TRelationEntity : cBaseEntity;
        IQueryFilterOperand<TOwnerEntity, TEntity> Count(Expression<Func<TEntity, object>> _PropertyExpression);
        IQueryFilterOperand<TOwnerEntity, TEntity> Count(string _Name);
        IQueryFilterOperand<TOwnerEntity, TEntity> Count();
        IQueryFilterOperand<TOwnerEntity, TEntity> Count<TRelationEntity>() where TRelationEntity : cBaseEntity;
    }
}
