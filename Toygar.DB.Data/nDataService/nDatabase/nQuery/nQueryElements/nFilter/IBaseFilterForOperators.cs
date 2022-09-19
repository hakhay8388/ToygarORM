using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter
{
    public interface IBaseFilterForOperators<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        cQuery<TOwnerEntity> ToQuery();
        cBaseFilter<TOwnerEntity, TEntity> Not { get; }
        cBaseFilter<TOwnerEntity, TEntity> And { get; }
        cBaseFilter<TOwnerEntity, TEntity> Or { get; }
        cBaseFilter<TOwnerEntity, TEntity> PrOpen { get; }
        cBaseFilter<TOwnerEntity, TEntity> PrClose { get; }
        cBaseFilter<TOwnerEntity, TEntity> Exists(IQuery _Query);
        cWhen<TEntity> Then();
    }
}
