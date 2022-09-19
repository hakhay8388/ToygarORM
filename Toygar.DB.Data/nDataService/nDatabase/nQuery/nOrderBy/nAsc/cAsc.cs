using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nOrderByQueryElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nOrderBy.nAsc
{
    public class cAsc<TEntity> : cBaseQueryElement where TEntity : cBaseEntity
    {
        protected List<string> NameList { get; set; }
        public new cQuery<TEntity> Query { get; set; }

        public cAsc(cQuery<TEntity> _OwnerQuery, params Expression<Func<object>>[] _Columns)
           : base(_OwnerQuery)
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
                    string __ColumnName = Query.EntityTable.TableForeing_ColumnName_For_InOtherTable;
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

            cOrderByQueryElement<TEntity> __GroupBy = new cOrderByQueryElement<TEntity>(Query, this);
            Query.Add(Query.OrderBys, __GroupBy);
        }

        public cAsc(cQuery<TEntity> _OwnerQuery, params Expression<Func<TEntity, object>>[] _Columns)
            : base(_OwnerQuery)
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
                    string __ColumnName = Query.EntityTable.TableForeing_ColumnName_For_InOtherTable;
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


            cOrderByQueryElement<TEntity> __GroupBy = new cOrderByQueryElement<TEntity>(Query, this);
            Query.Add(Query.OrderBys, __GroupBy);
        }

        public cAsc(cQuery<TEntity> _OwnerQuery, string _ColumnName, bool _UseAlias)
            : base(_OwnerQuery)
        {
            Query = _OwnerQuery;

            NameList = new List<string>();
            string __ParamName = _UseAlias ? Query.DefaultAlias + "." + _ColumnName : _ColumnName;
            NameList.Add(__ParamName);

            cOrderByQueryElement<TEntity> __GroupBy = new cOrderByQueryElement<TEntity>(Query, this);
            Query.Add(Query.OrderBys, __GroupBy);
        }

        public cAsc(cQuery<TEntity> _OwnerQuery, string _AliasName, string _ColumnName)
            : base(_OwnerQuery)
        {
            Query = _OwnerQuery;

            NameList = new List<string>();
            string __ParamName = _AliasName + "." + _ColumnName;
            NameList.Add(__ParamName);

            cOrderByQueryElement<TEntity> __OrderBy = new cOrderByQueryElement<TEntity>(Query, this);
            Query.Add(Query.OrderBys, __OrderBy);
        }

        public override string ToElementString(params object[] _Params)
        {
            string __Result = "";
            foreach (var __Item in NameList)
            {
                __Result += __Result.IsNullOrEmpty() ? __Item : ", " + __Item;
            }
            return " " + __Result + " ASC";
        }

    }
}
