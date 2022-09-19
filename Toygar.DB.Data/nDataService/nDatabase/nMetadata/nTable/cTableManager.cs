using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nTableOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable.nColumn;
using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable
{
    public class cTableManager
    {
        public cMetadataManager MetadataManager { get; set; }
        public List<cTable> TableList { get; set; }

        public cTableManager(cMetadataManager _MetadataManager)
        {
            MetadataManager = _MetadataManager;
        }

        public void Reload()
        {
            foreach (cTable __Item in TableList)
            {
				__Item.Reload();
            }
        }

		public void Create()
		{
			TableList = new List<cTable>();
			List<cTableCoreEnitity> __Table = MetadataManager.Database.Catalogs.DatabaseOperationsSQLCatalog.GetTables();
			foreach (cTableCoreEnitity __Item in __Table)
			{
				TableList.Add(new cTable(this, __Item));
			}
		}

		public void DropForeignKeys()
        {
            foreach (cTable __Item in TableList)
            {
                __Item.DropForeignKeys();
            }
        }

        public void DropDefaultConstraints()
        {
            foreach (cTable __Item in TableList)
            {
                __Item.DropDefaultConstraints();
            }
        }

        public void DropIndexes()
        {
            foreach (cTable __Item in TableList)
            {
                __Item.DropIndexes();
            }
        }

        public void DropTables()
        {
            foreach (cTable __Item in TableList)
            {
                __Item.Drop();
            }
        }


        public cTable GetTableByName(string _TableName)
        {
            List<cTable> __List = TableList.Where((_Table) =>
            {
                return _Table != null && _Table.TableEnitity != null && _Table.TableEnitity.TableName == _TableName;
            }).ToList();
            return __List.FirstOrDefault();
        }

        public cTable CreateTable(string _TableName, List<cColumn> _ColumnList)
        {
            string __Columns = "";
            foreach (cColumn __Column in _ColumnList)
            {
                __Columns += __Columns.IsNullOrEmpty() ? GetCreationString(__Column, MetadataManager.Database.Catalogs.TableOperationSQLCatalog) : " , " + GetCreationString(__Column, MetadataManager.Database.Catalogs.TableOperationSQLCatalog);
            }
            cSql __Sql = MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLAddTable(_TableName, __Columns);
            MetadataManager.Database.DefaultConnection.Execute(__Sql);
            cTableCoreEnitity __Table = MetadataManager.Database.Catalogs.DatabaseOperationsSQLCatalog.FindTable(_TableName);
            cTable __Result = new cTable(this, __Table);
            TableList.Add(__Result);
            return __Result;
        }

        public string GetCreationString(cColumn _Column, cBaseTableOperationSQLCatalog _TableOperationSQLCatalog)
        {
            string __Defination = _Column.ColumnEnitity.ColumnName + " " + _Column.DataType.Name;
            if (_Column.DataType.UseLength)
            {
                if (_Column.ColumnEnitity.Length == -1)
                {
                    __Defination += "(max)";
                }
                else
                {
                    __Defination += "(" + _Column.ColumnEnitity.Length + ")";
                }
            }

            if (_Column.DataType.UseDecimalCount)
            {
                __Defination += "(" + _Column.ColumnEnitity.Precision + "," + _Column.ColumnEnitity.Scale + ")";
            }


            if (_Column.IdentityEnitity != null)
            {
                __Defination += " " + _TableOperationSQLCatalog.GetIdentityString(_Column.IdentityEnitity.SeedValue, _Column.IdentityEnitity.IncrementValue);
            }

            if (!_Column.ColumnEnitity.Nullable || _Column.IdentityEnitity != null)
            {
                __Defination += " " + _TableOperationSQLCatalog.GetNotNullColumnString();
            }

            return __Defination;
        }
    }
}
