using Toygar.Base.Core.nApplication;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.Base.Boundary.nData;

namespace Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TDBField : System.Attribute
    {
        public bool PrimaryKey { get; private set; }
        public bool UniqueKey { get; private set; }
        public int KeyOrderNo { get; private set; }
        public bool Nullable { get; private set; }
        Object m_DefaultValue { get; set; }
        public EDataType DataType { get; private set; }
        public int Length { get; private set; }
        public bool Identity { get; private set; }
        public int IdentityStart { get; private set; }
        public int IdentityIncrement { get; private set; }
        public bool Clustered { get; private set; }
        public Type ToForeingKey { get; private set; }

        public int DecimalBigger { get; private set; }

        public int DecimalLower { get; private set; }

        public TDBField(bool _PrimaryKey = false, bool _UniqueKey = false, bool _Clustered = false, int _KeyOrderNo = 1, bool _Nullable = true, Object _DefaultValue = null, EDataType _DataType = EDataType.Nvarchar, int _Length = 255, bool _Identity = false, int _IdentityStart = 1, int _IdentityIncrement = 1, Type _ToForeingKey = null, int _DecimalBigger = 0, int _DecimalLower = 0)
        {
            PrimaryKey = _PrimaryKey;
            UniqueKey = _UniqueKey;
            Clustered = _Clustered;
            KeyOrderNo = _KeyOrderNo;
            Nullable = _Nullable;
            m_DefaultValue = _DefaultValue;
            DataType = _DataType;
            Length = _Length;
            Identity  = _Identity;
            IdentityStart = _IdentityStart;
            IdentityIncrement =_IdentityIncrement;
            ToForeingKey = _ToForeingKey;
            DecimalBigger = _DecimalBigger;
            DecimalLower = _DecimalLower;
        }

        public Object DefaultValue
        {
            get
            {
                if (DataType == EDataType.Datetime)
                {
                    if (m_DefaultValue != null)
                    {
                        if (m_DefaultValue.ToString().ToLower() == "now".ToLower())
                        {
                            return DateTime.Now;
                        }
                        else
                        {
                            try
                            {
                                DateTime __Date = Convert.ToDateTime(m_DefaultValue);
                                return __Date;
                            }
                            catch (Exception _Ex)
                            {
                                cApp.App.Loggers.SqlLogger.LogError(_Ex);
								throw _Ex;
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (DataType == EDataType.Decimal)
                {
                    if (m_DefaultValue != null)
                    {
                        try
                        {
                            return Convert.ToDecimal(m_DefaultValue);
                        }
                        catch (Exception _Ex)
                        {
                            cApp.App.Loggers.SqlLogger.LogError(_Ex);
							throw _Ex;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return m_DefaultValue;
                }
            }
        }

        public string ForeingKey_TableName
        {
            get
            {
                if (ToForeingKey != null)
                {
                    return cEntityTable.GetTableNameByTypeEntity(ToForeingKey);
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
