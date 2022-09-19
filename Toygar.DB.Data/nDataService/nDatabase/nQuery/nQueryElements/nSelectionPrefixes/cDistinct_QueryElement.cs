using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nSelectionPrefixes
{
     public class cDistinct_QueryElement<TEntity> : cBaseQueryElement
        where TEntity : cBaseEntity
    {
       public cDistinct_QueryElement(IQuery _Query)
            : base(_Query)
        {
        }

        public override string ToElementString(params object[] _Params)
        {
            return " DISTINCT ";
        }
    }}
