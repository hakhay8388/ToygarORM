using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nSelectionPrefixes
{
     public class cTop_QueryElement<TEntity> : cBaseQueryElement
        where TEntity : cBaseEntity
    {
         int Count { get; set; }
         public cTop_QueryElement(IQuery _Query, int _Count)
            : base(_Query)
        {
            Count = _Count;
        }

        public override string ToElementString(params object[] _Params)
        {
            return "  TOP (" + Count + ") ";
        }
    }
}
