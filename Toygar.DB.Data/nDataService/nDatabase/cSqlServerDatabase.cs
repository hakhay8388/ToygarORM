using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.DB.Data.nDataService.nDatabase;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nConnection;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;

namespace Toygar.DB.Data.nDataService.nDatabase
{
    public class cSqlServerDatabase<TBaseEntity> : cBaseDatabase<TBaseEntity> where TBaseEntity : cBaseEntity
    {
        public cSqlServerDatabase(cDatabaseContext _DatabaseContext, bool _IsGlobalConnection)
            : base(_DatabaseContext, _IsGlobalConnection)
        {
        }

        protected override Type GetConnectionType()
        {
            return typeof(cSqlServerConnection);
        }

        protected override cBaseCatalogs GetCatalogs()
        {
            return new cSqlServerCatalogs(this);
        }
    }
}
