using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery
{
    public interface IBaseQuery
    {
        EQueryType QueryType { get; set; }

        string DefaultAlias { get; }
        cSql ToSql();

        IDatabase Database { get; }
        cEntityTable EntityTable { get; }

        List<IQueryElement> DataSource { get; }
        List<IQueryElement> Columns { get; }
        List<IFilterElement> Filters { get; }
        List<cParameter> Parameters { get; set; }
    }
}
