using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nConnection
{
    public class cSqlServerConnection : cBaseConnection
    {
        public cSqlServerConnection(cConnectionPoolingManager _ConnectionController)
            : base(_ConnectionController)
        {

        }
        protected override IDbConnection NewConnection()
        {
            return new SqlConnection();
        }

        protected override IDbCommand NewDbCommand()
        {
            return new SqlCommand();
        }

        protected override IDbDataAdapter NewDbDataAdapter()
        {
            return new SqlDataAdapter();
        }

        public override string GetParameterMarker()
        {
            return "@";
        }

        protected override void AfterOpen()
        {
            // RCSI is used database level, SET TRANSACTION ISOLATION.. not necessary
        }
    }
}
