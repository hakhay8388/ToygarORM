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

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber.nOver.nOrderBy.nAsc
{
    public class cAsc<TEntity> : cBaseQueryElement where TEntity : cBaseEntity
    {
        protected List<string> NameList { get; set; }
        public new cQuery<TEntity> Query { get; set; }

        public cAsc(cRowNumber<TEntity> _RowNumber, params Expression<Func<object>>[] _Columns)
            : base(_RowNumber.Query)
        {
            Query = _RowNumber.Query;
            NameList = new List<string>();
            foreach (var __ValueItem in _Columns)
            {
                string __ParamName = _RowNumber.RowNumberTempAlias;
                Type __ObjectType = Query.Database.App.Handlers.LambdaHandler.GetObjectPropType(__ValueItem);
                if (typeof(cBaseEntity).IsAssignableFrom(__ObjectType))
                {
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
                    __ParamName += "." + Query.Database.App.Handlers.LambdaHandler.GetObjectNameAndPropName(__ValueItem);
                }
                NameList.Add(__ParamName);
            }

            _RowNumber.OrderBys.Add(this);
        }

        public cAsc(cRowNumber<TEntity> _RowNumber, params Expression<Func<TEntity, object>>[] _Columns)
            : base(_RowNumber.Query)
        {
            Query = _RowNumber.Query;
            NameList = new List<string>();
            foreach (var __ValueItem in _Columns)
            {
                string __ParamName = _RowNumber.RowNumberTempAlias;
                Type __ObjectType = Query.Database.App.Handlers.LambdaHandler.GetParamPropType<TEntity>(__ValueItem);
                if (typeof(cBaseEntity).IsAssignableFrom(__ObjectType))
                {
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
                    __ParamName += "." + Query.Database.App.Handlers.LambdaHandler.GetParamPropName<TEntity>(__ValueItem);
                }
                NameList.Add(__ParamName);
            }

            _RowNumber.OrderBys.Add(this);
        }


        public cAsc(cRowNumber<TEntity> _RowNumber, string _ColumnName)
            : base(_RowNumber.Query)
        {
            Query = _RowNumber.Query;

            NameList = new List<string>();
            string __ParamName = _RowNumber.RowNumberTempAlias + "." + _ColumnName;
            NameList.Add(__ParamName);

            _RowNumber.OrderBys.Add(this);
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
