using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy.nHaving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy
{
    public interface IGroupBy<TEntity, TAlias>
        where TEntity : cBaseEntity
        where TAlias : cBaseEntity
    {
        IHaving<TEntity, TEntity, TAlias> Having();
        cQuery<TEntity> ToQuery();
    }
}
