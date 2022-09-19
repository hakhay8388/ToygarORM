using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators
{
    public static class ParameterNameGenerator
    {
        static int ParamCounter = 0;

        public static string GetNewParamName()
        {

            if (ParamCounter > 10000000)
            {
                ParamCounter = 0;
            }
            ParamCounter++;

            return "Param" + ParamCounter.ToString();
        }
    }
}
