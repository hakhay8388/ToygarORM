using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nApply
{
    public interface IApply : IBaseQuery
    {
        IQuery OwnerQuery { get; set; }
    }
}
