using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable.nColumn;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable.nDefaultContraint;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable.nForeignKey;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable.nIndex;
using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.Base.Boundary.nData;

namespace Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable
{
    public class cTable
    {
        public int MaxForeignKeyNameLength = 110;
        public List<cColumn> ColumnList { get; set; }
        public List<cIndex> IndexList { get; set; }
        public List<cForeignKey> ParentedForeignKeyList { get; set; }
        public List<cForeignKey> ReferencedForeignKeyList { get; set; }
        public List<cDefaultContraint> DefaultContraintList { get; set; }
        public cTableManager TableManager { get; set; }
        public cTableCoreEnitity TableEnitity { get; set; }

        public cTable(cTableManager _TableManager, cTableCoreEnitity _TableEnitity)
        {
            TableManager = _TableManager;
            TableEnitity = _TableEnitity;
            if (_TableEnitity != null)
            {
                Reload();
            }
        }

        public cColumn GetColumnByName(string _ColumnName)
        {
            return ColumnList.Where((_Column) => _Column.ColumnEnitity.ColumnName == _ColumnName).ToList().FirstOrDefault();
        }

        public void DropForeignKeys()
        {
            for (int i = ParentedForeignKeyList.Count - 1; i > -1; i--)
            {
                ParentedForeignKeyList[i].Drop();
            }
        }

        public void DropDefaultConstraints()
        {
            for (int i = DefaultContraintList.Count - 1; i > -1; i--)
            {
                DefaultContraintList[i].Drop();
            }
        }

        public void DropIndexes()
        {
            /*for (int i = ReferencedForeignKeyList.Count - 1; i > -1; i--)
            {
                cTable __Table = TableManager.GetTableByName(ReferencedForeignKeyList[i].ForeignKeyEnitity.ParentTableName);
                //if (__Table != null)
                //{
                for (int j = __Table.ParentedForeignKeyList.Count - 1; j > -1; j--)
                {
                    if (ReferencedForeignKeyList[i].ForeignKeyEnitity.ForeignKeyName == __Table.ParentedForeignKeyList[j].ForeignKeyEnitity.ForeignKeyName)
                    {
                        __Table.ParentedForeignKeyList[j].Drop();
                        break;
                    }
                }
                //}
            }*/

            for (int i = IndexList.Count - 1; i > -1; i--)
            {
                IndexList[i].Drop();
            }
        }

        public void Drop()
        {
            DropForeignKeys();
            DropIndexes();
            DropDefaultConstraints();

            cSql __Sql = TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLDropTable(TableEnitity.TableName);
            TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
            TableManager.TableList.Remove(this);
        }

        public void Reload()
        {
            ReloadColumns();
            ReloadIndexes();
            ReloadParentedForeignKey();
            ReloadReferencedForeignKey();
            ReloadDefaultContraints();
        }

        private void ReloadDefaultContraints()
        {
            DefaultContraintList = new List<cDefaultContraint>();
            List<cDefaultConstraintCoreEnitity> __CoreEntityList = TableManager.MetadataManager.Database.Catalogs.DatabaseOperationsSQLCatalog.GetDefaultConstraintsByTable(TableEnitity.TableName);
            foreach (cDefaultConstraintCoreEnitity __Item in __CoreEntityList)
            {
                DefaultContraintList.Add(new cDefaultContraint(this, __Item));
            }
        }

        private void ReloadColumns()
        {
            ColumnList = new List<cColumn>();
            List<cColumnCoreEnitity> __CoreEntityList = TableManager.MetadataManager.Database.Catalogs.DatabaseOperationsSQLCatalog.GetColumnsByTableName(TableEnitity.TableName);
            cIdentityCoreEnitity __IdentityCoreEntity = TableManager.MetadataManager.Database.Catalogs.DatabaseOperationsSQLCatalog.GetIdentityByTableName(TableEnitity.TableName).FirstOrDefault();

            foreach (cColumnCoreEnitity __Item in __CoreEntityList)
            {
                if (__IdentityCoreEntity != null)
                {
                    if (__IdentityCoreEntity.ColumnName == __Item.ColumnName)
                    {
                        ColumnList.Add(new cColumn(this, __Item, __IdentityCoreEntity));
                    }
                    else
                    {
                        ColumnList.Add(new cColumn(this, __Item, null));
                    }
                }
                else
                {
                    ColumnList.Add(new cColumn(this, __Item, null));
                }

            }
        }


