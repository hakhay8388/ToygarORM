using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins
{
    public interface IJoinOn<TEntity, TJoin>
        where TEntity : cBaseEntity
        where TJoin : cBaseEntity
    {
        IBaseFilterForOperands<TEntity, TJoin> On();
    }
}
