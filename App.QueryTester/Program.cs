using Bootstrapper.Boundary.nCore.nBootType;
using Bootstrapper.Core.nApplication;
using System;
using System.IO;
using System.Text;
using Toygar.DB.Data;
using Toygar.DB.Data.nConfiguration;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataServiceManager;
using Toygar.Boundary.nData;
using App.QueryTester.nDataServices.nDataService.nDataManagers;
using App.QueryTester.nDataServices.nDataService.nEntityServices.nEntities;
using App.QueryTester.nDataServices.nDataService.nOtherEntityServices.nEntities;

namespace App.QueryTester
{
    class Program
    {
        static void Main(string[] args)
        {
            //first create configuration
            cDataConfiguration __DataConfiguration = ToygarApp.CreateConfiguration(EBootType.Console);

            //this is domain search layer order 
            //this application name is starting with App (App.QueryTester) so like this 
            // if you have many layer you can add your domain from core to app layer
            
            __DataConfiguration.DomainNames.Add("App");

            // this is culture
            __DataConfiguration.UICultureName = "tr-TR";


            // this section is gloabal DB settings
            // Global DB is ChildDB's setting container
            // We can use this for many far layers synchronization and many things

            __DataConfiguration.MaxConnectCount = 100;
            __DataConfiguration.GlobalDBUserName = "sa";
            __DataConfiguration.GlobalDBPassword = "123456";
            __DataConfiguration.GlobalDBServer = "127.0.0.1";
            __DataConfiguration.GlobalDBName = "GlobalQueryTesterDB2";
            __DataConfiguration.DBVendor = EDBVendor.MSSQL;
            /////////////////////////


            // If you changed entity structure you need increase version for apply entity structure to DB (code first)
            __DataConfiguration.DBVersion = 4;


            // Define your child DBs
            // You can define same domain diffrent DB
            // You can define diffrent domain same DB
            // You can define diffrent domain diffrent DB
            // what is cBaseTestEntity mean. this meain is TestDB2 table is extending from cBaseTestEntity.
            // so those classes will create Tables in TestDB2 drived by cBaseTestEntity 
            __DataConfiguration.Add<cBaseTestEntity>("localhost", "sa", "123456", "127.0.0.1", "TestDB2", 100, EDBVendor.MSSQL);
            __DataConfiguration.Add<cBaseOtherTestEntity>("otherdomain", "sa", "123456", "127.0.0.1", "TestDB3", 100, EDBVendor.MSSQL);

            // ToygarORM initializing
            ToygarApp.Init(__DataConfiguration);


            TestDomian1();
            TestDomian2();
        }

        static void TestDomian1()
        {
            // IDataServiceManager is all DataServices manager.
            IDataServiceManager __DataServiceManager = ToygarApp.GetDataServiceManager();
            IDataService __DataService = __DataServiceManager.GetDataService<cBaseTestEntity>("localhost");

            cDefaultParamDataManager<cBaseTestEntity> __ParamDataManager = new cDefaultParamDataManager<cBaseTestEntity>(__DataServiceManager);


            // this is commit section
            // if occur exception, all operation will rollback
            __DataService.Perform(() =>
            {
                __ParamDataManager.AddParam(__DataService, "Param1", "TestValue");
            });
        }

        static void TestDomian2()
        {
            // IDataServiceManager is all DataServices manager.
            IDataServiceManager __DataServiceManager = ToygarApp.GetDataServiceManager();
            IDataService __DataService = __DataServiceManager.GetDataService<cBaseOtherTestEntity>("otherdomain");

            // this is commit section
            // if occur exception, all operation will rollback
            __DataService.Perform(() =>
            {
                cTestFriendEntity __TestFriendEntity = __DataService.Database.CreateNew<cTestFriendEntity>();
                __TestFriendEntity.Name = "Hayri";
                __TestFriendEntity.Surname = "Eryürek";
                __TestFriendEntity.Save();
            });
        }

    }
}
