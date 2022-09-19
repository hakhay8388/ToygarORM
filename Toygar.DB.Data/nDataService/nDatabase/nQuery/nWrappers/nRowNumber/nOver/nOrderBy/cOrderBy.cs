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
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber.nOver.nOrderBy.nAsc;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber.nOver.nOrderBy
{
    public class cOrderBy<TEntity> where TEntity : cBaseEntity
    {
        cRowNumber<TEntity> RowNumber { get; set; }
        public cOrderBy(cRowNumber<TEntity> _RowNumber)
        {
            RowNumber = _RowNumber;
        }

        public cOrderBy<TEntity> Asc(params Expression<Func<object>>[] _Columns)
        {
            new cAsc<TEntity>(RowNumber, _Columns);
            return this;
        }

        public cOrderBy<TEntity> Asc(params Expression<Func<TEntity, object>>[] _Columns)
        {
            new cAsc<TEntity>(RowNumber, _Columns);
            return this;
        }

        public cOrderBy<TEntity> Asc(string _ColumnName)
        {
            new cAsc<TEntity>(RowNumber, _ColumnName);
            return this;
        }

        public cOrderBy<TEntity> Asc<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            if (RowNumber.Query.EntityTable.ControlColumnByName<TRelationEntity>())
            {
                return Asc(RowNumber.Query.EntityTable.GetRelationColumnName<TRelationEntity>());
            }
            return this;
        }

        public cOrderBy<TEntity> Desc(params Expression<Func<object>>[] _Columns)
        {
            new cDesc<TEntity>(RowNumber, _Columns);
            return this;
        }

        public cOrderBy<TEntity> Desc(params Expression<Func<TEntity, object>>[] _Columns)
        {
            new cDesc<TEntity>(RowNumber, _Columns);
            return this;
        }

        public cOrderBy<TEntity> Desc(string _ColumnName)
        {
            new cDesc<TEntity>(RowNumber, _ColumnName);
            return this;
        }

        public cOrderBy<TEntity> Desc<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            if (RowNumber.Query.EntityTable.ControlColumnByName<TRelationEntity>())
            {
                return Desc(RowNumber.Query.EntityTable.GetRelationColumnName<TRelationEntity>());
            }
            return this;
        }

        public cQuery<TEntity> ToQuery()
        {
            return RowNumber.Query;
        }
    }
}
