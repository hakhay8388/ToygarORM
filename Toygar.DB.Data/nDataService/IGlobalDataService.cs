using Toygar.DB.Data.nDataService.nDatabase;
using System;
using System.Collections.Generic;
using System.Text;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;

namespace Toygar.DB.Data.nDataService
{
    public interface IGlobalDataService : IDataService
    {
        void LockPofile(Action _ServiceMethod);
        public void LockPofile<TServiceBaseEntity>(string _HostName, Action _ServiceMethod) where TServiceBaseEntity : cBaseEntity;

        public bool IsProfileLocked<TServiceBaseEntity>(string _HostName, Action _ServiceMethod) where TServiceBaseEntity : cBaseEntity;

    }
}
