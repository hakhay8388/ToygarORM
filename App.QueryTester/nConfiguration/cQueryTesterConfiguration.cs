using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.Base.Boundary.nCore.nBootType;
using Toygar.Base.Boundary.nData;
using Toygar.Base.Core.nApplication.nConfiguration;
using Toygar.DB.Data.nConfiguration;

namespace App.QueryTester.nConfiguration
{
    public class cQueryTesterConfiguration : cDataConfiguration
    {
        public cQueryTesterConfiguration(EBootType _BootType)
            :base(_BootType)
        {
        }

        public override void Init()
        {
            base.Init();
            TargetHostName = "localhost";

            MaxConnectCount = 100;
            GlobalDBUserName = "sa";
            GlobalDBPassword = "123456";
            GlobalDBServer = "127.0.0.1";
            GlobalDBName = "GlobalQueryTesterDB";
            DBVendor = EDBVendor.MSSQL;
        }
    }
}
