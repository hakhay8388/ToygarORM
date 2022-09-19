using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.DB.Data.nDataServiceManager
{
    public interface IComponentLoader
    {
        void Load(IDataService _DataService);
    }
}
