using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements
{
    public abstract class cBaseQueryElement : IQueryElement
    {
        public IBaseQuery Query { get; set; }
        public cBaseQueryElement(IBaseQuery _Query)
        {
            Query = _Query;
        }
        public abstract string ToElementString(params object[] _Params);
    }
}
