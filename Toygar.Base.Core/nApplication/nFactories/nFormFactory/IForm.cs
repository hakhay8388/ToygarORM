using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.Base.Core.nApplication.nFactories.nFormFactory
{
    public interface IForm<TOut>
    {
        TOut Result();
    }
}
