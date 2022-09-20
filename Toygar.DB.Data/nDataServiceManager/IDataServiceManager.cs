using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;

namespace Toygar.DB.Data.nDataServiceManager
{
    public interface IDataServiceManager
    {
        //string GetDataHost();
        //IDataService GetDataService();
        IDataService GetDataService<TServiceBaseEntity>(string _HostName) where TServiceBaseEntity : cBaseEntity;


       List<IDataService> GetAllDataService();
		IGlobalDataService GetGlobalDataService();
    }
}
