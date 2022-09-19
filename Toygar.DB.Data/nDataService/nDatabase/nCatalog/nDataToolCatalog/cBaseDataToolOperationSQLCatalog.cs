using Toygar.DB.Data.nDataUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDataToolCatalog
{
    public abstract class cBaseDataToolOperationSQLCatalog : cBaseCatalogOperations
    {
        public cBaseDataToolOperationSQLCatalog(IDatabase _Database)
            : base(_Database)
        {
        }
        public abstract string ConvertToString(string _Value);
        public abstract string ConvertToDate(string _Value);

        public abstract string FullTextFilter(string _ColumnName, string _SearchExpression);

        public string ReplaceExpression(string baseExpression, string _SearchExpression, string _ReplaceExpression)
        {
            return "REPLACE(" + baseExpression + "," + _SearchExpression + "," + _ReplaceExpression + ")";
        }
        public abstract string IfIsNull(string _Column1, string _Column2, string _ColumnAs);
        public abstract string CastAsVarchar(string _Column1, string _ColumnAs);
        public string StringExpression(object value)
        {
            return "'" + value.ToString() + "'";
        }

        public abstract string As(string _Value, string _AsColumn);

        public abstract cBaseHardCodedValues GetHardCodedValues(IDataService _DataService);


    }
}
