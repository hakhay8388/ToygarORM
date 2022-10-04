using Toygar.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins
{
    public class cJoin<TEntity, TJoin> : cBaseQuery<TJoin>, IJoin, IJoinOn<TEntity, TJoin>
        where TEntity : cBaseEntity
        where TJoin : cBaseEntity
    {
        cBaseJoinType<TEntity, TJoin> JoinType { get; set; }
        public IQuery OwnerQuery { get; set; }
        public cJoin(cQuery<TEntity> _OwnerQuery, cBaseJoinType<TEntity, TJoin> _JoinType)
            : base(_OwnerQuery.Database, EQueryType.Select)
        {
            OwnerQuery = _OwnerQuery;
            JoinType = _JoinType;
        }

        public cJoin(cQuery<TEntity> _OwnerQuery, cBaseJoinType<TEntity, TJoin> _JoinType, Expression<Func<TJoin>> _Alias)
            : base(_OwnerQuery.Database, EQueryType.Select, _Alias)
        {
            OwnerQuery = _OwnerQuery;
            JoinType = _JoinType;
        }

        public cJoin(cQuery<TEntity> _OwnerQuery, cBaseJoinType<TEntity, TJoin> _JoinType, IQuery _Query)
            : base(_OwnerQuery.Database, EQueryType.Select, _Query)
        {
            OwnerQuery = _OwnerQuery;
            JoinType = _JoinType;
        }

        public cJoin(cQuery<TEntity> _OwnerQuery, cBaseJoinType<TEntity, TJoin> _JoinType, Expression<Func<TJoin>> _Alias, IQuery _Query)
            : base(_OwnerQuery.Database, EQueryType.Select, _Alias, _Query)
        {
            OwnerQuery = _OwnerQuery;
            JoinType = _JoinType;
        }

        public cJoin(cQuery<TEntity> _OwnerQuery, cBaseJoinType<TEntity, TJoin> _JoinType, Expression<Func<TJoin>> _Alias, IQuery _Query, Expression<Func<TJoin>> _SubQueryExternalAlias)
            : this(_OwnerQuery, _JoinType, _Alias, _Query)
        {
            _Query.DefaultExternalAlias = Database.App.Handlers.LambdaHandler.GetObjectName<TJoin>(_SubQueryExternalAlias);
        }

        public override cSql ToSql()
        {
            string __DataSources = CollectDataSource();
            string __Filters = CollectFilters();
            cSql __Sql = JoinType.GetSql(__DataSources, __Filters);
            OwnerQuery.Parameters = OwnerQuery.Parameters.Union(Parameters).ToList();
            return __Sql;
        }

        public IBaseFilterForOperands<TEntity, TJoin> On()
        {
            return new cJoinOn<TEntity, TJoin>(this);
        }
    }
}
