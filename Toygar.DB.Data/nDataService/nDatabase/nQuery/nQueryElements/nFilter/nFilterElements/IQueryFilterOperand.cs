using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements
{
    public interface IQueryFilterOperand<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        IBaseFilterForOperators<TOwnerEntity, TEntity> Eq(object _Value);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Eq(IQuery _Query);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Eq(Expression<Func<object>> _Alias);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Eq<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity;
        IBaseFilterForOperators<TOwnerEntity, TEntity> Eq<TAliaslessEntity, TRelationEntity>() 
            where TAliaslessEntity : cBaseEntity
            where TRelationEntity : cBaseEntity;

        IBaseFilterForOperators<TOwnerEntity, TEntity> EqAny(params object[] _Value);
        IBaseFilterForOperators<TOwnerEntity, TEntity> EqAny(IQuery _Query);
        IBaseFilterForOperators<TOwnerEntity, TEntity> EqAny(params Expression<Func<object>>[] _Alias);
        IBaseFilterForOperators<TOwnerEntity, TEntity> EqAny<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity;
        IBaseFilterForOperators<TOwnerEntity, TEntity> EqAny<TAliaslessEntity, TRelationEntity>()
            where TAliaslessEntity : cBaseEntity
            where TRelationEntity : cBaseEntity;


        IBaseFilterForOperators<TOwnerEntity, TEntity> NotEq(object _Value);
        IBaseFilterForOperators<TOwnerEntity, TEntity> NotEq(IQuery _Query);
        IBaseFilterForOperators<TOwnerEntity, TEntity> NotEq(Expression<Func<object>> _Alias);
        IBaseFilterForOperators<TOwnerEntity, TEntity> NotEq<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity;
        IBaseFilterForOperators<TOwnerEntity, TEntity> NotEq<TAliaslessEntity, TRelationEntity>()
            where TAliaslessEntity : cBaseEntity
            where TRelationEntity : cBaseEntity;




        IBaseFilterForOperators<TOwnerEntity, TEntity> NotEqAny(params object[] _Value);
        IBaseFilterForOperators<TOwnerEntity, TEntity> NotEqAny(IQuery _Query);
        IBaseFilterForOperators<TOwnerEntity, TEntity> NotEqAny(params Expression<Func<object>>[] _Alias);
        IBaseFilterForOperators<TOwnerEntity, TEntity> NotEqAny<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity;
        IBaseFilterForOperators<TOwnerEntity, TEntity> NotEqAny<TAliaslessEntity, TRelationEntity>()
           where TAliaslessEntity : cBaseEntity
           where TRelationEntity : cBaseEntity;



        IBaseFilterForOperators<TOwnerEntity, TEntity> Like(object _Value);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Gt(object _Value);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Gt(IQuery _Query);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Gt(Expression<Func<object>> _Alias);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Gt<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity;
        IBaseFilterForOperators<TOwnerEntity, TEntity> Gt<TAliaslessEntity, TRelationEntity>()
               where TAliaslessEntity : cBaseEntity
               where TRelationEntity : cBaseEntity;


        IBaseFilterForOperators<TOwnerEntity, TEntity> Ge(object _Value);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Ge(IQuery _Query);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Ge(Expression<Func<object>> _Alias);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Ge<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity;
        IBaseFilterForOperators<TOwnerEntity, TEntity> Ge<TAliaslessEntity, TRelationEntity>()
               where TAliaslessEntity : cBaseEntity
               where TRelationEntity : cBaseEntity;


        IBaseFilterForOperators<TOwnerEntity, TEntity> Lt(object _Value);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Lt(IQuery _Query);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Lt(Expression<Func<object>> _Alias);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Lt<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity;
        IBaseFilterForOperators<TOwnerEntity, TEntity> Lt<TAliaslessEntity, TRelationEntity>()
               where TAliaslessEntity : cBaseEntity
               where TRelationEntity : cBaseEntity;



        IBaseFilterForOperators<TOwnerEntity, TEntity> Le(object _Value);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Le(IQuery _Query);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Le(Expression<Func<object>> _Alias);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Le<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity;
        IBaseFilterForOperators<TOwnerEntity, TEntity> Le<TAliaslessEntity, TRelationEntity>()
               where TAliaslessEntity : cBaseEntity
               where TRelationEntity : cBaseEntity;


        IBaseFilterForOperators<TOwnerEntity, TEntity> IsNull();
        IBaseFilterForOperators<TOwnerEntity, TEntity> IsNotNull();
        IBaseFilterForOperators<TOwnerEntity, TEntity> Between(object _Value, object _Value2);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Between(object _Value, IQuery _Value2);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Between(object _Value, Expression<Func<object>> _Value2);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Between(IQuery _Value, object _Value2);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Between(IQuery _Value, IQuery _Value2);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Between(IQuery _Value, Expression<Func<object>> _Value2);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Between(Expression<Func<object>> _Value, object _Value2);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Between(Expression<Func<object>> _Value, IQuery _Value2);
        IBaseFilterForOperators<TOwnerEntity, TEntity> Between(Expression<Func<object>> _Value, Expression<Func<object>> _Value2);
    }
}
