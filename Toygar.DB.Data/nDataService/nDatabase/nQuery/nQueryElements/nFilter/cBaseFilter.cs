using Toygar.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nCase;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter
{
    public abstract class cBaseFilter<TOwnerEntity, TEntity> : cBaseQueryElement, IBaseFilterForOperators<TOwnerEntity, TEntity>, IBaseFilterForOperands<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        public cBaseFilter(IBaseQuery _Query)
            : base(_Query)
        {
        }

        public virtual cWhen<TEntity> Then()
        {
            throw new Exception("Bu metod sadece CASE WHEN kullandıktan sonra kullanılabilir.");
        }

        public IQueryFilterOperand<TOwnerEntity, TEntity> Difference(Expression<Func<TEntity, object>> _PropertyExpression, string _Value)
        {
            string __FullName = Query.DefaultAlias;
            if (Query.QueryType.ID != EQueryType.Select.ID)
            {
                __FullName = Query.EntityTable.TableName;
            }
            string __ColumnName = Query.Database.App.Handlers.LambdaHandler.GetParamPropName<TEntity>(_PropertyExpression);
            __FullName += "." + __ColumnName;

            string __Difference = Query.Database.Catalogs.RowOperationSQLCatalog.SQLDifference(__FullName, _Value, true);

            return new cQueryFilterOperand<TOwnerEntity, TEntity>(this, __Difference, true);
        }

        public IQueryFilterOperand<TOwnerEntity, TEntity> Difference<TAlias, TAlias2>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpression, Expression<Func<TAlias2>> _Alias2, Expression<Func<TAlias2, object>> _PropertyExpression2)
            where TAlias : cBaseEntity
            where TAlias2 : cBaseEntity
        {
            string __AliasName = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            string __ColumnName = Query.Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpression);

            string __AliasName2 = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAlias2>(_Alias2);
            string __ColumnName2 = Query.Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpression2);

            string __Difference = Query.Database.Catalogs.RowOperationSQLCatalog.SQLDifference(__AliasName + "." + __ColumnName, __AliasName2 + "." + __ColumnName2, false);

            return new cQueryFilterOperand<TOwnerEntity, TEntity>(this, __Difference, true);
        }
        public IQueryFilterOperand<TOwnerEntity, TEntity> DateDiff<TAlias, TAlias2>(EMssqlDateInterval _Interval, Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpression, Expression<Func<TAlias2>> _Alias2, Expression<Func<TAlias2, object>> _PropertyExpression2)
            where TAlias : cBaseEntity
            where TAlias2 : cBaseEntity
        {
            string __AliasName = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            string __ColumnName = Query.Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpression);

            string __AliasName2 = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAlias2>(_Alias2);
            string __ColumnName2 = Query.Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpression2);




            string __DateDiff = Query.Database.Catalogs.RowOperationSQLCatalog.SQLDateDiff(_Interval.Code, __AliasName + "." + __ColumnName, __AliasName2 + "." + __ColumnName2);

            return new cQueryFilterOperand<TOwnerEntity, TEntity>(this, __DateDiff, true);
        }
        public IQueryFilterOperand<TOwnerEntity, TEntity> DateDiff<TAlias>(EMssqlDateInterval _Interval, Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpression, DateTime _Value)
            where TAlias : cBaseEntity
        {
            string __AliasName = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            string __ColumnName = Query.Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpression);


            string __ParamName = ParameterNameGenerator.GetNewParamName();
            string __DateDiff = Query.Database.Catalogs.RowOperationSQLCatalog.SQLDateDiff(_Interval.Code, __AliasName + "." + __ColumnName, ":" + __ParamName);
            this.Query.Parameters.Add(new cParameter(__ParamName, _Value));

            return new cQueryFilterOperand<TOwnerEntity, TEntity>(this, __DateDiff, true);
        }


        public IQueryFilterOperand<TOwnerEntity, TEntity> Difference<TAlias>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpression, string _Value) where TAlias : cBaseEntity
        {
            string __AliasName = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            string __ColumnName = Query.Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpression);

            string __Difference = Query.Database.Catalogs.RowOperationSQLCatalog.SQLDifference(__AliasName + "." + __ColumnName, _Value, true);

            return new cQueryFilterOperand<TOwnerEntity, TEntity>(this, __Difference, true);
        }


        public virtual IQueryFilterOperand<TOwnerEntity, TEntity> Operand<TAlias>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpression) where TAlias : cBaseEntity
        {
            return new cQueryFilterAliasOperand<TOwnerEntity, TEntity, TAlias>(this, _Alias, _PropertyExpression);
        }

        public virtual IQueryFilterOperand<TOwnerEntity, TEntity> Operand(Expression<Func<TEntity, object>> _PropertyExpression)
        {
            return new cQueryFilterOperand<TOwnerEntity, TEntity>(this, _PropertyExpression);
        }

        public virtual IQueryFilterOperand<TOwnerEntity, TEntity> Operand<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnName) where TAlias : cBaseEntity
        {
            return new cQueryFilterAliasOperand<TOwnerEntity, TEntity, TAlias>(this, _Alias, _ColumnName);
        }


        public virtual IQueryFilterOperand<TOwnerEntity, TEntity> Operand<TAlias, TRelatedEntity>(Expression<Func<TAlias>> _Alias)
            where TAlias : cBaseEntity
            where TRelatedEntity : cBaseEntity
        {
            cEntityTable __EntityTable = Query.Database.EntityManager.GetEntityTableByEnitityType<TRelatedEntity>();
            return new cQueryFilterAliasOperand<TOwnerEntity, TEntity, TAlias>(this, _Alias, __EntityTable.TableForeing_ColumnName_For_InOtherTable);
        }

        public virtual IQueryFilterOperand<TOwnerEntity, TEntity> Operand(string _ColumnName)
        {
            return new cQueryFilterOperand<TOwnerEntity, TEntity>(this, _ColumnName);
        }

        public virtual IQueryFilterOperand<TOwnerEntity, TEntity> Operand()
        {
            return new cQueryFilterOperand<TOwnerEntity, TEntity>(this);
        }

        public virtual IQueryFilterOperand<TOwnerEntity, TEntity> Operand<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            cEntityTable __Table = Query.Database.EntityManager.GetEntityTableByEnitityType<TRelationEntity>();
            return new cQueryFilterOperand<TOwnerEntity, TEntity>(this, __Table.TableForeing_ColumnName_For_InOtherTable);
        }

        public virtual cQuery<TOwnerEntity> ToQuery()
        {
            return ((cQuery<TOwnerEntity>)Query);
        }

        public cBaseFilter<TOwnerEntity, TEntity> UnmanagedCondition(string _Value)
        {
            Query.Filters.Add(new cUnmanagedCondition<TOwnerEntity, TEntity>(this, _Value));
            return this;
        }

        public cBaseFilter<TOwnerEntity, TEntity> Not
        {
            get
            {
                Query.Filters.Add(new cNot<TOwnerEntity, TEntity>(this));
                return this;
            }
        }

        public cBaseFilter<TOwnerEntity, TEntity> And
        {
            get
            {
                Query.Filters.Add(new cAnd<TOwnerEntity, TEntity>(this));
                return this;
            }
        }

        public cBaseFilter<TOwnerEntity, TEntity> Or
        {
            get
            {
                Query.Filters.Add(new cOr<TOwnerEntity, TEntity>(this));
                return this;
            }
        }

        public cBaseFilter<TOwnerEntity, TEntity> PrOpen
        {
            get
            {
                Query.Filters.Add(new cPrOpen<TOwnerEntity, TEntity>(this));
                return this;
            }
        }

        public cBaseFilter<TOwnerEntity, TEntity> PrClose
        {
            get
            {
                Query.Filters.Add(new cPrClose<TOwnerEntity, TEntity>(this));
                return this;
            }
        }

        public override string ToElementString(params object[] _Params)
        {
            return "";
        }

        public cBaseFilter<TOwnerEntity, TEntity> Exists(IQuery _Query)
        {
            cExists<TOwnerEntity, TEntity> __Exists = new cExists<TOwnerEntity, TEntity>(this, _Query);
            Query.Filters.Add(__Exists);
            return this;
        }
    }
}
