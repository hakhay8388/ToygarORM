using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements
{
    public interface ISelectableColumns
    {
        List<string> GetColumnNameList();
    }
}
