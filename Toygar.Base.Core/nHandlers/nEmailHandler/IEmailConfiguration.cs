using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace Toygar.Base.Core.nHandlers.nEmailHandler
{
    public interface IEmailConfiguration
    {
        cEmailConfiguration GetEmailConfiguration();
    }
}