        private void ReloadIndexes()
        {
            IndexList = new List<cIndex>();
            List<cIndexCoreEnitity> __CoreEntityList = TableManager.MetadataManager.Database.Catalogs.DatabaseOperationsSQLCatalog.GetIndexesByTableName(TableEnitity.TableName);
            foreach (cIndexCoreEnitity __Item in __CoreEntityList)
            {
                IndexList.Add(new cIndex(this, __Item));
            }
        }

        private void ReloadParentedForeignKey()
        {
            ParentedForeignKeyList = new List<cForeignKey>();
            List<cForeignKeyCoreEnitity> __CoreEntityList = TableManager.MetadataManager.Database.Catalogs.DatabaseOperationsSQLCatalog.GetForegnKeyByParentTableName(TableEnitity.TableName);
            foreach (cForeignKeyCoreEnitity __Item in __CoreEntityList)
            {
                ParentedForeignKeyList.Add(new cForeignKey(this, __Item));
            }
        }

        private void ReloadReferencedForeignKey()
        {
            ReferencedForeignKeyList = new List<cForeignKey>();
            List<cForeignKeyCoreEnitity> __CoreEntityList = TableManager.MetadataManager.Database.Catalogs.DatabaseOperationsSQLCatalog.GetForegnKeyByReferencedTableName(TableEnitity.TableName);
            foreach (cForeignKeyCoreEnitity __Item in __CoreEntityList)
            {
                ReferencedForeignKeyList.Add(new cForeignKey(this, __Item));
            }
        }

        public void AddPrimaryKey(List<cColumn> _ColumnList)
        {
            string __Columns = "";
            string __PKColumns = "";
            foreach (cColumn __Column in _ColumnList)
            {
                if (__Column.ColumnEnitity.Nullable) throw new Exception(string.Format("'PrimaryKey' yapılmaya çalışılan '{0}' kolonu Nullable olamaz!!!", __Column.ColumnEnitity.ColumnName.ToString()));
                __Columns += __Columns.IsNullOrEmpty() ? __Column.ColumnEnitity.ColumnName : ", " + __Column.ColumnEnitity.ColumnName;
                __PKColumns += __Columns.IsNullOrEmpty() ? __Column.ColumnEnitity.ColumnName : "_" + __Column.ColumnEnitity.ColumnName;
            }
            string __Name = "PK_{0}_{1}_{2}";

            __Name = __Name.FormatEx(TableEnitity.TableName, __PKColumns, TableManager.MetadataManager.Database.App.Handlers.HashHandler.GetHashValue(DateTime.Now.Ticks.ToString()));
            if (__Name.Length > MaxForeignKeyNameLength)
            {
                __Name = __Name.Substring(0, MaxForeignKeyNameLength);
            }
            cSql __Sql = TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLAddPrimaryConstraint(TableEnitity.TableName, __Name, __Columns);
            TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
        }

        public void AddColumn(string _ColumnName, EDataTypeClass _DataType, int _Length, bool _Nullable, Object _Value = null, bool _Identity = false, int _IdentityStart = 1, int _IdentityIncrement = 1, int _DecimalBigger = 0, int _DecimalLower = 0)
        {
            cColumn __Column = new cColumn(this, _ColumnName, _DataType, _Length, _Nullable, _Identity, _IdentityStart, _IdentityIncrement, _DecimalBigger, _DecimalLower);
            if (!_Nullable && _Value == null)
            {
                throw new Exception("Not Null olan bir kolona(" + _ColumnName + ")  varsayılan bir değer gönderilmeli!!!");
            }
            __Column.AddColumn(_Value);
        }

        public void ModifyColumn(bool _NeedUpdate, string _ColumnName, EDataTypeClass _DataType, int _Length, bool _Nullable, Object _Value = null, bool _Identity = false, int _IdentityStart = 1, int _IdentityIncrement = 1, int _DecimalBigger = 0, int _DecimalLower = 0)
        {
            cColumn __Column = new cColumn(this, _ColumnName, _DataType, _Length, _Nullable, _Identity, _IdentityStart, _IdentityIncrement, _DecimalBigger, _DecimalLower);
            if (!_Nullable && _Value == null)
            {
                throw new Exception("Not Null olan bir kolon(" + this.TableEnitity.TableName + " ." + _ColumnName + ") varsayılan bir değer gönderilmeli!!!");
            }
            __Column.Modify(_Value, _NeedUpdate);
        }

