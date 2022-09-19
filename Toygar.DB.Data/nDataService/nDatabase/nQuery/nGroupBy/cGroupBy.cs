using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy.nHaving;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nGroupByQueryElement;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy
{
    public class cGroupBy<TEntity, TAlias> : cBaseQuery<TEntity>, IGroupBy<TEntity, TAlias>
        where TEntity : cBaseEntity
        where TAlias : cBaseEntity
    {
        List<string> NameList { get; set; }
        //public IBaseQuery Query { get; set; }
        public new cQuery<TEntity> Query { get; set; }


        public cGroupBy(cQuery<TEntity> _OwnerQuery, params Expression<Func<object>>[] _Columns)
            : base(_OwnerQuery.Database, EQueryType.Select)
        {
            Query = _OwnerQuery;
            NameList = new List<string>();
            foreach (var __ValueItem in _Columns)
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
                    __ParamName = Query.DefaultAlias + "." + Query.Database.App.Handlers.LambdaHandler.GetObjectNameAndPropName(__ValueItem);
                }
                NameList.Add(__ParamName);
            }

            cGroupBy_QueryElement<TEntity, TAlias> __GroupBy = new cGroupBy_QueryElement<TEntity, TAlias>(Query, this);
            Query.Add(Query.GroupBys, __GroupBy);
        }
        public cGroupBy(cQuery<TEntity> _OwnerQuery, params string[] _Columns)
            : base(_OwnerQuery.Database, EQueryType.Select)
        {
            Query = _OwnerQuery;
            NameList = new List<string>();
            NameList.AddRange(_Columns);


            cGroupBy_QueryElement<TEntity, TAlias> __GroupBy = new cGroupBy_QueryElement<TEntity, TAlias>(Query, this);
            Query.Add(Query.GroupBys, __GroupBy);
        }

        public cGroupBy(cQuery<TEntity> _OwnerQuery, params Expression<Func<TEntity, object>>[] _Columns)
            : base(_OwnerQuery.Database, EQueryType.Select)
        {
            Query = _OwnerQuery;
            NameList = new List<string>();
            foreach (var __ValueItem in _Columns)
            {
                string __ParamName = "";
                Type __ObjectType = Query.Database.App.Handlers.LambdaHandler.GetParamPropType<TEntity>(__ValueItem);
                if (typeof(cBaseEntity).IsAssignableFrom(__ObjectType))
                {
                    __ParamName = Query.Database.App.Handlers.LambdaHandler.GetParamPropName<TEntity>(__ValueItem);
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
                    __ParamName = Query.DefaultAlias + "." + Query.Database.App.Handlers.LambdaHandler.GetParamPropName<TEntity>(__ValueItem);
                }
                NameList.Add(__ParamName);
            }


            cGroupBy_QueryElement<TEntity, TAlias> __GroupBy = new cGroupBy_QueryElement<TEntity, TAlias>(Query, this);
            Query.Add(Query.GroupBys, __GroupBy);
        }

        public cGroupBy(cQuery<TEntity> _OwnerQuery, string _ColumnName)
            : base(_OwnerQuery.Database, EQueryType.Select)
        {
            Query = _OwnerQuery;

            NameList = new List<string>();
            string __ParamName = Query.DefaultAlias + "." + _ColumnName;
            NameList.Add(__ParamName);

            cGroupBy_QueryElement<TEntity, TAlias> __GroupBy = new cGroupBy_QueryElement<TEntity, TAlias>(Query, this);
            Query.Add(Query.GroupBys, __GroupBy);
        }


        public cGroupBy(cQuery<TEntity> _OwnerQuery, Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _Column)
           : base(_OwnerQuery.Database, EQueryType.Select)
        {
            Query = _OwnerQuery;

            NameList = new List<string>();

            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(_Column);

            string __ParamName = __AliasName + "." + __ColumnName;
            NameList.Add(__ParamName);

            cGroupBy_QueryElement<TEntity, TAlias> __GroupBy = new cGroupBy_QueryElement<TEntity, TAlias>(Query, this);
            Query.Add(Query.GroupBys, __GroupBy);
        }

        public cGroupBy(cQuery<TEntity> _OwnerQuery, Expression<Func<TAlias>> _Alias, string _ColumnName)
          : base(_OwnerQuery.Database, EQueryType.Select)
        {
            Query = _OwnerQuery;

            NameList = new List<string>();

            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);

            string __ParamName = __AliasName + "." + _ColumnName;
            NameList.Add(__ParamName);

            cGroupBy_QueryElement<TEntity, TAlias> __GroupBy = new cGroupBy_QueryElement<TEntity, TAlias>(Query, this);
            Query.Add(Query.GroupBys, __GroupBy);
        }

        protected string CollectFilters()
        {
            string __Result = "";
            Filters.ForEach(__Item =>
            {
                __Result += __Item.ToElementString();

            });
            return __Result;
        }

        public IHaving<TEntity, TEntity, TAlias> Having()
        {
            return new cHaving<TEntity, TEntity, TAlias>(this);
        }

        public cQuery<TEntity> ToQuery()
        {
            return Query;
        }

        public override cSql ToSql()
        {
            string __Filters = CollectFilters();
            Query.Parameters = Query.Parameters.Union(Parameters).ToList(); ;

            string __Params = "";
            foreach (var __Item in NameList)
            {
                __Params += __Params.IsNullOrEmpty() ? __Item : ", " + __Item;

            }
            return Database.Catalogs.RowOperationSQLCatalog.SQLGroupByHaving(__Params, __Filters);
        }
    }
}
