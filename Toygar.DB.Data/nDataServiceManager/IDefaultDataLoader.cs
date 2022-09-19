using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.DB.Data.nDataServiceManager
{
    public interface IDefaultDataLoader
    {
        void Load(IDataService _DataService);
    }
}
