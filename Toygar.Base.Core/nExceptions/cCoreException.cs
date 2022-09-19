using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.Base.Core.nApplication;

namespace Toygar.Base.Core.nExceptions
{
    public class cCoreException : Exception
    {
        public cCoreException(nApplication.cApp _App, string _Message)
            : base(_Message)
        {
            _App.Loggers.CoreLogger.LogError("Hata : {0}", StackTrace.ToString());
        }
    }
}
