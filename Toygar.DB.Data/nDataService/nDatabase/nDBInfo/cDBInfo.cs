using Toygar.DB.Data.nDataService.nDatabase.nConnection;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toygar.DB.Data.nDataService.nDatabase;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities;

namespace Toygar.DB.Data.nDataService.nDatabase.nDBInfo
{
    public class cDBInfo : cBaseDatabaseComponent
    {
        int m_MainVersion = 0;
        int m_DBVersion = 0;
        int m_ExtensitionVersion = 0;

        public int MainVersion
        { 
            get 
            { 
                LoadVersion(); 
                return m_MainVersion; 
            } 
            set 
            { 
                cDBInfoEntity __DBInfoEntity = GetVersion(); 
                if (__DBInfoEntity.IsValid) __DBInfoEntity.MainVersion = value; 
                else { 
                    __DBInfoEntity.MainVersion = value; 
                    __DBInfoEntity.ExtensitionVersion = 1; 
                    __DBInfoEntity.DBVersion = 1; 
                }
                __DBInfoEntity.Save(); 
            }
        }

        public int DBVersion 
        { 
            get 
            { 
                LoadVersion(); 
                return m_DBVersion; 
            } 
            set 
            { 
                cDBInfoEntity __DBInfoEntity = GetVersion();
                if (__DBInfoEntity.IsValid) __DBInfoEntity.DBVersion = value; 
                else 
                { 
                    __DBInfoEntity.DBVersion = value; 
                    __DBInfoEntity.ExtensitionVersion = 1; 
                    __DBInfoEntity.MainVersion = 1; 
                }
                __DBInfoEntity.Save(); 
            }
        }

        public int ExtensitionVersion 
        {
            get 
            { 
                LoadVersion(); 
                return m_ExtensitionVersion; 
            } 
            set 
            { 
                cDBInfoEntity __DBInfoEntity = GetVersion(); 
                if (__DBInfoEntity.IsValid) __DBInfoEntity.ExtensitionVersion = value; 
                else 
                { 
                    __DBInfoEntity.ExtensitionVersion = value; 
                    __DBInfoEntity.DBVersion = 1; 
                    __DBInfoEntity.MainVersion = 1; 
                }
                __DBInfoEntity.Save(); 
            } 
        }

        public cDBInfo(IDatabase _Database)
            :base(_Database)
        {
        }
        
        public void LoadVersion()
        {
            if (!Database.ControlDBConnection())
            {
                return;
            }
            cBaseConnection __Connection = Database.CustomConnectionPoolingManager.DefaultConnection;

            cEntityTable __Table = Database.EntityManager.GetEntityTableByEnitityType<cDBInfoEntity>();

            cTableCoreEnitity __CoreEntity = Database.Catalogs.Database.Catalogs.DatabaseOperationsSQLCatalog.FindTable(__Table.TableName);
            if (__CoreEntity == null)
            {
                Database.CustomConnectionPoolingManager.RemoveConnection(__Connection);
                return;
            }


            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelect(__Table.TableName, "ID=:ID");
            __Sql.SetParameter("ID", 1);

            try
            {
                DataTable __DataTable = __Connection.Query(__Sql);
                if (__DataTable.Rows.Count > 0)
                {
                    m_MainVersion = Convert.ToInt32(__DataTable.Rows[0]["MainVersion"].ToString());
                    m_DBVersion = Convert.ToInt32(__DataTable.Rows[0]["DBVersion"].ToString());
                    m_ExtensitionVersion = Convert.ToInt32(__DataTable.Rows[0]["ExtensitionVersion"].ToString());
                }
                else
                {
                    Reset();
                }
                __Connection.Commit();
                __Connection.Release();
            }
            catch (Exception _Ex)
            {
				Database.App.Loggers.SqlLogger.LogError(_Ex);
				Reset();
                __Connection.Commit();
                __Connection.Release();
            }
            Database.CustomConnectionPoolingManager.RemoveConnection(__Connection);
        }
        
        private void Reset()
        {
            m_DBVersion = -1;
            m_MainVersion = -1;
            m_ExtensitionVersion = -1;
        }

        private cDBInfoEntity GetVersion()
        {
            cDBInfoEntity __DBInfoEntity = Database.GetEntityByID_Or_CreateNew<cDBInfoEntity>(1);
            return __DBInfoEntity;
        }
    }
}
