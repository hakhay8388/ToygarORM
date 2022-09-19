using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.DB.Data.nDataService
{
    public interface IDataService
    {
        cDatabaseContext DatabaseContext { get; }
        IDatabase Database { get; set; }

        void Perform(Func<bool> _ServiceMethod);
        TOutput Perform<TInput, TOutput>(Func<TInput, TOutput> _ServiceMethod, TInput _Input);
        void Perform<TInput>(Action<TInput> _ServiceMethod, TInput _Input);

        void Perform(Action _ServiceMethod);
        void InvokeTransactionalAction(Func<bool> _ServiceMethod);

        void LoadDB(EDBVendor _DBVendor, string _Server, string _UserName, string _Password, string _Database, int _MaxConnectCount);
    }
}
