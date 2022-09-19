using Toygar.Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.Base.Boundary.nData
{
    public class EDBVendor : cBaseConstType<EDBVendor>
    {
        public const string MSSQL_Consts = "MSSQL";
        public static EDBVendor MSSQL = new EDBVendor(MSSQL_Consts, 1);

        static List<EDBVendor> TypeList { get; set; }
        public EDBVendor(string _Name, int _Value)
            : base(_Name, _Name, _Value)
        {
            TypeList = TypeList ?? new List<EDBVendor>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EDBVendor GetByID(int _ID, EDBVendor _DBVendor)
        {
            return GetByID(TypeList, _ID, _DBVendor);
        }
        public static EDBVendor GetByName(string _Name, EDBVendor _DBVendor)
        {
            return GetByName(TypeList, _Name, _DBVendor);
        }
    }
}
