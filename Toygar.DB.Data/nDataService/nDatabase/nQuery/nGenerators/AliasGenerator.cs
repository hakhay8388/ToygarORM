using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators
{
    public static class AliasGenerator
    {
        static int Counter = 0;
        public static string GetNewAlias(string _Value)
        {
            string __Result = _Value;

            if (Counter > 10000000)
            {
                Counter = 0;
            }
            Counter++;
            __Result += Counter.ToString();

            return __Result;
        }
    }
}
