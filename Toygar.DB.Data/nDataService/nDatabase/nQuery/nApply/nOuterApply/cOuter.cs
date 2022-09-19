using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nApply.nOuterApply
{
    public class cOuter<TEntity> : cBaseApplyType<TEntity>
        where TEntity : cBaseEntity
    {
        public cOuter(cQuery<TEntity> _Query)
            : base(_Query)
        {
            Query = _Query;
        }

        public override cSql GetSql(string _DataSource)
        {
            return Query.Database.Catalogs.RowOperationSQLCatalog.SQLOuterApply(_DataSource);
        }
    }
}
