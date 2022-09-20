using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;
using Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nEntityServices.nEntities;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;

namespace Toygar.DB.Data.nDataServiceManager
{
    public interface IGlobalDefaultDataLoader : IDefaultDataLoader
    {

        cProfileEntity GetProfileByHostName<TBaseEntity>(string _HostName) where TBaseEntity : cBaseEntity;
    }
}
