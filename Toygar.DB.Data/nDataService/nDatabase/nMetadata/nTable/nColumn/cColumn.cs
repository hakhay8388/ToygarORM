using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nTableOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable.nColumn;
using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.Boundary.nData;

namespace Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable.nColumn
{
    public class cColumn
    {
        cBaseTableOperationSQLCatalog TableOperationSQLCatalog { get; set; }
        public cTable Table { get; set; }
        public cColumnCoreEnitity ColumnEnitity { get; set; }
        public cIdentityCoreEnitity IdentityEnitity { get; set; }

        public cColumn(cTable _Table, cColumnCoreEnitity _ColumnEnitity, cIdentityCoreEnitity _IdentityEnitity)
        {
            Table = _Table;
            ColumnEnitity = _ColumnEnitity;
            IdentityEnitity = _IdentityEnitity;
            if (Table != null)
            {
                TableOperationSQLCatalog = Table.TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog;
            }
        }

        public cColumn(cTable _Table, string _ColumnName, EDataTypeClass _DataType, int _Length, bool _Nullable, bool _Identity = false, int _IdentityStart = 1, int _IdentityIncrement = 1, int _DecimalBigger = 0, int _DecimalLower = 0)
            : this(_Table, _DecimalLower != 0 && _DecimalBigger != 0 ? new cColumnCoreEnitity() { ColumnName = _ColumnName, DataTypeName = _DataType.Name, DataType = _DataType.ID, Precision = _DecimalBigger, Nullable = _Nullable, Scale = _DecimalLower.ToString() } : 
                  new cColumnCoreEnitity() { ColumnName = _ColumnName, DataTypeName = _DataType.Name, DataType = _DataType.ID, Length = _Length, Nullable = _Nullable}, null)
        {
            if (_Identity)
            {
                IdentityEnitity = new cIdentityCoreEnitity() { ColumnName = _ColumnName, IncrementValue = _IdentityIncrement, LastValue = 0, SeedValue = _IdentityStart, TableName = (_Table == null ? "" : _Table.TableEnitity.TableName), IsNotForReplication = 0, ObjectID = 0 };
            }
        }

        public cColumn(string _ColumnName, EDataTypeClass _DataType, int _Length, bool _Nullable, bool _Identity = false, int _IdentityStart = 1, int _IdentityIncrement = 1, int _DecimalBigger = 0, int _DecimalLower = 0)
            :this(null, _ColumnName, _DataType, _Length, _Nullable, _Identity, _IdentityStart , _IdentityIncrement, _DecimalBigger, _DecimalLower)
        {          
        }



        public EDataTypeClass DataType
        {
            get
            {
                return EDataTypeClass.GetByID(ColumnEnitity.DataType, EDataTypeClass.Varchar);
            }
        }

        public void AddColumn(Object _DefaultValue)
        {
            string __Defination = DataType.Name;
            if (DataType.UseLength)
            {
                if (ColumnEnitity.Length == -1)
                {
                    __Defination += "(max)";
                }
                else
                {
                    __Defination += "(" + ColumnEnitity.Length + ")";
                }
            }

            if (DataType.UseDecimalCount)
            {
                __Defination += "(" + ColumnEnitity.Precision + "," + ColumnEnitity.Scale + ")";
            }

            cSql __Sql = null;
            if (ColumnEnitity.Nullable)
            {
                __Sql = TableOperationSQLCatalog.SQLAddColumn(Table.TableEnitity.TableName, ColumnEnitity.ColumnName, __Defination);
            }
            else
            {
                if (typeof(DateTime).IsAssignableFrom(_DefaultValue.GetType()))
                {
                    __Sql = TableOperationSQLCatalog.SQLAddNotNullColumn(Table.TableEnitity.TableName, ColumnEnitity.ColumnName, __Defination, ((DateTime)_DefaultValue).ToString("yyyy.MM.dd HH:mm:ss"));
                }
                else
                {
                    __Sql = TableOperationSQLCatalog.SQLAddNotNullColumn(Table.TableEnitity.TableName, ColumnEnitity.ColumnName, __Defination, _DefaultValue.ToString());
                }                
            }
            Table.TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
        }

        public void Modify(Object _DefaultValue, bool _NeedUpdate)
        {
            string __Defination = DataType.Name;
            if (DataType.UseLength)
            {
                if (ColumnEnitity.Length==-1)
                {
                    __Defination += "(max)";
                }
                else
                {
                    __Defination += "(" + ColumnEnitity.Length + ")";
                }
            }

            if (DataType.UseDecimalCount)
            {
                __Defination += "(" + ColumnEnitity.Precision + "," + ColumnEnitity.Scale  + ")";
            }

            cSql __Sql = null;
            if (ColumnEnitity.Nullable)
            {
                __Sql = TableOperationSQLCatalog.SQLAlterColumn(Table.TableEnitity.TableName, ColumnEnitity.ColumnName, __Defination);
            }
            else
            {
                if (_NeedUpdate)
                {
                    __Sql = Table.TableManager.MetadataManager.Database.Catalogs.RowOperationSQLCatalog.SQLUpdateByCondition(Table.TableEnitity.TableName, ColumnEnitity.ColumnName + "=0", ColumnEnitity.ColumnName + " " + Table.TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.GetIsNullColumnString());
                    Table.TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
                    Table.TableManager.MetadataManager.Database.DefaultConnection.Commit();
                }

                if (IdentityEnitity != null)
                {
                    __Sql = TableOperationSQLCatalog.SQLAlterNotNullColumn(Table.TableEnitity.TableName, ColumnEnitity.ColumnName, __Defination, _DefaultValue.ToString(), true, IdentityEnitity.SeedValue, IdentityEnitity.IncrementValue);
                }
                else
                {
                    __Sql = TableOperationSQLCatalog.SQLAlterNotNullColumn(Table.TableEnitity.TableName, ColumnEnitity.ColumnName, __Defination, _DefaultValue.ToString());
                }

            }
            Table.TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
}

        public void Drop()
        {
            cSql __Sql = TableOperationSQLCatalog.SQLDropColumn(Table.TableEnitity.TableName, ColumnEnitity.ColumnName);
            Table.TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
            Table.ColumnList.Remove(this);
        }
    }
}
