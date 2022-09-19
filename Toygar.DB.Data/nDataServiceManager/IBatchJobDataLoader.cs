using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.DB.Data.nDataServiceManager
{
    public interface IBatchJobDataLoader
    {
        void Load(IDataService _DataService);
    }
}
