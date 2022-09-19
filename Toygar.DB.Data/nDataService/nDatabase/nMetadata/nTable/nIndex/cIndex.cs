using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable.nIndex.nIndexColumn;
using Toygar.DB.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable.nIndex
{
    public class cIndex 
    {
        public cTable Table { get; set; }
        public cIndexCoreEnitity IndexEnitity { get; set; }
        public List<cIndexColumn> IndexColumnList { get; set; }

        public cIndex(cTable _Table, cIndexCoreEnitity _IndexEnitity)
        {
            Table = _Table;
            IndexEnitity = _IndexEnitity;
            Reload();
        }

        public void Drop()
        {
            if (IndexEnitity.IsPrimaryKey)
            {
                cSql __Sql = Table.TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLDropConstraint(Table.TableEnitity.TableName, IndexEnitity.IndexName);
                Table.TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
            }
            else
            {
                if (IndexEnitity.IsUniqueConstraint)
                {
                    cSql __Sql = Table.TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLDropConstraint(Table.TableEnitity.TableName, IndexEnitity.IndexName);
                    Table.TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
                }
                else
                {
                    cSql __Sql = Table.TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLDropIndex(Table.TableEnitity.TableName, IndexEnitity.IndexName);
                    Table.TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
                }
            }
            Table.IndexList.Remove(this);
        }

        public void Rebuild(List<cSql> _SqlList, bool _ExecuteSql)
        {
            cSql __Sql = Table.TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLRebuildIndex(Table.TableEnitity.TableName, IndexEnitity.IndexName);
            _SqlList.Add(__Sql);
            if (_ExecuteSql)
            {
                Table.TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
            }
        }


        private void Reload()
        {
            IndexColumnList = new List<cIndexColumn>();
            List<cIndexColumnCoreEnitity> __CoreEntityList = Table.TableManager.MetadataManager.Database.Catalogs.DatabaseOperationsSQLCatalog.GetIndexConstraintColumnByIndexName(IndexEnitity.IndexName);
            foreach (cIndexColumnCoreEnitity __Item in __CoreEntityList)
            {
                IndexColumnList.Add(new cIndexColumn(this, __Item));
            }
        }
    }
}
