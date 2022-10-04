
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Toygar.Boundary.nData;
using Toygar.DB.Data.nConfiguration;

namespace Toygar.DB.Data.nDataService.nDatabase
{
    public class cDatabaseContext : cCoreServiceContext
    {
        public cDataConfiguration Configuration { get { return App.Cfg<cDataConfiguration>(); } }
        public EDBVendor DBVendor { get; set; }
        public int MaxConnectionCount { get; set; }
        public string Server { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public cDatabaseContext(cApp _App, EDBVendor _DBVendor, string _Server, string _UserName, string _Password, string _Database, int _MaxConnectionCount)
            : base(_App)
        {
            DBVendor = _DBVendor;
            Server = _Server;
            UserName = _UserName;
            Password = _Password;
            Database = _Database;
            MaxConnectionCount = _MaxConnectionCount;
        }

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(Database))
                {
                    return "server=" + Server + ";user id=" + UserName + ";password=" + Password + ";max pool size=" + (MaxConnectionCount + 5) + ";Connection Timeout=60";
                }
                else
                {
                    return "server=" + Server + ";user id=" + UserName + ";password=" + Password + ";database=" + Database + ";max pool size=" + (MaxConnectionCount + 5) + ";Connection Timeout=60";
                }

            }
        }
    }

}
