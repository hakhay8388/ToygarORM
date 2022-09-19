using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nAttributes;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities
{
    public abstract class cBaseCoreEntity<TEntity> : ICoreEntity where TEntity : ICoreEntity
    {
        [CoreDBField(0)]
        public long ObjectID { get; set; }

        public cBaseCoreEntity()
        {
            SetDefaultValue();
        }

        private void InitializeDatas(DataRow _Row)
        {
            _Row.Fill(this);
        }

        private void SetDefaultValue()
        {
            Type __Type = this.GetType();
            foreach (PropertyInfo __PropertyInfo in __Type.GetProperties())
            {
                CoreDBField __DBField = __PropertyInfo.GetCustomAttribute<CoreDBField>();
                if (__DBField != null)
                {
                    if (__DBField.IsDateTime)
                    {
                        __PropertyInfo.GetSetMethod().Invoke(this, new object[] { __DBField.GetDefaultValueCastedDateTime()});
                    }
                    else if (__DBField.IsDecimal)
                    {
                        __PropertyInfo.GetSetMethod().Invoke(this, new object[] { Convert.ToDecimal(__DBField.DefaultValue) });
                    }
                    else
                    {
                        __PropertyInfo.GetSetMethod().Invoke(this, new object[] { __DBField.DefaultValue });
                    }
                }
            }
        }

    }
}

