using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements
{
    public class cQueryFilterOperand<TOwnerEntity, TEntity> : cBaseFilterElement<TOwnerEntity, TEntity>, IQueryFilterOperand<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        public string ColumnName { get; set; }
        public string FullName { get; set; }
        public cBaseFilter<TOwnerEntity, TEntity> Filter { get; set; }
        public cQueryFilterOperand(cBaseFilter<TOwnerEntity, TEntity> _Filter, Expression<Func<TEntity, object>> _PropertyExpression)
            : base(_Filter.Query)
        {
            Filter = _Filter;
            FullName = Filter.Query.DefaultAlias;
            if (Query.QueryType.ID != EQueryType.Select.ID)
            {
                FullName = Query.EntityTable.TableName;
            }
            ColumnName = Filter.Query.Database.App.Handlers.LambdaHandler.GetParamPropName<TEntity>(_PropertyExpression);
            FullName += "." + ColumnName;
        }

        public cQueryFilterOperand(cBaseFilter<TOwnerEntity, TEntity> _Filter, Expression<Func<TEntity>> _Alias, Expression<Func<TEntity, object>> _PropertyExpression)
           : base(_Filter.Query)
        {
            string __AliasName = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_Alias);
            string __ColumnName = Query.Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpression);

            Filter = _Filter;
            FullName = __AliasName;
            if (Query.QueryType.ID != EQueryType.Select.ID)
            {
                FullName = Query.EntityTable.TableName;
            }
            ColumnName = __ColumnName;
            FullName += "." + ColumnName;
        }

        public cQueryFilterOperand(cBaseFilter<TOwnerEntity, TEntity> _Filter, string _Name, bool _UseNameDirect = false)
            : base(_Filter.Query)
        {
            Filter = _Filter;

			if (_UseNameDirect)
			{
				FullName = _Name;
			}
			else
			{
				FullName = Filter.Query.DefaultAlias;
				if (Query.QueryType.ID != EQueryType.Select.ID)
				{
					FullName = Query.EntityTable.TableName;
				}
				ColumnName = _Name;
				FullName += "." + ColumnName;
			}
        }

        public cQueryFilterOperand(cBaseFilter<TOwnerEntity, TEntity> _Filter)
            : base(_Filter.Query)
        {
            Filter = _Filter;
            FullName = "";
        }


		//************************************************************
		//**********BASE  CODE***************
		//*************************************************************
		private IBaseFilterForOperators<TOwnerEntity, TEntity> Operation<T>()
        {
            Type __GenericType = typeof(T);
            ConstructorInfo __Info = __GenericType.GetConstructor(new Type[] { typeof(cQueryFilterOperand<TOwnerEntity, TEntity>) });
            object __Result = __Info.Invoke(new object[] { this });
            Query.Filters.Add((cBaseCompareOperator<TOwnerEntity, TEntity>)__Result);
            return Filter;
        }

        private IBaseFilterForOperators<TOwnerEntity, TEntity> Operation<T>(object _Value)
        {
            Type __GenericType = typeof(T);
            ConstructorInfo __Info = __GenericType.GetConstructor(new Type[] { typeof(cQueryFilterOperand<TOwnerEntity, TEntity>), typeof(object) });
            object __Result = __Info.Invoke(new object[] { this, _Value });
            Query.Filters.Add((cBaseCompareOperator<TOwnerEntity, TEntity>)__Result);
            return Filter;
        }

        private IBaseFilterForOperators<TOwnerEntity, TEntity> Operation<T>(IQuery _Query)
        {
            Type __GenericType = typeof(T);
            ConstructorInfo __Info = __GenericType.GetConstructor(new Type[] { typeof(cQueryFilterOperand<TOwnerEntity, TEntity>), typeof(IQuery) });
            object __Result = __Info.Invoke(new object[] { this, _Query });
            Query.Filters.Add((cBaseCompareOperator<TOwnerEntity, TEntity>)__Result);
            return Filter;
        }

        private IBaseFilterForOperators<TOwnerEntity, TEntity> Operation<T>(Expression<Func<object>> _Alias)
        {
            Type __GenericType = typeof(T);
            ConstructorInfo __Info = __GenericType.GetConstructor(new Type[] { typeof(cQueryFilterOperand<TOwnerEntity, TEntity>), typeof(Expression<Func<object>>) });
            object __Result = __Info.Invoke(new object[] { this, _Alias });
            Query.Filters.Add((cBaseCompareOperator<TOwnerEntity, TEntity>)__Result);
            return Filter;
        }

        private IBaseFilterForOperators<TOwnerEntity, TEntity> Operation<T, T2>(Expression<Func<object>> _Alias) where T2 : cBaseEntity
        {
            Type __GenericType = typeof(T);
            ConstructorInfo __Info = __GenericType.GetConstructor(new Type[] { typeof(cQueryFilterOperand<TOwnerEntity, TEntity>), typeof(Type), typeof(Expression<Func<object>>) });
            object __Result = __Info.Invoke(new object[] { this, typeof(T2), _Alias });
            Query.Filters.Add((cBaseCompareOperator<TOwnerEntity, TEntity>)__Result);
            return Filter;
        }

        private IBaseFilterForOperators<TOwnerEntity, TEntity> Operation<T, TAliaslessEntity, TRelationEntity>()
            where TAliaslessEntity : cBaseEntity
            where TRelationEntity : cBaseEntity
        {
            Type __GenericType = typeof(T);
            ConstructorInfo __Info = __GenericType.GetConstructor(new Type[] { typeof(cQueryFilterOperand<TOwnerEntity, TEntity>), typeof(Type), typeof(Type) });
            object __Result = __Info.Invoke(new object[] { this, typeof(TAliaslessEntity), typeof(TRelationEntity) });
            Query.Filters.Add((cBaseCompareOperator<TOwnerEntity, TEntity>)__Result);
            return Filter;
        }

        private IBaseFilterForOperators<TOwnerEntity, TEntity> Operation<T>(params object[] _Value)
        {
            Type __GenericType = typeof(T);
            ConstructorInfo __Info = __GenericType.GetConstructor(new Type[] { typeof(cQueryFilterOperand<TOwnerEntity, TEntity>), typeof(object[]) });
            object __Result = __Info.Invoke(new object[] { this, _Value });
            Query.Filters.Add((cBaseCompareOperator<TOwnerEntity, TEntity>)__Result);
            return Filter;
        }

        private IBaseFilterForOperators<TOwnerEntity, TEntity> Operation<T>(params Expression<Func<object>>[] _Alias)
        {
            Type __GenericType = typeof(T);
            ConstructorInfo __Info = __GenericType.GetConstructor(new Type[] { typeof(cQueryFilterOperand<TOwnerEntity, TEntity>), typeof(Expression<Func<object>>[]) });
            object __Result = __Info.Invoke(new object[] { this, _Alias });
            Query.Filters.Add((cBaseCompareOperator<TOwnerEntity, TEntity>)__Result);
            return Filter;
        }



        //************************************************************
        //************************************************************

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Eq(object _Value)
        {
            return Operation<cEq<TOwnerEntity, TEntity>>(_Value);
            /*cBaseCompareOperator<TEntity> __Result = new cEq<TEntity>(this, _Value);
            Query.Filters.Add((cBaseCompareOperator<TEntity>)__Result);
            return Filter;*/
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Eq(IQuery _Query)
        {
            return Operation<cEq<TOwnerEntity, TEntity>>(_Query);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Eq(Expression<Func<object>> _Alias)
        {
            return Operation<cEq<TOwnerEntity, TEntity>>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Eq<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity
        {
            return Operation<cEq<TOwnerEntity, TEntity>, TRelationEntity>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Eq<TAliaslessEntity, TRelationEntity>()
          where TAliaslessEntity : cBaseEntity
          where TRelationEntity : cBaseEntity
        {
            return Operation<cEq<TOwnerEntity, TEntity>, TAliaslessEntity, TRelationEntity>();
        }

        //************************************************************
        //************************************************************
        public IBaseFilterForOperators<TOwnerEntity, TEntity> EqAny(params object[] _Value)
        {
            return Operation<cEqAny<TOwnerEntity, TEntity>>(_Value);
        }

        /*public IBaseFilterForOperators<TOwnerEntity, TEntity> EqAny(params IQuery[] _Query)
        {
            return Operation<cEqAny<TOwnerEntity, TEntity>>(_Query);
        }*/

        public IBaseFilterForOperators<TOwnerEntity, TEntity> EqAny(IQuery _Query)
        {
            return Operation<cEqAny<TOwnerEntity, TEntity>>(_Query);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> EqAny(params Expression<Func<object>>[] _Alias)
        {
            return Operation<cEqAny<TOwnerEntity, TEntity>>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> EqAny<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity
        {
            return Operation<cEqAny<TOwnerEntity, TEntity>, TRelationEntity>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> EqAny<TAliaslessEntity, TRelationEntity>()
         where TAliaslessEntity : cBaseEntity
         where TRelationEntity : cBaseEntity
        {
            return Operation<cEqAny<TOwnerEntity, TEntity>, TAliaslessEntity, TRelationEntity>();
        }

        //************************************************************
        //************************************************************
        public IBaseFilterForOperators<TOwnerEntity, TEntity> NotEq(object _Value)
        {
            return Operation<cNotEq<TOwnerEntity, TEntity>>(_Value);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> NotEq(IQuery _Query)
        {
            return Operation<cNotEq<TOwnerEntity, TEntity>>(_Query);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> NotEq(Expression<Func<object>> _Alias)
        {
            return Operation<cNotEq<TOwnerEntity, TEntity>>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> NotEq<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity
        {
            return Operation<cNotEq<TOwnerEntity, TEntity>, TRelationEntity>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> NotEq<TAliaslessEntity, TRelationEntity>()
        where TAliaslessEntity : cBaseEntity
        where TRelationEntity : cBaseEntity
        {
            return Operation<cNotEq<TOwnerEntity, TEntity>, TAliaslessEntity, TRelationEntity>();
        }

        //************************************************************
        //************************************************************
        public IBaseFilterForOperators<TOwnerEntity, TEntity> NotEqAny(params object[] _Value)
        {
            return Operation<cNotEqAny<TOwnerEntity, TEntity>>(_Value);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> NotEqAny(IQuery _Query)
        {
            return Operation<cNotEqAny<TOwnerEntity, TEntity>>(_Query);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> NotEqAny(params Expression<Func<object>>[] _Alias)
        {
            return Operation<cNotEqAny<TOwnerEntity, TEntity>>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> NotEqAny<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity
        {
            return Operation<cNotEqAny<TOwnerEntity, TEntity>, TRelationEntity>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> NotEqAny<TAliaslessEntity, TRelationEntity>()
              where TAliaslessEntity : cBaseEntity
              where TRelationEntity : cBaseEntity
        {
            return Operation<cNotEqAny<TOwnerEntity, TEntity>, TAliaslessEntity, TRelationEntity>();
        }

        //************************************************************
        //************************************************************
        public IBaseFilterForOperators<TOwnerEntity, TEntity> Like(object _Value)
        {
            return Operation<cLike<TOwnerEntity, TEntity>>(_Value);
        }
        //************************************************************
        //************************************************************
        public IBaseFilterForOperators<TOwnerEntity, TEntity> Gt(object _Value)
        {
            return Operation<cGt<TOwnerEntity, TEntity>>(_Value);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Gt(IQuery _Query)
        {
            return Operation<cGt<TOwnerEntity, TEntity>>(_Query);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Gt(Expression<Func<object>> _Alias)
        {
            return Operation<cGt<TOwnerEntity, TEntity>>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Gt<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity
        {
            return Operation<cGt<TOwnerEntity, TEntity>, TRelationEntity>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Gt<TAliaslessEntity, TRelationEntity>()
            where TAliaslessEntity : cBaseEntity
            where TRelationEntity : cBaseEntity
        {
            return Operation<cGt<TOwnerEntity, TEntity>, TAliaslessEntity, TRelationEntity>();
        }

        //************************************************************
        //************************************************************
        public IBaseFilterForOperators<TOwnerEntity, TEntity> Ge(object _Value)
        {
            return Operation<cGe<TOwnerEntity, TEntity>>(_Value);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Ge(IQuery _Query)
        {
            return Operation<cGe<TOwnerEntity, TEntity>>(_Query);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Ge(Expression<Func<object>> _Alias)
        {
            return Operation<cGe<TOwnerEntity, TEntity>>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Ge<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity
        {
            return Operation<cGe<TOwnerEntity, TEntity>, TRelationEntity>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Ge<TAliaslessEntity, TRelationEntity>()
          where TAliaslessEntity : cBaseEntity
          where TRelationEntity : cBaseEntity
        {
            return Operation<cGe<TOwnerEntity, TEntity>, TAliaslessEntity, TRelationEntity>();
        }

        //************************************************************
        //************************************************************
        public IBaseFilterForOperators<TOwnerEntity, TEntity> Lt(object _Value)
        {
            return Operation<cLt<TOwnerEntity, TEntity>>(_Value);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Lt(IQuery _Query)
        {
            return Operation<cLt<TOwnerEntity, TEntity>>(_Query);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Lt(Expression<Func<object>> _Alias)
        {
            return Operation<cLt<TOwnerEntity, TEntity>>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Lt<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity
        {
            return Operation<cLt<TOwnerEntity, TEntity>, TRelationEntity>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Lt<TAliaslessEntity, TRelationEntity>()
        where TAliaslessEntity : cBaseEntity
        where TRelationEntity : cBaseEntity
        {
            return Operation<cLt<TOwnerEntity, TEntity>, TAliaslessEntity, TRelationEntity>();
        }

        //************************************************************
        //************************************************************
        public IBaseFilterForOperators<TOwnerEntity, TEntity> Le(object _Value)
        {
            return Operation<cLe<TOwnerEntity, TEntity>>(_Value);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Le(IQuery _Query)
        {
            return Operation<cLe<TOwnerEntity, TEntity>>(_Query);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Le(Expression<Func<object>> _Alias)
        {
            return Operation<cLe<TOwnerEntity, TEntity>>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Le<TRelationEntity>(Expression<Func<object>> _Alias) where TRelationEntity : cBaseEntity
        {
            return Operation<cLe<TOwnerEntity, TEntity>, TRelationEntity>(_Alias);
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Le<TAliaslessEntity, TRelationEntity>()
               where TAliaslessEntity : cBaseEntity
               where TRelationEntity : cBaseEntity
        {
            return Operation<cLe<TOwnerEntity, TEntity>, TAliaslessEntity, TRelationEntity>();
        }

        //************************************************************
        //************************************************************
        public IBaseFilterForOperators<TOwnerEntity, TEntity> IsNull()
        {
            return Operation<cIsNull<TOwnerEntity, TEntity>>();
        }
        //************************************************************
        //************************************************************
        public IBaseFilterForOperators<TOwnerEntity, TEntity> IsNotNull()
        {
            return Operation<cIsNotNull<TOwnerEntity, TEntity>>();
        }
        //************************************************************
        //************************************************************
        public IBaseFilterForOperators<TOwnerEntity, TEntity> Between(object _Value, object _Value2)
        {
            new cBetween<TOwnerEntity, TEntity>(this, _Value, _Value2);
            return Filter;
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Between(object _Value, IQuery _Value2)
        {
            new cBetween<TOwnerEntity, TEntity>(this, _Value, _Value2);
            return Filter;
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Between(object _Value, Expression<Func<object>> _Value2)
        {
            new cBetween<TOwnerEntity, TEntity>(this, _Value, _Value2);
            return Filter;
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Between(IQuery _Value, object _Value2)
        {
            new cBetween<TOwnerEntity, TEntity>(this, _Value, _Value2);
            return Filter;
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Between(IQuery _Value, IQuery _Value2)
        {
            new cBetween<TOwnerEntity, TEntity>(this, _Value, _Value2);
            return Filter;
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Between(IQuery _Value, Expression<Func<object>> _Value2)
        {
            new cBetween<TOwnerEntity, TEntity>(this, _Value, _Value2);
            return Filter;
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Between(Expression<Func<object>> _Value, object _Value2)
        {
            new cBetween<TOwnerEntity, TEntity>(this, _Value, _Value2);
            return Filter;
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Between(Expression<Func<object>> _Value, IQuery _Value2)
        {
            new cBetween<TOwnerEntity, TEntity>(this, _Value, _Value2);
            return Filter;
        }

        public IBaseFilterForOperators<TOwnerEntity, TEntity> Between(Expression<Func<object>> _Value, Expression<Func<object>> _Value2)
        {
            new cBetween<TOwnerEntity, TEntity>(this, _Value, _Value2);
            return Filter;
        }
        //************************************************************
        //************************************************************
        public override string ToElementString(params object[] _Params)
        {
            return FullName;
        }
    }
}
