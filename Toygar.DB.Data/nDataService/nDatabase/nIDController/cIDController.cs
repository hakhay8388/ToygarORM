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

namespace Toygar.DB.Data.nDataService.nDatabase.nIDController
{
    public class cIDController : cBaseDatabaseComponent
    {
        public cIDController(IDatabase _Database)
            :base(_Database)
        {
        }

        public long GetNewEntityID(Type _Type, long _ID = -1)
        {
            if (Database.GetEntityType().IsAssignableFrom(_Type))
            {
                Type __Type = GetType();
                MethodInfo __MethodInfo = __Type.GetMethod("GetNewEntityID", new Type[] { typeof(long) });
                __MethodInfo = __MethodInfo.MakeGenericMethod(new Type[] { _Type });
                long __Temp = (long)__MethodInfo.Invoke(this, new object[] { _ID });
                return __Temp;
            }
            else
            {
                throw new Exception("cEntityManager->GetEntityByColumnValue");
            }
        
        }

        public long GetNewEntityID<TEntity>(long _ID = -1) where TEntity : cBaseEntity
        {
            lock (typeof(TEntity))
            {
                Exception __Exception = null;
                for (int i = 0; i < 10; i++)
                {
                    cBaseConnection __Connection = Database.CustomConnectionPoolingManager.DefaultConnection;
                    try
                    {

                        cEntityTable __Table = Database.EntityManager.GetEntityTableByEnitityType<TEntity>();
                        cEntityTable __IDCounterTable = Database.EntityManager.GetEntityTableByEnitityType<cIDCounterEntity>();

                        cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelect(__IDCounterTable.TableName, "TableName=:TableName");
                        __Sql.SetParameter("TableName", __Table.TableName);
                        DataTable __DataTable = __Connection.Query(__Sql);
                        long __CurrentCount = 1;
                        if (__DataTable.Rows.Count < 1)
                        {
                            __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLInsertSingleRow(__IDCounterTable.TableName, "TableName, Count, CreateDate, UpdateDate", ":TableName, :Count, :CreateDate, :UpdateDate");
                            __Sql.SetParameter("TableName", __Table.TableName);
                            __Sql.SetParameter("Count", __CurrentCount);
                            __Sql.SetParameter("CreateDate", DateTime.Now);
                            __Sql.SetParameter("UpdateDate", DateTime.Now);
                            __Connection.Execute(__Sql);
                            __Connection.Commit();
                            __Connection.Release();
                            return __CurrentCount;
                        }
                        else if (__DataTable.Rows.Count == 1)
                        {
                            __CurrentCount = Convert.ToInt32(__DataTable.Rows[0]["Count"]);
                            if (__CurrentCount + 1 < _ID) __CurrentCount = _ID;
                            else __CurrentCount++;
                            __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLUpdateByCondition(__IDCounterTable.TableName, "Count=:Count, UpdateDate=:UpdateDate", "TableName=:TableName");
                            __Sql.SetParameter("TableName", __Table.TableName);
                            __Sql.SetParameter("Count", __CurrentCount);
                            __Sql.SetParameter("UpdateDate", DateTime.Now);
                            __Connection.Execute(__Sql);
                            __Connection.Commit();
                            __Connection.Release();
                            return __CurrentCount;
                        }
                        else
                        {
                            __Exception = new Exception("cIDController->GetNewEntityID");
                        }
                    }
                    catch(Exception _Ex)
                    {
						Database.App.Loggers.SqlLogger.LogError(_Ex);
						if (__Exception != null)
                        {
                            throw __Exception;
                        }
                    }
                }
                throw new Exception("cIDController->GetNewEntityID");
            }
        }
    }
}
