using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;
using Toygar.DB.Data.nDataFileEntity;

namespace Toygar.DB.Data.nDataServiceManager
{
    public interface IGlobalDefaultDataLoader : IDefaultDataLoader
    {
        cDBConnectionSettingEntity GetConnectionSettingByHostName(string _HostName);
    }
}
