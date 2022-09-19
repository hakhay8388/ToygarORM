using Toygar.DB.Data.nDataUtils;
using Toygar.DB.Data.nDataUtils.nMsSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDataToolCatalog
{
    public class cSqlServerDataToolOperationSQLCatalog : cBaseDataToolOperationSQLCatalog
    {
        public cSqlServerDataToolOperationSQLCatalog(IDatabase _Database)
            : base(_Database)
        { 
        }
        public override string ConvertToString(string _Value)
        {
            return "CAST(" + _Value + " AS VARCHAR)";
        }
        public override string ConvertToDate(string _Value)
        {
            return "CAST(" + _Value + " AS date)";
        }
        public override string FullTextFilter(string _ColumnName, string _SearchExpression)
        {
            return "CONTAINS(" + _ColumnName + ", '" + _SearchExpression + "')";
        }

		public override string As(string _Value, string _AsColumn)
		{
			return  _Value  + " AS " + _AsColumn;
		}

        public override cBaseHardCodedValues GetHardCodedValues(IDataService _DataService)
        {
            return new cHardCodedValues(_DataService);
        }

        public override string IfIsNull(string _Column1, string _Column2, string _ColumnAs)
        {
            return " IsNull(" + _Column1 + "," + _Column2 + ") " + (_ColumnAs) + " ";
        }

        public override string CastAsVarchar(string _Column1, string _ColumnAs)
        {
            return " CAST(" + _Column1 + " AS varchar(max)) " + (_ColumnAs) + " ";
        }
    }
}
