using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nApply
{
    public interface IApplyable<TEntity>
        where TEntity : cBaseEntity
    {
        IApplyEnd<TEntity> Apply(IQuery _Query);
        IApplyEnd<TEntity> Apply(IQuery _Query, Expression<Func<object>> _SubQueryExternalAlias);
    }
}
