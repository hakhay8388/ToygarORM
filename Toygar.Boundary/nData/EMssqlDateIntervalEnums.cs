using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Boundary.nValueTypes.nConstType;

namespace Toygar.Boundary.nData
{
    public enum EMssqlDateIntervalEnums
    {
        DAY = 0,
        MONTH = 1,
        YEAR = 2,
        //LessonRequest = 3
    }

    public class EMssqlDateInterval : cBaseConstType<EMssqlDateInterval>
    {
        public static EMssqlDateInterval DAY = new EMssqlDateInterval(GetVariableName(() => DAY), (int)EMssqlDateIntervalEnums.DAY, "DAY");
        public static EMssqlDateInterval MONTH = new EMssqlDateInterval(GetVariableName(() => MONTH), (int)EMssqlDateIntervalEnums.MONTH, "MONTH");
        public static EMssqlDateInterval YEAR = new EMssqlDateInterval(GetVariableName(() => YEAR), (int)EMssqlDateIntervalEnums.YEAR, "YEAR");
        
        public static List<EMssqlDateInterval> TypeList { get; set; }

        public EMssqlDateInterval(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EMssqlDateInterval>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EMssqlDateInterval GetByID(int _ID, EMssqlDateInterval _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EMssqlDateInterval GetByName(string _Name, EMssqlDateInterval _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }

        public static EMssqlDateInterval GetByCode(string _Code, EMssqlDateInterval _DefaultData)
        {
            return GetByCode(TypeList, _Code, _DefaultData);
        }
    }
}
