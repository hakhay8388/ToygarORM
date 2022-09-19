using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn
{
    public class cEntityColumn
    {
        public static string ID_ColumnName = "ID";
        public cEntityTable EntityTable { get; private set; }
        public string Name { get; set; }
        public Type PropertyType { get; set; }
        public TDBField DBField { get; private set; }

        public cEntityColumn(cEntityTable _EntityTable, string _Name, Type _PropertyType, TDBField _DBField)
        {
            EntityTable = _EntityTable;
            PropertyType = _PropertyType;
            Name = _Name;
            DBField = _DBField;
        }


        public string ForeignKey_Reference_TableName
        {
            get
            {
                if (DBField.ToForeingKey != null)
                {
                    return DBField.ForeingKey_TableName;
                }
                else
                {
                    throw new Exception("Primitive ve BaseEntity den türememiş bir tip tanımlanamaz..!");
                }                
            }
        }

        public string ForeignKey_Reference_ColumnName
        {
            get
            {
                if (DBField.ToForeingKey != null)
                {
                    return cEntityColumn.ID_ColumnName;
                }
                else
                {
                    throw new Exception("Primitive ve BaseEntity den türememiş bir tip tanımlanamaz..!");
                }         
            }
        }

        public string ColumnName
        {
            get
            {
                if (PropertyType.IsPrimitiveWithString()  || PropertyType.IsAssignableFrom(typeof(DateTime)))
                {
                    return Name;
                }
                if (DBField.ToForeingKey != null)
                {
                    return ForeignKey_Reference_TableName + ForeignKey_Reference_ColumnName;
                }
                else
                {
                    throw new Exception("Primitive ve BaseEntity den türememiş bir tip tanımlanamaz..!");
                }
            }
        }

        public EDataTypeClass DataType
        {
            get
            {
                return EDataTypeClass.GetByID((int)DBField.DataType.ConvertEnum<EDataType>(), EDataTypeClass.Nvarchar);
            }
        }
    }
}
