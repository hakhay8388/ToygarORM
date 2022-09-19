using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy.nHaving;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nOrderBy.nAsc;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nGroupByQueryElement;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nWrappers;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber.nOver;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber.nOver.nOrderBy;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber.nOver.nPartitionBy;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber
{
    public class cRowNumber<TEntity> : cBaseWrapper<TEntity> where TEntity : cBaseEntity
    {
        public List<IQueryElement> OrderBys { get; private set; }
        public string RowNumberTempAlias { get; set; }
        public string RowNumberColumnName { get; set; }
        cOrderBy<TEntity> m_OrderBy { get; set; }
        cPartitionBy<TEntity> m_PartitionBy { get; set; }
        string TotalCountColumName { get; set; }

        public cRowNumber(cQuery<TEntity> _Query, string _TotalCountColumName = null)
            :base(_Query)
        {
            OrderBys = new List<IQueryElement>();
            RowNumberTempAlias = AliasGenerator.GetNewAlias("RowNumberTempAlias");
            RowNumberColumnName = AliasGenerator.GetNewAlias("RowNumber");
            TotalCountColumName = _TotalCountColumName;
        }

        public cOrderBy<TEntity> OrderBy()
        {
            m_OrderBy = new cOrderBy<TEntity>(this);
            return m_OrderBy;
        }

        public cPartitionBy<TEntity> PartitionBy(Expression<Func<object>> _Columns)
        {
            m_PartitionBy = new cPartitionBy<TEntity>(this, _Columns);
            return m_PartitionBy;
        }

        public cPartitionBy<TEntity> PartitionBy(Expression<Func<TEntity, object>> _Columns)
        {
            m_PartitionBy = new cPartitionBy<TEntity>(this, _Columns);
            return m_PartitionBy;
        }

        /*public cPartitionBy<TEntity> PartitionBy<TAliasEntity>(Expression<Func<TAliasEntity>> _Alias, Expression<Func<TAliasEntity, object>> _Column)
        {
            string __AliasName = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAliasEntity>(_Alias);
            string __ColumnName = Query.Database.App.Handlers.LambdaHandler.GetParamPropName<TAliasEntity>(_Column);

            m_PartitionBy = new cPartitionBy<TEntity>(this, __AliasName, __ColumnName);
            return m_PartitionBy;
        }

        public cPartitionBy<TEntity> PartitionBy<TAliasEntity>(Expression<Func<TAliasEntity>> _Alias, string _ColumnName)
        {
            string __AliasName = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAliasEntity>(_Alias);

            m_PartitionBy = new cPartitionBy<TEntity>(this, __AliasName, _ColumnName);
            return m_PartitionBy;
        }*/

        public cPartitionBy<TEntity> PartitionBy(string _ColumnName)
        {
            m_PartitionBy = new cPartitionBy<TEntity>(this, _ColumnName);
            return m_PartitionBy;
        }

        public cPartitionBy<TEntity> Desc<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            if (Query.EntityTable.ControlColumnByName<TRelationEntity>())
            {
                return PartitionBy(Query.EntityTable.GetRelationColumnName<TRelationEntity>());
            }
            return null;
        }


        public override string Wrap(cSql _Sql)
        {
            string __OrderBy = "";
            foreach(var __Item in OrderBys )
            {
                __OrderBy += __OrderBy.IsNullOrEmpty() ? __Item.ToElementString() : ", " + __Item.ToElementString();
            }
            _Sql = Query.Database.Catalogs.RowOperationSQLCatalog.WrapForRowNumber(TotalCountColumName, m_PartitionBy == null ? "" : m_PartitionBy.ToElementString(), __OrderBy, RowNumberTempAlias, RowNumberColumnName, _Sql.FullSQLString);
            return _Sql.FullSQLString;
        }        
    }
}
