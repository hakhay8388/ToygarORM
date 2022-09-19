using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter
{
    public interface IBaseFilterForOperands<TOwnerEntity, TEntity> : IBaseFilterForOperators<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity

    {
        IQueryFilterOperand<TOwnerEntity, TEntity> Operand<TAlias>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpression) where TAlias : cBaseEntity;

        IQueryFilterOperand<TOwnerEntity, TEntity> Operand<TAlias, TRelatedEntity>(Expression<Func<TAlias>> _Alias)
            where TAlias : cBaseEntity
            where TRelatedEntity : cBaseEntity;

        IQueryFilterOperand<TOwnerEntity, TEntity> Operand<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnName) where TAlias : cBaseEntity;

        IQueryFilterOperand<TOwnerEntity, TEntity> Operand(Expression<Func<TEntity, object>> _PropertyExpression);
        IQueryFilterOperand<TOwnerEntity, TEntity> Operand(string _Name);
        IQueryFilterOperand<TOwnerEntity, TEntity> Operand();
        IQueryFilterOperand<TOwnerEntity, TEntity> Operand<TRelationEntity>() where TRelationEntity : cBaseEntity;

        cBaseFilter<TOwnerEntity, TEntity> UnmanagedCondition(string _Value);

        IQueryFilterOperand<TOwnerEntity, TEntity> Difference(Expression<Func<TEntity, object>> _PropertyExpression, string _Value);

        IQueryFilterOperand<TOwnerEntity, TEntity> Difference<TAlias, TAlias2>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpression, Expression<Func<TAlias2>> _Alias2, Expression<Func<TAlias2, object>> _PropertyExpression2) where TAlias : cBaseEntity where TAlias2 : cBaseEntity;

        IQueryFilterOperand<TOwnerEntity, TEntity> Difference<TAlias>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpression, string _Value) where TAlias : cBaseEntity;
 
        IQueryFilterOperand<TOwnerEntity, TEntity> DateDiff<TAlias>(EMssqlDateInterval _Interval, Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpression, DateTime _Value) where TAlias : cBaseEntity;
        IQueryFilterOperand<TOwnerEntity, TEntity> DateDiff<TAlias, TAlias2>(EMssqlDateInterval _Interval, Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpression, Expression<Func<TAlias2>> _Alias2, Expression<Func<TAlias2, object>> _PropertyExpression2) where TAlias : cBaseEntity where TAlias2 : cBaseEntity;


        /*cBaseFilter<TOwnerEntity, TEntity> Exists(IQuery _Query);

        cBaseFilter<TOwnerEntity, TEntity> PrOpen { get; }
        cBaseFilter<TOwnerEntity, TEntity> PrClose { get; }*/
    }
}
