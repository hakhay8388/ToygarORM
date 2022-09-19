using Toygar.Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.Base.Boundary.nData
{
    public class EQueryType : cBaseConstType<EQueryType>
    {
        public static EQueryType Select = new EQueryType(GetVariableName(() => Select), 1);
        public static EQueryType Delete = new EQueryType(GetVariableName(() => Delete), 2);

        static List<EQueryType> TypeList { get; set; }

        public EQueryType(string _Name, int _Value)
            : base(_Name, _Name, _Value)
        {
            TypeList = TypeList ?? new List<EQueryType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EQueryType GetByID(int _ID, EQueryType _DefaultDataType)
        {
            return GetByID(TypeList, _ID, _DefaultDataType);
        }
        public static EQueryType GetByName(string _Name, EQueryType _DefaultDataType)
        {
            return GetByName(TypeList, _Name, _DefaultDataType);
        }

        public static EQueryType GetByName(EDataType _DataTypeEnum, EQueryType _DefaultDataType)
        {
            return GetByID((int)_DataTypeEnum, _DefaultDataType);
        }
    }
}