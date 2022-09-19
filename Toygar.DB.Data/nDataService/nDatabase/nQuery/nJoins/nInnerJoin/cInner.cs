using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nInnerJoin
{
    public class cInner<TEntity, TJoin> : cBaseJoinType<TEntity, TJoin>
        where TEntity : cBaseEntity
        where TJoin : cBaseEntity
    {
        public cQuery<TEntity> Query { get; set; }
        public cInner(cQuery<TEntity> _Query)
            : base(_Query)
        {
            Query = _Query;
        }

        public override cSql GetSql(string _DataSource, string _Condition)
        {
            return Query.Database.Catalogs.RowOperationSQLCatalog.SQLInnerJoin(_DataSource, _Condition);
        }
    }
}
