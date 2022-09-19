using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators
{
    public class cExists<TOwnerEntity, TEntity> : cBaseFilterElement<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        public IQuery SubQuery { get; set; }
        public cExists(IQueryElement _QueryElement, IQuery _Value)
            : base(_QueryElement.Query)
        {
            SubQuery = _Value;
        }  

        public override string ToElementString(params object[] _Params)
        {
            string __Result = " EXISTS ( " + SubQuery.ToSql().FullSQLString + " ) ";
            Query.Parameters = Query.Parameters.Union(SubQuery.Parameters).ToList();
            return __Result;
        }
    }
}
