using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nLeftJoin;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nJoins;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins
{
    public abstract class cBaseJoinType<TEntity, TJoin> : IJoinable<TEntity, TJoin>
        where TEntity : cBaseEntity
        where TJoin : cBaseEntity
    {

        public abstract cSql GetSql(string _DataSource, string _Condition);

        public cQuery<TEntity> Query { get; set; }
        public cBaseJoinType(cQuery<TEntity> _Query)
        {
            Query = _Query;
        }

        public IJoinOn<TEntity, TJoin> Join()
        {
            cJoin<TEntity, TJoin> __LeftJoin = new cJoin<TEntity, TJoin>(Query, this);
            AddQueryElement(__LeftJoin);
            return __LeftJoin;
        }

        public IJoinOn<TEntity, TJoin> Join(IQuery _Query)
        {
            cJoin<TEntity, TJoin> __LeftJoin = new cJoin<TEntity, TJoin>(Query, this, _Query);
            AddQueryElement(__LeftJoin);
            return __LeftJoin;
        }

        public IJoinOn<TEntity, TJoin> Join(Expression<Func<TJoin>> _Alias)
        {
            cJoin<TEntity, TJoin> __LeftJoin = new cJoin<TEntity, TJoin>(Query, this, _Alias);
            AddQueryElement(__LeftJoin);
            return __LeftJoin;
        }

        public IJoinOn<TEntity, TJoin> Join(Expression<Func<TJoin>> _Alias, IQuery _Query)
        {
            cJoin<TEntity, TJoin> __LeftJoin = new cJoin<TEntity, TJoin>(Query, this, _Alias, _Query);
            AddQueryElement(__LeftJoin);
            return __LeftJoin;
        }

        public IJoinOn<TEntity, TJoin> Join(Expression<Func<TJoin>> _Alias, IQuery _Query, Expression<Func<TJoin>> _SubQueryExternalAlias)
        {
            cJoin<TEntity, TJoin> __LeftJoin = new cJoin<TEntity, TJoin>(Query, this, _Alias, _Query, _SubQueryExternalAlias);
            AddQueryElement(__LeftJoin);
            return __LeftJoin;
        }

        public void AddQueryElement(cJoin<TEntity, TJoin> _QueryElement)
        {
            cJoin_QueryElement<TEntity, TJoin> __Element = new cJoin_QueryElement<TEntity, TJoin>(Query, _QueryElement);
            Query.Joins.Add(__Element);
        }
    }
}
