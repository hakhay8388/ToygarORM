using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators
{
    public class cParameter
    {
        public string ParamName { get; set; }
        public object ParamValue { get; set; }
        public cParameter(string _ParamName, object _ParamValue)
        {
            ParamName = _ParamName;
            ParamValue = _ParamValue;
        }
    }
}
