using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins
{
    public interface IJoinable<TEntity, TJoin>
        where TEntity : cBaseEntity
        where TJoin : cBaseEntity
    {
        IJoinOn<TEntity, TJoin> Join();
        IJoinOn<TEntity, TJoin> Join(IQuery _Query);
        IJoinOn<TEntity, TJoin> Join(Expression<Func<TJoin>> _Alias);
        IJoinOn<TEntity, TJoin> Join(Expression<Func<TJoin>> _Alias, IQuery _Query);
        IJoinOn<TEntity, TJoin> Join(Expression<Func<TJoin>> _Alias, IQuery _Query, Expression<Func<TJoin>> _SubQueryExternalAlias);
    }
}
