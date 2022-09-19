using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.DB.Data.nDataUtils
{
    public abstract class cBaseHardCodedValues
    {
        IDataService DataService { get; set; }
        public cBaseHardCodedValues(IDataService _DataService)
        {
            DataService = _DataService;
        }

        public abstract cSql ToSql();
        public abstract void AddValue(params object[] _Value);
        public abstract void DefineColumns(params string[] _Columns);
    }
}
