using Toygar.Base.Boundary.nCore.nBootType;
using Toygar.Base.Core.nApplication;
using System;
using System.IO;
using System.Text;
using Toygar.DB.Data;
using Toygar.DB.Data.nConfiguration;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataServiceManager;
using Toygar.Base.Boundary.nData;
using App.QueryTester.nDataServices.nDataService.nDataManagers;
using App.QueryTester.nDataServices.nDataService.nEntityServices.nEntities;

namespace App.QueryTester
{
    class Program
    {
        static void Main(string[] args)
        {
            cDataConfiguration __DataConfiguration = ToygarApp.CreateConfiguration(EBootType.Console);

            __DataConfiguration.DomainNames.Add("App");
            __DataConfiguration.UICultureName = "tr-TR";

            __DataConfiguration.MaxConnectCount = 100;
            __DataConfiguration.GlobalDBUserName = "sa";
            __DataConfiguration.GlobalDBPassword = "123456";
            __DataConfiguration.GlobalDBServer = "127.0.0.1";
            __DataConfiguration.GlobalDBName = "GlobalQueryTesterDB2";
            __DataConfiguration.DBVendor = EDBVendor.MSSQL;
            __DataConfiguration.DBVersion = 1;

            __DataConfiguration.Add<cBaseQueryTesterEntity>("localhost", "sa", "123456", "127.0.0.1", "TestDB2", 100, EDBVendor.MSSQL);


            ToygarApp.Init(__DataConfiguration);

            IDataServiceManager __DataServiceManager = ToygarApp.GetDataServiceManager();
            IDataService __DataService = __DataServiceManager.GetDataService<cBaseQueryTesterEntity>("localhost");

            cChecksumDataManager<cBaseQueryTesterEntity> __cChecksumDataManager = new cChecksumDataManager<cBaseQueryTesterEntity>(__DataServiceManager);

            __DataService.Perform(() =>
            {
                __cChecksumDataManager.AddCheckSum("localhost", "asdad", "asdasdasdas");
            });


            

            //__DataConfiguration.RequestPerformanceLogPath

            //IDataService __DataService = DataServiceManager.GetDataService();


        }
    }
}
