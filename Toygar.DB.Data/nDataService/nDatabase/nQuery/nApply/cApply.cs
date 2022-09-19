using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nApply
{
    public class cApply<TEntity> : cBaseQuery<TEntity>, IApply, IApplyEnd<TEntity>
        where TEntity : cBaseEntity
    {
        cBaseApplyType<TEntity> ApplyType { get; set; }
        public IQuery OwnerQuery { get; set; }

        public cApply(cQuery<TEntity> _OwnerQuery, cBaseApplyType<TEntity> _ApplyType, IQuery _Query)
            : base(_OwnerQuery.Database, EQueryType.Select, _Query)
        {
            OwnerQuery = _OwnerQuery;
            ApplyType = _ApplyType;
        }

        public cApply(cQuery<TEntity> _OwnerQuery, cBaseApplyType<TEntity> _ApplyType, IQuery _Query, Expression<Func<object>> _SubQueryExternalAlias)
            : this(_OwnerQuery, _ApplyType, _Query)
        {
            _Query.DefaultExternalAlias = Database.App.Handlers.LambdaHandler.GetObjectName(_SubQueryExternalAlias);
        }

        public override cSql ToSql()
        {
            string __DataSources = CollectDataSource();
            cSql __Sql = ApplyType.GetSql(__DataSources);
            OwnerQuery.Parameters = OwnerQuery.Parameters.Union(Parameters).ToList();
            return __Sql;
        }

        public cQuery<TEntity> EndApply()
        {
            return (cQuery<TEntity>)OwnerQuery;
        }
    }
}
