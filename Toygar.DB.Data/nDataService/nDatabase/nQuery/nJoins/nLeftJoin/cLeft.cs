using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nLeftJoin
{
    public class cLeft<TEntity, TJoin> : cBaseJoinType<TEntity, TJoin>
        where TEntity : cBaseEntity
        where TJoin : cBaseEntity
    {
#pragma warning disable CS0108 // 'cLeft<TEntity, TJoin>.Query' hides inherited member 'cBaseJoinType<TEntity, TJoin>.Query'. Use the new keyword if hiding was intended.
        public cQuery<TEntity> Query { get; set; }
#pragma warning restore CS0108 // 'cLeft<TEntity, TJoin>.Query' hides inherited member 'cBaseJoinType<TEntity, TJoin>.Query'. Use the new keyword if hiding was intended.
        public cLeft(cQuery<TEntity> _Query)
            : base(_Query)
        {
            Query = _Query;
        }

        public override cSql GetSql(string _DataSource, string _Condition)
        {
            return Query.Database.Catalogs.RowOperationSQLCatalog.SQLLeftJoin(_DataSource, _Condition);
        }
    }
}