        public void AddForeignKey(cColumn _ParentedColumn, cColumn _ReferencedColumn)
        {
            _ParentedColumn = ColumnList.Where((_Column) => _Column == _ParentedColumn).FirstOrDefault();
            if (_ParentedColumn == null) throw new Exception(string.Format("'Foreign' yapılmaya çalışılan ParentedColumn '{0}' kolonu {1} tablosunda bulnumadı!!!", _ParentedColumn.ColumnEnitity.ColumnName, _ParentedColumn.Table.TableEnitity.TableName));
            string __Name = "FK_{0}_{1}_{2}";
            __Name = __Name.FormatEx(TableEnitity.TableName, _ParentedColumn.ColumnEnitity.ColumnName, TableManager.MetadataManager.Database.App.Handlers.HashHandler.GetHashValue(DateTime.Now.Ticks.ToString()));
            if (__Name.Length > MaxForeignKeyNameLength)
            {
                __Name = __Name.Substring(0, MaxForeignKeyNameLength);
            }
            cSql __Sql = TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLAddForeignKey(__Name, TableEnitity.TableName, _ParentedColumn.ColumnEnitity.ColumnName, _ReferencedColumn.Table.TableEnitity.TableName, _ReferencedColumn.ColumnEnitity.ColumnName);
            TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
        }

        public void AddUniqueKey(List<cColumn> _ColumnList, bool _Clustered)
        {
            string __Columns = "";
            string __UKColumns = "";
            foreach (cColumn __Column in _ColumnList)
            {
                if (__Column.ColumnEnitity.Nullable) throw new Exception(string.Format("'UniqueKey' yapılmaya çalışılan '{0}' kolonu Nullable olamaz!!!", __Column.ColumnEnitity.ColumnName.ToString()));
                __Columns += __Columns.IsNullOrEmpty() ? __Column.ColumnEnitity.ColumnName : ", " + __Column.ColumnEnitity.ColumnName;
                __UKColumns += __Columns.IsNullOrEmpty() ? __Column.ColumnEnitity.ColumnName : "_" + __Column.ColumnEnitity.ColumnName;
            }
            string __Name = "UK_{0}_{1}_{2}";
            __Name = __Name.FormatEx(TableEnitity.TableName, __UKColumns, TableManager.MetadataManager.Database.App.Handlers.HashHandler.GetHashValue(DateTime.Now.Ticks.ToString()));
            if (__Name.Length > MaxForeignKeyNameLength)
            {
                __Name = __Name.Substring(0, MaxForeignKeyNameLength);
            }
            cSql __Sql = TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLAddUniqueConstraint(TableEnitity.TableName, __Name, _Clustered, __Columns);
            TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
        }

        public void AddIndexKey(List<cColumn> _ColumnList, bool _Unique, bool _Clustered)
        {
            string __Columns = "";
            string __UKColumns = "";
            foreach (cColumn __Column in _ColumnList)
            {
                if (__Column.ColumnEnitity.Nullable) throw new Exception(string.Format("'IndexK' yapılmaya çalışılan '{0}' kolonu Nullable olamaz!!!", __Column.ColumnEnitity.ColumnName.ToString()));
                __Columns += __Columns.IsNullOrEmpty() ? __Column.ColumnEnitity.ColumnName : ", " + __Column.ColumnEnitity.ColumnName;
                __UKColumns += __Columns.IsNullOrEmpty() ? __Column.ColumnEnitity.ColumnName : "_" + __Column.ColumnEnitity.ColumnName;
            }
            string __Name = "IK_{0}_{1}_{2}";
            __Name = __Name.FormatEx(TableEnitity.TableName, __UKColumns, TableManager.MetadataManager.Database.App.Handlers.HashHandler.GetHashValue(DateTime.Now.Ticks.ToString()));
            if (__Name.Length > MaxForeignKeyNameLength)
            {
                __Name = __Name.Substring(0, MaxForeignKeyNameLength);
            }

            cSql __Sql = TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLAddIndex(TableEnitity.TableName, __Name, _Unique, _Clustered, __Columns);
            TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
        }
    }
}
