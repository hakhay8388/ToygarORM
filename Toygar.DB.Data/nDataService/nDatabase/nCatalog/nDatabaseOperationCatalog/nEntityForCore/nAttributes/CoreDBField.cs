using Bootstrapper.Core.nApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CoreDBField : System.Attribute
    {
        public Object DefaultValue;
        public CoreDBField(Object _DefaultValue)
        {
            DefaultValue = _DefaultValue;
        }

        public bool IsInteger
        {
            get
            {
                return DefaultValue is sbyte
                    || DefaultValue is byte
                    || DefaultValue is short
                    || DefaultValue is ushort
                    || DefaultValue is int
                    || DefaultValue is uint
                    || DefaultValue is long
                    || DefaultValue is ulong;
            }
        }

        public bool IsDouble
        {
            get
            {
                return DefaultValue is float
                    || DefaultValue is double;

            }
        }

        public bool IsDecimal
        {
            get
            {
                return DefaultValue is decimal;
            }
        }

        public bool IsString
        {
            get
            {
                return DefaultValue is string;
            }
        }

        public bool IsDateTime
        {
            get
            {
                try
                {
                    if (DefaultValue is DateTime)
                    {
                        return true;
                    }
                    else if (DefaultValue is string && DefaultValue.ToString().ToLower() == "now".ToLower())
                    {
                        return true;
                    }
                    else if (DefaultValue is string)
                    {
                        if (DefaultValue is string && DefaultValue.ToString() != "")
                        {
                            DateTime __Temp = Convert.ToDateTime(DefaultValue);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception _Ex)
                {
                    cApp.App.Loggers.SqlLogger.LogError(_Ex);
					//throw _Ex;
				}
                return false;
            }
        }

        public DateTime GetDefaultValueCastedDateTime()
        {
            if (DefaultValue is DateTime)
            {
                return (DateTime)DefaultValue;
            }
            else if (DefaultValue is string && DefaultValue.ToString().ToLower() == "now".ToLower())
            {
                return cApp.App.Handlers.DateTimeHandler.Now;
            }
            else if (DefaultValue is string)
            {
                DateTime __Temp = Convert.ToDateTime(DefaultValue);
                return __Temp;
            }
            throw new Exception("DBField DefaultValue Tarihe donusturulemiyor");
        }

    }
}
