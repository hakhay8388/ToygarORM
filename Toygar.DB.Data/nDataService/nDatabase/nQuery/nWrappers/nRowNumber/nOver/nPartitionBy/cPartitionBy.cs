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
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nWrappers;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber.nOver.nOrderBy;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber.nOver.nPartitionBy
{
    public class cPartitionBy<TEntity> : cBaseQueryElement where TEntity : cBaseEntity
    {
        private string ColumnName { get; set; }
        public new cQuery<TEntity> Query { get; set; }
        cRowNumber<TEntity> RowNumber { get; set; }
        public cPartitionBy(cRowNumber<TEntity> _RowNumber, Expression<Func<object>> _Column)
            : base(_RowNumber.Query)
        {
            RowNumber = _RowNumber;
            Query = _RowNumber.Query;
            ColumnName = _RowNumber.RowNumberTempAlias;
            Type __ObjectType = Query.Database.App.Handlers.LambdaHandler.GetObjectPropType(_Column);
            if (typeof(cBaseEntity).IsAssignableFrom(__ObjectType))
            {
                string __ColumnName = Query.EntityTable.TableForeing_ColumnName_For_InOtherTable;
                cEntityTable __Table = Query.Database.EntityManager.GetEntityTableByEnitityType(__ObjectType);
                if (__Table.EntityFieldList.Where(__Item => __Item.ColumnName == __ColumnName).ToList().Count > 0)
                {
                    ColumnName += "." + __ColumnName;
                }
                else
                {
                    throw new Exception(__Table.TableName + "." + __ColumnName + " Kolonu bulunamadı..!");
                }

            }
            else
            {
                ColumnName += "." + Query.Database.App.Handlers.LambdaHandler.GetObjectNameAndPropName(_Column);
            }
        }

        public cPartitionBy(cRowNumber<TEntity> _RowNumber, Expression<Func<TEntity, object>> _Columns)
            : base(_RowNumber.Query)
        {
            RowNumber = _RowNumber;
            Query = _RowNumber.Query;

            ColumnName = _RowNumber.RowNumberTempAlias;
            Type __ObjectType = Query.Database.App.Handlers.LambdaHandler.GetParamPropType<TEntity>(_Columns);
            if (typeof(cBaseEntity).IsAssignableFrom(__ObjectType))
            {
                string __ColumnName = Query.EntityTable.TableForeing_ColumnName_For_InOtherTable;
                cEntityTable __Table = Query.Database.EntityManager.GetEntityTableByEnitityType(__ObjectType);
                if (__Table.EntityFieldList.Where(__Item => __Item.ColumnName == __ColumnName).ToList().Count > 0)
                {
                    ColumnName += "." + __ColumnName;
                }
                else
                {
                    throw new Exception(__Table.TableName + "." + __ColumnName + " Kolonu bulunamadı..!");
                }

            }
            else
            {
                ColumnName += "." + Query.Database.App.Handlers.LambdaHandler.GetParamPropName<TEntity>(_Columns);
            }

        }

        public cPartitionBy(cRowNumber<TEntity> _RowNumber, string _ColumnName)
            : base(_RowNumber.Query)
        {
            RowNumber = _RowNumber;
            Query = _RowNumber.Query;
            ColumnName = _RowNumber.RowNumberTempAlias + "." + _ColumnName;
        }


/*
        public cPartitionBy(cRowNumber<TEntity> _RowNumber, string _AliasName, string _ColumnName)
        : base(_RowNumber.Query)
        {
            RowNumber = _RowNumber;
            Query = _RowNumber.Query;

            ColumnName = _AliasName;
            ColumnName += "." + _ColumnName;
        }*/


        public cOrderBy<TEntity> OrderBy()
        {
            return RowNumber.OrderBy();
        }

        public override string ToElementString(params object[] _Params)
        {
            return " " + ColumnName + " ";
        }
       
    }
}
