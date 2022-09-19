using Toygar.Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.Base.Boundary.nData
{
    public class EDataTypeClass : cBaseConstType<EDataTypeClass>
    {
        public static EDataTypeClass Image = new EDataTypeClass(GetVariableName(() => Image), 34);
        public static EDataTypeClass Text = new EDataTypeClass(GetVariableName(() => Text), 35, false);
        public static EDataTypeClass Uniqueidentifier = new EDataTypeClass(GetVariableName(() => Uniqueidentifier), 36);
        public static EDataTypeClass Date = new EDataTypeClass(GetVariableName(() => Date), 40);
        public static EDataTypeClass Time = new EDataTypeClass(GetVariableName(() => Time), 41);
        public static EDataTypeClass Datetime2 = new EDataTypeClass(GetVariableName(() => Datetime2), 42);
        public static EDataTypeClass Datetimeoffset = new EDataTypeClass(GetVariableName(() => Datetimeoffset), 43);
        public static EDataTypeClass Tinyint = new EDataTypeClass(GetVariableName(() => Tinyint), 48);
        public static EDataTypeClass Smallint = new EDataTypeClass(GetVariableName(() => Smallint), 52);
        public static EDataTypeClass Int = new EDataTypeClass(GetVariableName(() => Int), 56);
        public static EDataTypeClass Smalldatetime = new EDataTypeClass(GetVariableName(() => Smalldatetime), 58);
        public static EDataTypeClass Real = new EDataTypeClass(GetVariableName(() => Real), 59);
        public static EDataTypeClass Money = new EDataTypeClass(GetVariableName(() => Money), 60);
        public static EDataTypeClass Datetime = new EDataTypeClass(GetVariableName(() => Datetime), 61);
        public static EDataTypeClass Float = new EDataTypeClass(GetVariableName(() => Float), 62);
        public static EDataTypeClass Sql_variant = new EDataTypeClass(GetVariableName(() => Sql_variant), 98);
        public static EDataTypeClass Ntext = new EDataTypeClass(GetVariableName(() => Ntext), 99);
        public static EDataTypeClass Bit = new EDataTypeClass(GetVariableName(() => Bit), 104);
        public static EDataTypeClass Decimal = new EDataTypeClass(GetVariableName(() => Decimal), 106, false, true);
        public static EDataTypeClass Numeric = new EDataTypeClass(GetVariableName(() => Numeric), 108, false, true);
        public static EDataTypeClass Smallmoney = new EDataTypeClass(GetVariableName(() => Smallmoney), 122);
        public static EDataTypeClass Bigint = new EDataTypeClass(GetVariableName(() => Bigint), 127);
        public static EDataTypeClass Hierarchyid = new EDataTypeClass(GetVariableName(() => Hierarchyid), 240);
        public static EDataTypeClass Geometry = new EDataTypeClass(GetVariableName(() => Geometry), 240);
        public static EDataTypeClass Geography = new EDataTypeClass(GetVariableName(() => Geography), 240);
        public static EDataTypeClass Varbinary = new EDataTypeClass(GetVariableName(() => Varbinary), 165, true);
        public static EDataTypeClass Varchar = new EDataTypeClass(GetVariableName(() => Varchar), 167, true);
        public static EDataTypeClass Binary = new EDataTypeClass(GetVariableName(() => Binary), 173);
        public static EDataTypeClass Char = new EDataTypeClass(GetVariableName(() => Char), 175);
        public static EDataTypeClass Timestamp = new EDataTypeClass(GetVariableName(() => Timestamp), 189);
        public static EDataTypeClass Nvarchar = new EDataTypeClass(GetVariableName(() => Nvarchar), 231, true);
        public static EDataTypeClass Nchar = new EDataTypeClass(GetVariableName(() => Nchar), 239, true);
        public static EDataTypeClass Xml = new EDataTypeClass(GetVariableName(() => Xml), 241);
        public static EDataTypeClass Sysname = new EDataTypeClass(GetVariableName(() => Sysname), 231);
        public static EDataTypeClass NoPremitiveType = new EDataTypeClass(GetVariableName(() => NoPremitiveType), 9999);


        static List<EDataTypeClass> TypeList { get; set; }
        public bool UseLength { get; set; }

        public bool UseDecimalCount { get; set; }

        public EDataTypeClass(string _Name, int _Value, bool _UseLength = false, bool _UseDecimalCount = false)
            : base(_Name, _Name, _Value)
        {
            UseLength = _UseLength;
            ID = _Value;
            UseDecimalCount = _UseDecimalCount;
            TypeList = TypeList ?? new List<EDataTypeClass>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EDataTypeClass GetByID(int _ID, EDataTypeClass _DefaultDataType)
        {
            return GetByID(TypeList, _ID, _DefaultDataType);
        }
        public static EDataTypeClass GetByName(string _Name, EDataTypeClass _DefaultDataType)
        {
            return GetByName(TypeList, _Name, _DefaultDataType);
        }

        public static EDataTypeClass GetByName(EDataType _DataTypeEnum)
        {
            return GetByID((int)_DataTypeEnum, EDataTypeClass.Nvarchar);
        }

    }
}
