using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy.nHaving;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nOrderBy.nAsc;
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

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nOrderBy
{
    public class cOrderBy<TEntity> : IOrderBy<TEntity>, IAscDesc<TEntity> where TEntity : cBaseEntity
    {
        public cQuery<TEntity> Query { get; set; }
        public cOrderBy(cQuery<TEntity> _Query)
        {
            Query = _Query;
        }

        public IAscDesc<TEntity> Asc(params Expression<Func<object>>[] _Columns)
        {
            new cAsc<TEntity>(Query, _Columns);
            return this;
        }

        public IAscDesc<TEntity> Asc(params Expression<Func<TEntity, object>>[] _Columns)
        {
            new cAsc<TEntity>(Query, _Columns);
            return this;
        }

        public IAscDesc<TEntity> Asc(string _ColumnName, bool _UseAlias = true)
        {
            new cAsc<TEntity>(Query, _ColumnName, _UseAlias);
            return this;
        }

        public IAscDesc<TEntity> Asc<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            if (Query.EntityTable.ControlColumnByName<TRelationEntity>())
            {
                Asc(Query.EntityTable.GetRelationColumnName<TRelationEntity>());
            }
            return this;
        }

        public IAscDesc<TEntity> Asc<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnName) where TAlias : cBaseEntity
        {
            string __AliasName = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            new cAsc<TEntity>(Query, __AliasName, _ColumnName);
            return this;
        }

        public IAscDesc<TEntity> Desc(params Expression<Func<object>>[] _Columns)
        {
            new cDesc<TEntity>(Query, _Columns);
            return this;
        }

        public IAscDesc<TEntity> Desc(params Expression<Func<TEntity, object>>[] _Columns)
        {
            new cDesc<TEntity>(Query, _Columns);
            return this;
        }

        public IAscDesc<TEntity> Desc(string _ColumnName, bool _UseAlias = true)
        {
            new cDesc<TEntity>(Query, _ColumnName, _UseAlias);
            return this;
        }

        public IAscDesc<TEntity> Desc<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            if (Query.EntityTable.ControlColumnByName<TRelationEntity>())
            {
                Desc(Query.EntityTable.GetRelationColumnName<TRelationEntity>());
            }
            return this;
        }

        public IAscDesc<TEntity> Desc<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnName) where TAlias : cBaseEntity
        {
            string __AliasName = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            new cDesc<TEntity>(Query, __AliasName, _ColumnName);
            return this;
        }

        public cQuery<TEntity> ToQuery()
        {
            return Query;
        }
    }
}
