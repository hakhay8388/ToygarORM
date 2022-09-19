using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators
{

    public abstract class cBaseCompareOperator<TOwnerEntity, TEntity> : cBaseFilterElement<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        public cQueryFilterOperand<TOwnerEntity, TEntity> QueryFilterOperand { get; set; }
        protected List<cParameter> Parameters { get; set; }
        public bool IsConstValue { get; private set; }

        public bool IsPlacedInParentheses { get; private set; }

        protected cBaseCompareOperator(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, params object[] _Values)
            : base(_QueryFilterOperand.Query)
        {
            Parameters = new List<cParameter>();
            IsConstValue = true;
            IsPlacedInParentheses = false;
            QueryFilterOperand = _QueryFilterOperand;
            foreach (var __ValueItem in _Values)
            {
                if (typeof(cBaseEntity).IsAssignableFrom(__ValueItem.GetType()))
                {
                    string __ParamName = ParameterNameGenerator.GetNewParamName();
                    Parameters.Add(new cParameter(__ParamName, ((cBaseEntity)__ValueItem).ID));
                }
                else
                {
                    if (typeof(IList).IsAssignableFrom(__ValueItem.GetType()))
                    {
                        foreach (var __InnerItem in  (IList)__ValueItem)
                        {
                            string __ParamName = ParameterNameGenerator.GetNewParamName();
                            Parameters.Add(new cParameter(__ParamName, __InnerItem));
                        }
                    }
                    else
                    {
                        string __ParamName = ParameterNameGenerator.GetNewParamName();
                        Parameters.Add(new cParameter(__ParamName, __ValueItem));
                    }                    
                }

            }
            InitParametersToQuery();
        }

        protected cBaseCompareOperator(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, IQuery _Value)
            : base(_QueryFilterOperand.Query)
        {
            Parameters = new List<cParameter>();
            IsConstValue = false;
            IsPlacedInParentheses = true;
            QueryFilterOperand = _QueryFilterOperand;
            Parameters.Add(new cParameter("(" + _Value.ToSql().FullSQLString + ")", ""));
            Query.Parameters = Query.Parameters.Union(_Value.Parameters).ToList();
        }

        public cBaseCompareOperator(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Type _AliasType, params Expression<Func<object>>[] _PropertyExpression)
               : base(_QueryFilterOperand.Query)
        {
            Parameters = new List<cParameter>();
            IsConstValue = false;
            IsPlacedInParentheses = false;
            QueryFilterOperand = _QueryFilterOperand;

            foreach (var __ValueItem in _PropertyExpression)
            {
                string __ParamName = "";
                Type __ObjectType = Query.Database.App.Handlers.LambdaHandler.GetObjectPropType(__ValueItem);
                if (typeof(cBaseEntity).IsAssignableFrom(__ObjectType))
                {
                    __ParamName = Query.Database.App.Handlers.LambdaHandler.GetObjectName(__ValueItem);
                    string __ColumnName = Query.Database.EntityManager.GetEntityTableByEnitityType(_AliasType).TableForeing_ColumnName_For_InOtherTable;
                    cEntityTable __Table = Query.Database.EntityManager.GetEntityTableByEnitityType(__ObjectType);
                    if (__Table.EntityFieldList.Where(__Item => __Item.ColumnName == __ColumnName).ToList().Count > 0)
                    {
                        __ParamName += "." + __ColumnName;
                    }
                    else
                    {
                        throw new Exception(__Table.TableName + "." + __ColumnName + " Kolonu bulunamadı..!");
                    }

                }
                else
                {
                    __ParamName = Query.Database.App.Handlers.LambdaHandler.GetObjectNameAndPropName(__ValueItem);
                }
                Parameters.Add(new cParameter(__ParamName, ""));
            }
        }

        public cBaseCompareOperator(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Type _AliaslessEntity, Type _RelatedEntity)
              : base(_QueryFilterOperand.Query)
        {
            Parameters = new List<cParameter>();
            IsConstValue = false;
            IsPlacedInParentheses = false;
            QueryFilterOperand = _QueryFilterOperand;

            string __ParamName = "";
            if (typeof(cBaseEntity).IsAssignableFrom(_AliaslessEntity) && typeof(cBaseEntity).IsAssignableFrom(_RelatedEntity))
            {
                cEntityTable __Table = Query.Database.EntityManager.GetEntityTableByEnitityType(_AliaslessEntity);
                cEntityTable __RealtedTableTable = Query.Database.EntityManager.GetEntityTableByEnitityType(_RelatedEntity);
                __ParamName = __Table.TableName + "." + __RealtedTableTable.TableForeing_ColumnName_For_InOtherTable;
                Parameters.Add(new cParameter(__ParamName, ""));
            }
            else
            {
                throw new Exception("Uygun olmayan Entity tipi.!");
            }
        }

        protected cBaseCompareOperator(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, params Expression<Func<object>>[] _PropertyExpression)
            : base(_QueryFilterOperand.Query)
        {
            Parameters = new List<cParameter>();
            IsConstValue = false;
            IsPlacedInParentheses = false;
            QueryFilterOperand = _QueryFilterOperand;

            foreach (var __ValueItem in _PropertyExpression)
            {
                string __ParamName = "";
                Type __ObjectType = Query.Database.App.Handlers.LambdaHandler.GetObjectPropType(__ValueItem);
                if (typeof(cBaseEntity).IsAssignableFrom(__ObjectType))
                {
                    __ParamName = Query.Database.App.Handlers.LambdaHandler.GetObjectName(__ValueItem);
                    string __ColumnName = Query.Database.EntityManager.GetEntityTableByEnitityType<TEntity>().TableForeing_ColumnName_For_InOtherTable;
                    cEntityTable __Table = Query.Database.EntityManager.GetEntityTableByEnitityType(__ObjectType);
                    if (__Table.EntityFieldList.Where(__Item => __Item.ColumnName == __ColumnName).ToList().Count > 0)
                    {
                        __ParamName += "." + __ColumnName;
                    }
                    else
                    {
                        throw new Exception(__Table.TableName + "." + __ColumnName + " Kolonu bulunamadı..!");
                    }

                }
                else
                {
                    __ParamName = Query.Database.App.Handlers.LambdaHandler.GetObjectNameAndPropName(__ValueItem);
                }
                Parameters.Add(new cParameter(__ParamName, ""));
            }

        }

        protected void InitParametersToQuery()
        {
            foreach (var __Param in Parameters)
            {
                Query.Parameters.Add(__Param);
            }
        }

    }
}
