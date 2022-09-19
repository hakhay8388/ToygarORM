using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nLeftJoin;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nApplyElement;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nApply
{
    public abstract class cBaseApplyType<TEntity> : IApplyable<TEntity>
        where TEntity : cBaseEntity
    {

        public abstract cSql GetSql(string _DataSource);

        public cQuery<TEntity> Query { get; set; }
        public cBaseApplyType(cQuery<TEntity> _Query)
        {
            Query = _Query;
        }

        public IApplyEnd<TEntity> Apply(IQuery _Query)
        {
            cApply<TEntity> __Apply = new cApply<TEntity>(Query, this, _Query);
            AddQueryElement(__Apply);
            return __Apply;
        }

        public IApplyEnd<TEntity> Apply(IQuery _Query, Expression<Func<object>> _SubQueryExternalAlias)
        {
            cApply<TEntity> __Apply = new cApply<TEntity>(Query, this, _Query, _SubQueryExternalAlias);
            AddQueryElement(__Apply);
            return __Apply;
        }

        public void AddQueryElement(cApply<TEntity> _QueryElement)
        {
            cApply_QueryElement<TEntity> __Element = new cApply_QueryElement<TEntity>(Query, _QueryElement);
            Query.Joins.Add( __Element);
        }

    }
}
