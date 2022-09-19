using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nSourceQueryElement
{
    public class cTableSource_QueryElement<TEntity> : cBaseQueryElement where TEntity : cBaseEntity
    {
        public cTableSource_QueryElement(IBaseQuery _Query)
            : base(_Query)
        {
        }

        public override string ToElementString(params object[] _Params)
        {
            if (Query.QueryType.ID == EQueryType.Select.ID)
            {
                return Query.EntityTable.TableName + " " + Query.DefaultAlias;
            }
            else
            {
                return Query.EntityTable.TableName;
            }            
        }
    }
}
