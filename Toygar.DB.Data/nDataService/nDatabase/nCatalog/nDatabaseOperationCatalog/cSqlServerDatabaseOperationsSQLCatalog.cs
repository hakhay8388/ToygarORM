using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog
{
    public class cSqlServerDatabaseOperationsSQLCatalog : cBaseDatabaseOperationsSQLCatalog
    {
        public cSqlServerDatabaseOperationsSQLCatalog(IDatabase _Database)
            : base(_Database)
        { 
        }

        public override cTableCoreEnitity FindTable(string _TableName)
        {
            cSql __Sql = CreateSql("SELECT SYSOBJECTS.NAME TableName, SYSOBJECTS.id ObjectID FROM SYSOBJECTS WHERE TYPE='U' AND Name=:TableName ORDER BY  SYSOBJECTS.NAME");
            __Sql.SetParameter("TableName", _TableName);
            DataTable __Table = Database.DefaultConnection.Query(__Sql);
            List<cTableCoreEnitity> __Items =  __Table.ToList<cTableCoreEnitity>();
            return __Items.Count > 0 ? __Items[0] : null;
        }
        public override List<cTableCoreEnitity> GetTables()
        {
            cSql __Sql = CreateSql("SELECT SYSOBJECTS.NAME TableName, SYSOBJECTS.id ObjectID FROM SYSOBJECTS WHERE TYPE='U' ORDER BY TableName");
            DataTable __Table = Database.DefaultConnection.Query(__Sql);
            return __Table.ToList<cTableCoreEnitity>();
        }

		public override List<cTableCoreEnitity> GetLockedTables()
		{
			cSql __Sql = CreateSql(@"SELECT dm_tran_locks.request_session_id,
										   dm_tran_locks.resource_database_id,
										   DB_NAME(dm_tran_locks.resource_database_id) AS dbname,
										   CASE
											   WHEN resource_type = 'OBJECT'
												   THEN OBJECT_NAME(dm_tran_locks.resource_associated_entity_id)
											   ELSE OBJECT_NAME(partitions.OBJECT_ID)
										   END AS TableName,
										   partitions.OBJECT_ID AS ObjectID,
										   partitions.index_id,
										   indexes.name AS index_name,
										   dm_tran_locks.resource_type,
										   dm_tran_locks.resource_description,
										   dm_tran_locks.resource_associated_entity_id,
										   dm_tran_locks.request_mode,
										   dm_tran_locks.request_status
											FROM sys.dm_tran_locks
											LEFT JOIN sys.partitions ON partitions.hobt_id = dm_tran_locks.resource_associated_entity_id
											LEFT JOIN sys.indexes ON indexes.OBJECT_ID = partitions.OBJECT_ID AND indexes.index_id = partitions.index_id
											WHERE resource_associated_entity_id > 0
											  AND resource_database_id = DB_ID()
											  AND resource_type = 'KEY'
											ORDER BY request_session_id, resource_associated_entity_id
				");
			DataTable __Table = Database.DefaultConnection.Query(__Sql);
			return __Table.ToList<cTableCoreEnitity>();
		}

		public override List<cColumnCoreEnitity> GetColumnsByTableName(string _Name)
        {
            cSql __Sql = CreateSql("SELECT T.id ObjectID, T.NAME TableName, C.NAME ColumnName, Y.NAME DataTypeName, Y.XTYPE DataType, C.LENGTH Length, C.PREC Precision, C.SCALE Scale, C.ISNULLABLE Nullable FROM SYSCOLUMNS C, SYSOBJECTS T, SYSTYPES Y WHERE T.ID=C.ID AND T.TYPE='U' AND Y.XTYPE=C.XTYPE AND Y.NAME <> 'sysname' AND T.NAME=:TableName ORDER BY TableName, ColumnName");
            __Sql.SetParameter("TableName", _Name);
            DataTable __Table = Database.DefaultConnection.Query(__Sql);
            return __Table.ToList<cColumnCoreEnitity>();
        }

        public override List<cIndexCoreEnitity> GetIndexesByTableName(string _Name)
        {
            cSql __Sql = CreateSql(@"select 
                                        I.object_id ObjectID,
                                        I.name IndexName,
                                        I.index_id IndexID,
                                        I.type Type,
                                        I.type_desc TypeDesc,
                                        I.is_unique IsUnique,
                                        I.is_primary_key IsPrimaryKey,
                                        I.is_unique_constraint IsUniqueConstraint

                                   from SYS.indexes I 
                                   inner join sys.objects O on O.object_id = I.object_id and O.type = 'U' 
                                   where I.name is not null AND O.name=:TableName ");

            __Sql.SetParameter("TableName", _Name);
            DataTable __Table = Database.DefaultConnection.Query(__Sql);
            return __Table.ToList<cIndexCoreEnitity>();
        }

        public override List<cIndexColumnCoreEnitity> GetIndexConstraintColumnByIndexName(string _IndexName)
        {
            cSql __Sql = CreateSql(@"select 

                                        I.object_id ObjectID,
                                        I.name IndexName,
                                        I.index_id IndexID,
                                        I.type Type,
                                        I.type_desc TypeDesc,
                                        I.is_unique IsUnique,
                                        I.is_primary_key IsPrimaryKey,
                                        I.is_unique_constraint IsUniqueConstraint,
										C.name ColumnName,
										C.column_id ColumnID,
										CI.key_ordinal KeyOrdinal
                                   from SYS.indexes I 
                                   inner join sys.objects O on O.object_id = I.object_id and O.type = 'U' 
								   inner join sys.index_columns CI on CI.object_id = I.object_id
								   left join sys.columns C on C.column_id = CI.column_id and C.object_id = O.object_id
                                   where I.name is not null and I.name = :IndexName ORDER BY KeyOrdinal");

            __Sql.SetParameter("IndexName", _IndexName);
            DataTable __Table = Database.DefaultConnection.Query(__Sql);
            return __Table.ToList<cIndexColumnCoreEnitity>();
        }

        public override List<cForeignKeyCoreEnitity> GetForegnKeyByParentTableName(string _TableName)
        {
            cSql __Sql = CreateSql(@"select F.object_id ObjectID, C1.name ParentColumnName, C2.name ReferencedColumnName, O2.name ReferencedTableName, O1.name ParentTableName, F.name ForeignKeyName from sys.foreign_keys F
                                    inner join  sys.foreign_key_columns FC on FC.constraint_object_id = F.object_id
                                    inner join  sys.objects O1 on O1.object_id=FC.parent_object_id
                                    inner join  sys.objects O2 on O2.object_id=FC.referenced_object_id
                                    inner join  sys.columns C1 on C1.column_id=FC.parent_column_id and C1.object_id=O1.object_id
                                    inner join  sys.columns C2 on C2.column_id=FC.referenced_column_id and C2.object_id=O2.object_id
                                    where O1.name = :TableName");

            __Sql.SetParameter("TableName", _TableName);
            DataTable __Table = Database.DefaultConnection.Query(__Sql);
            return __Table.ToList<cForeignKeyCoreEnitity>();
        }

        public override List<cForeignKeyCoreEnitity> GetForegnKeyByReferencedTableName(string _TableName)
        {
            cSql __Sql = CreateSql(@"select F.object_id ObjectID, C1.name ParentColumnName, C2.name ReferencedColumnName, O2.name ReferencedTableName, O1.name ParentTableName, F.name ForeignKeyName  from sys.foreign_keys F
                                    inner join  sys.foreign_key_columns FC on FC.constraint_object_id = F.object_id
                                    inner join  sys.objects O1 on O1.object_id=FC.parent_object_id
                                    inner join  sys.objects O2 on O2.object_id=FC.referenced_object_id
                                    inner join  sys.columns C1 on C1.column_id=FC.parent_column_id and C1.object_id=O1.object_id
                                    inner join  sys.columns C2 on C2.column_id=FC.referenced_column_id and C2.object_id=O2.object_id
                                    where O2.name = :TableName");

            __Sql.SetParameter("TableName", _TableName);
            DataTable __Table = Database.DefaultConnection.Query(__Sql);
            return __Table.ToList<cForeignKeyCoreEnitity>();
        }

        public override List<cIdentityCoreEnitity> GetIdentityByTableName(string _TableName)
        {
            cSql __Sql = CreateSql(@"SELECT   OBJECT_NAME(OBJECT_ID) TableName, 
                                         NAME ColumnName, 
                                         SEED_VALUE SeedValue, 
                                         INCREMENT_VALUE IncrementValue, 
                                         LAST_VALUE LastValue, 
                                         IS_NOT_FOR_REPLICATION IsNotForReplication 
                                         FROM sys.identity_columns 
                                         WHERE OBJECT_NAME(OBJECT_ID)=:TableName");

            __Sql.SetParameter("TableName", _TableName);
            DataTable __Table = Database.DefaultConnection.Query(__Sql);
            return __Table.ToList<cIdentityCoreEnitity>();
        }

        public override string ParseErrorMessageConstraintName(string _ErrorMessage)
        {
            string __ConstraintName = null;

            foreach (string item in new string[] { "FK_", "EX_", "PK_" })
            {
                int i1 = _ErrorMessage.IndexOf(item);

                if (i1 > 0)
                {
                    if (_ErrorMessage.Length >= i1 + 28)
                    {
                        __ConstraintName = _ErrorMessage.Substring(i1, 28);
                        break;
                    }
                }
            }

            return __ConstraintName;
        }

        public override List<cDefaultConstraintCoreEnitity> GetDefaultConstraintsByTable(string _TableName)
        {
            cSql __Sql = CreateSql(@"select sys.objects.name TableName, sys.default_constraints.name ConstraintName from sys.objects
                                    inner join sys.default_constraints on sys.objects.object_id = sys.default_constraints.parent_object_id
                                    where sys.objects.name = :TableName");

            __Sql.SetParameter("TableName", _TableName);
            DataTable __Table = Database.DefaultConnection.Query(__Sql);
            return __Table.ToList<cDefaultConstraintCoreEnitity>();       
        }

        public override void SetDbLevelParams(string _DBName)
        {
            cSql __Sql = CreateSql(@"Alter Database " + _DBName + " set RECOVERY SIMPLE, READ_COMMITTED_SNAPSHOT ON WITH ROLLBACK IMMEDIATE");
            Database.DefaultConnection.Execute(__Sql);
            bool __InUse = Database.DefaultConnection.InUse;
        }

        public override void CreateDatabase(string _DBName)
        {
            cSql __Sql = CreateSql(@"CREATE DATABASE " + _DBName + " COLLATE Turkish_CI_AS");
            Database.DefaultConnection.Execute(__Sql);
        }

        public override void DropDatabase(string _DBName)
        {
            /*
            string __SqlCreateDBQuery;
            try
            {
                __SqlCreateDBQuery = "ALTER DATABASE " + _DBName + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE \n" +
                                    "ALTER DATABASE " + _DBName + " SET MULTI_USER";

                SqlCommand __SqlCmd = new SqlCommand(__SqlCreateDBQuery, GlobalConnection);
                __SqlCmd.ExecuteNonQuery();
                __SqlCreateDBQuery = "DROP DATABASE " + _DBName;
                __SqlCmd = new SqlCommand(__SqlCreateDBQuery, GlobalConnection);
                __SqlCmd.ExecuteNonQuery();
            }
            catch (Exception _Ex)
            {
                MessageBox.Show(_Ex.Message);
            }*/
        }

        public override bool IsDatabaseExists(string _DBName)
        {
            cSql __Sql = CreateSql(@"SELECT * FROM sysdatabases where name = :DBName");
            __Sql.SetParameter("DBName", _DBName);
            DataTable __Table = Database.DefaultConnection.Query(__Sql);
            return __Table.Rows.Count > 0;
        }
    }
}
