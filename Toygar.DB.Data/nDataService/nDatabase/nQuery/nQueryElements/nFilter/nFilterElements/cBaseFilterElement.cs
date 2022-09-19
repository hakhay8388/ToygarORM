using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements
{
    public abstract class cBaseFilterElement<TOwnerEntity, TEntity> : cBaseQueryElement, IFilterElement
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        //public new IBaseQuery Query { get; set; }

        public cBaseFilterElement(IBaseQuery _Query)
            : base(_Query)
        {
        }

      
    }
}
