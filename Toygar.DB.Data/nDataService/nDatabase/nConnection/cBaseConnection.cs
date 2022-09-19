using Toygar.Base.Boundary.nCore.nBootType;
using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nHandlers.nContextHandler;
using Toygar.DB.Data.nConfiguration;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Toygar.DB.Data.nDataService.nDatabase.nConnection
{
	public abstract class cBaseConnection
	{
		cDataConfiguration DataConfiguration { get; set; }
		protected cConnectionPoolingManager ConnectionPoolingManager { get; set; }
		protected IDbConnection DBConnection { get; set; }
		protected IDbTransaction DBTransaction { get; set; }
		bool m_InUse { get; set; }
		public DateTime LastUsedTime { get; set; }
		public bool InUse
		{
			get
			{
				return m_InUse;
			}
			set
			{
				if (value)
				{
					LastUsedTime = DateTime.Now;
				}
				Console.WriteLine("Connection ID : " + ID + " , InUse : " + value);
				m_InUse = value;
			}
		}

		protected abstract IDbConnection NewConnection();
		protected abstract IDbCommand NewDbCommand();
		protected abstract IDbDataAdapter NewDbDataAdapter();
		public abstract string GetParameterMarker();
		protected abstract void AfterOpen();

		protected int AffectedRowsCount { get; set; }

		static int IDCounter = 0;

		public int ID = 0;

		public List<cSql> ExecutedList { get; set; }

		public cBaseConnection(cConnectionPoolingManager _ConnectionPoolingManager)
		{
			ExecutedList = new List<cSql>();
            DataConfiguration = cApp.App.Cfg<cDataConfiguration>();
			InUse = false;
			DBConnection = NewConnection();
			ConnectionPoolingManager = _ConnectionPoolingManager;
			DBConnection.ConnectionString = ConnectionPoolingManager.Database.ServiceContext.ConnectionString;
			if (DBConnection.ConnectionString.IsNullOrEmpty()) throw new Exception("DB Bağlantı ayarları setlenmemiş");
			Open();
			IDCounter++;
			ID = IDCounter;
		}

		public cSql CreateSql(string _SQLString)
		{
			return new cSql(this, _SQLString);
		}

		public string ConnectionString
		{
			get
			{
				return ConnectionPoolingManager.DefaultConnection.ConnectionString;
			}
		}

		public bool IsOpen
		{
			get
			{
				return DBConnection.State == ConnectionState.Open;
			}
		}

		public void Open()
		{
			if (!IsOpen)
			{
				Console.WriteLine("Opening connection... ConnectionString:" + DBConnection.ConnectionString);
				DBConnection.Open();
				AfterOpen();
				BeginTransaction();
				Console.WriteLine("Opened");
			}
		}

		public void CoreOpen()
		{
			DBConnection.Open();
		}

		public void Close()
		{
			try
			{
				Console.WriteLine("Closing connection... ConnectionString:" + DBConnection.ConnectionString);
				try
				{
					if (DBTransaction != null)
					{
						DBTransaction.Rollback();
					}
				}
				catch(Exception _Ex2)
				{
				}
				DBConnection.Close();
				Console.WriteLine("Closed");
			}
			catch (Exception _Ex)
			{
				ConnectionPoolingManager.Database.App.Loggers.SqlLogger.LogError(new List<string>() { "Connection.Close Failed" }, _Ex, new List<string>() { });
				Console.WriteLine("Connection.Close Failed");
				Console.WriteLine(_Ex.ToString());
			}
		}

		public bool IsTransactionBegined
		{
			get
			{
				return DBTransaction != null;
			}

		}

		private void BeginTransaction()
		{
			DBTransaction = DBConnection.BeginTransaction();
			AffectedRowsCount = 0;
		}

		public void Release()
		{
			InUse = false;
			Console.WriteLine("Connection.Released...");
		}

		public void Rollback()
		{
			try
			{
				Console.WriteLine("Rollback : " + (AffectedRowsCount > 0 ? AffectedRowsCount : 0).ToString() + " Row(s)");

				if (DBTransaction != null)
				{
					DBTransaction.Rollback();
				}

				BeginTransaction();
			}
			catch (Exception _Ex)
			{

				try
				{
					BeginTransaction();
				}
				catch (Exception _Ex2)
				{
				}

				ConnectionPoolingManager.Database.App.Loggers.SqlLogger.LogError(new List<string>() { "Connection.Rollback Failed" }, _Ex, new List<string>() { });
				Console.WriteLine("Connection.Rollback Failed");
				Console.WriteLine(_Ex);
			}
			Release();
		}

		public void Commit()
		{
			try
			{
				Console.WriteLine("Commit Connection ID : " + ID);
				Console.WriteLine("Commit : " + (AffectedRowsCount > 0 ? AffectedRowsCount : 0).ToString() + " Row(s)");

				if (DBTransaction != null)
				{
					DBTransaction.Commit();
				}

				BeginTransaction();
			}
			catch (Exception _Ex)
			{
				try
				{
					BeginTransaction();
				}
				catch (Exception _Ex2)
				{
				}
				ConnectionPoolingManager.Database.App.Loggers.SqlLogger.LogError(new List<string>() { "Connection.Commit Failed" }, _Ex, new List<string>() { });
				Console.WriteLine("Connection.Commit Failed");
				Console.WriteLine(_Ex);
			}
			Release();
		}

		public void Execute(string _Sql)
		{
			Execute(new cSql(this, _Sql));
		}

		protected void Log(cSql _Sql, string _ElapsedTime)
		{
            cApp __App = cApp.App;
			List<string> __BulkLog = new List<string>();

			__BulkLog.Add(_Sql.FullSQLString);
			if (_Sql.Parameters.Count > 0)
			{
				__BulkLog.Add("//////////// PARAMETERS ///////////");
				foreach (var __Item in _Sql.Parameters)
				{
					__BulkLog.Add("\t" + __Item.Key + "\t:\t" + __Item.Value);
				}
				__BulkLog.Add("/////////////////////////////////// ");
			}

			__BulkLog.Add("                   ↓↓                    ");
			if (__App.Cfg<cDataConfiguration>().BootType == EBootType.Console
			  || __App.Cfg<cDataConfiguration>().BootType == EBootType.Batch)
			{
				__BulkLog.Add("RequestID : " + __App.Cfg<cDataConfiguration>().BootType.Name);
			}
			else
			{
				__BulkLog.Add("RequestID : " + cContextItem.GetRequestID());
			}
			__BulkLog.Add("ElapsedTime : " + _ElapsedTime);
			__BulkLog.Add("//////////////////////////////////");
			__App.Loggers.SqlLogger.LogInfo(__BulkLog);
		}

		public int Execute(cSql _Sql)
		{
			//ExecutedList.Add(_Sql);
			//DataConfiguration.ExecutedSqlList.Add(_Sql);

			List<MethodBase> __MethodList = ConnectionPoolingManager.Database.App.Handlers.StackHandler.GetMethods("InvokeTransactionalAction", 0);

			int __InvokeTransactionalActionCount =  __MethodList.Where(__Item => __Item.DeclaringType.Name == this.ConnectionPoolingManager.Database.DataService.GetType().Name).ToList().Count;

			if (__InvokeTransactionalActionCount != 1 && _Sql.IsTransactionalCommand && ConnectionPoolingManager.Database.App.Handlers.StackHandler.SearchMethodValidMaxCount("GetNewEntityID", 0))
			{
				throw new Exception("Transaction olmadan kayit edilmeye calisiliyor!!");
			}

			if (DataConfiguration.SimulateDBSynchronize)
			{
				DataConfiguration.ExecutedSqlList.Add(_Sql);
				return 0;
			}
			else
			{
				Open();
				IDbCommand __Command = NewDbCommand();
				__Command.Connection = DBConnection;

				if (_Sql.IsBackup)
				{
					__Command.CommandTimeout = 0; // for sql server error : Timeout expired..
				}

				if (!_Sql.IsTransactionalCommand)
				{
					if (DBTransaction != null)
					{
						DBTransaction.Commit(); // ends previously started transaction
					}
					__Command.Transaction = null; // Transaction must be null for "fulltext" commands in sql server
					DBTransaction = null;
				}
				else
				{
					__Command.Transaction = DBTransaction;
				}

				try
				{
					MethodInfo __MethodInfo = _Sql.GetType().GetMethod("GetConnectionMarkedSqlString", BindingFlags.NonPublic | BindingFlags.Instance);
					__Command.CommandText = __MethodInfo.Invoke(_Sql, new object[] { }).ToString();
					SetParameters(__Command, _Sql);

					if (DataConfiguration.LogExecutedSqlEnabled)
					{
						Stopwatch __StopWatch = new Stopwatch();
						__StopWatch.Start();

						AffectedRowsCount += __Command.ExecuteNonQuery();

						__StopWatch.Stop();
						// Get the elapsed time as a TimeSpan value.
						TimeSpan __TimeSpan = __StopWatch.Elapsed;

						string __Elapsed = string.Format("{0:0.000}", (Convert.ToDouble(__TimeSpan.TotalMilliseconds) / 1000d));

						Log(_Sql, __Elapsed);
					}
					else
					{
						AffectedRowsCount += __Command.ExecuteNonQuery();
					}
				}
				catch (Exception _Ex)
				{
					ConnectionPoolingManager.Database.App.Loggers.SqlLogger.LogError(_Ex);
					ConnectionPoolingManager.Database.App.Loggers.SqlLogger.LogError(_Sql.FullSQLString);
					Rollback();
					Console.WriteLine(_Sql.FullSQLString);
					throw _Ex;
				}
				if (!_Sql.IsTransactionalCommand)
				{
					Commit(); // required for sql server ??
				}
				return AffectedRowsCount;
			}
		}
		public object ExecuteScalar(cSql _Sql)
		{

			if (!ConnectionPoolingManager.Database.App.Handlers.StackHandler.SearchMethodMustCount("InvokeTransactionalAction", 1) && _Sql.IsTransactionalCommand && ConnectionPoolingManager.Database.App.Handlers.StackHandler.SearchMethodValidMaxCount("GetNewEntityID", 0))
			{
				throw new Exception("Tansaction olmadan kayit edilmeye calisiliyor!!");
			}

			object __ResultObject = null;
			//ExecutedList.Add(_Sql);
			//DataConfiguration.ExecutedSqlList.Add(_Sql);

			if (DataConfiguration.SimulateDBSynchronize)
			{
				DataConfiguration.ExecutedSqlList.Add(_Sql);
				return null;
			}
			else
			{
				Open();
				IDbCommand __Command = NewDbCommand();
				__Command.Connection = DBConnection;
				if (!_Sql.IsTransactionalCommand)
				{
					if (DBTransaction != null)
					{
						DBTransaction.Commit(); // ends previously started transaction
					}
					__Command.Transaction = null; // Transaction must be null for "fulltext" commands in sql server
					DBTransaction = null;
				}
				else
				{
					__Command.Transaction = DBTransaction;
				}

				__Command.CommandTimeout = 0;

				try
				{
					MethodInfo __MethodInfo = _Sql.GetType().GetMethod("GetConnectionMarkedSqlString", BindingFlags.NonPublic | BindingFlags.Instance);
					__Command.CommandText = __MethodInfo.Invoke(_Sql, new object[] { }).ToString();
					SetParameters(__Command, _Sql);

					if (DataConfiguration.LogExecutedSqlEnabled)
					{
						Stopwatch __StopWatch = new Stopwatch();
						__StopWatch.Start();

						__ResultObject = __Command.ExecuteScalar();

						__StopWatch.Stop();
						// Get the elapsed time as a TimeSpan value.
						TimeSpan __TimeSpan = __StopWatch.Elapsed;

						string __Elapsed = string.Format("{0:0.000}", (Convert.ToDouble(__TimeSpan.TotalMilliseconds) / 1000d));

						Log(_Sql, __Elapsed);
					}
					else
					{
						__ResultObject = __Command.ExecuteScalar();
					}
				}
				catch (Exception _Ex)
				{
					ConnectionPoolingManager.Database.App.Loggers.SqlLogger.LogError(_Ex);
					ConnectionPoolingManager.Database.App.Loggers.SqlLogger.LogError(_Sql.FullSQLString);
					Console.WriteLine(_Sql.FullSQLString);
					throw _Ex;
				}
				return __ResultObject;
			}
		}

		public DataTable Query(string _Sql)
		{
			return Query(new cSql(this, _Sql));
		}

		public DataTable Query(cSql _Sql)
		{
			//ExecutedList.Add(_Sql);
			if (_Sql.IsQuery)
			{
				Open();
				DataSet __DataSet = new DataSet();
				IDbCommand __Command = NewDbCommand();
				__Command.Connection = DBConnection;
				__Command.Transaction = DBTransaction;

				DataTable __Result = null;
				try
				{
					__Command.CommandText = _Sql.GetType().GetMethod("GetConnectionMarkedSqlString", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(_Sql, new object[] { }).ToString();
					SetParameters(__Command, _Sql);
					IDbDataAdapter __Adapter = NewDbDataAdapter();
					__Adapter.SelectCommand = __Command;

					if (DataConfiguration.LogExecutedSqlEnabled)
					{
						Stopwatch __StopWatch = new Stopwatch();
						__StopWatch.Start();

						__Adapter.Fill(__DataSet);

						__StopWatch.Stop();
						// Get the elapsed time as a TimeSpan value.
						TimeSpan __TimeSpan = __StopWatch.Elapsed;

						string __Elapsed = string.Format("{0:0.000}", (Convert.ToDouble(__TimeSpan.TotalMilliseconds) / 1000d));

						Log(_Sql, __Elapsed);
					}
					else
					{
						__Adapter.Fill(__DataSet);
					}

					if (__DataSet.Tables.Count == 1)
					{
						__Result = __DataSet.Tables[0];
					}

					if (__Result != null) Console.WriteLine(__Result.Rows.Count.ToString() + " Row(s) retrieved");
				}
				catch (Exception _Ex)
				{
					ConnectionPoolingManager.Database.App.Loggers.SqlLogger.LogError(_Ex);
					ConnectionPoolingManager.Database.App.Loggers.SqlLogger.LogError(_Sql.FullSQLString);
					if (DataConfiguration.SimulateDBSynchronize)
					{
						DataConfiguration.ExecutedSqlList.Add(_Sql);
						return new DataTable();
					}
					else
					{
						Console.WriteLine(_Sql.FullSQLString);
						throw _Ex;
					}
				}
				Release();
				return __Result;
			}
			else
			{
				throw new Exception("Query komutuyla sadece SELECT çekilebilir!");
			}
		}

		private object ConvertApplicationValueToDatabaseValue(object _Value)
		{
			if (_Value == null || _Value == DBNull.Value)
			{
				return DBNull.Value;
			}
			else
			{
				Type __Type = _Value.GetType();

				if (__Type == typeof(bool)) return ((bool)_Value ? 1 : 0);
				if (__Type == typeof(string)) return ((string)_Value).Trim();
				if (__Type == typeof(long)) return (long)_Value;
				if (__Type == typeof(DateTime)) return _Value;
				else return _Value;
			}
		}

		public void SetParameters(IDbCommand _Command, cSql _Sql)
		{
			foreach (var __Item in _Sql.Parameters)
			{
				IDbDataParameter parameter = _Command.CreateParameter();
				parameter.ParameterName = GetParameterMarker() + __Item.Key;
				parameter.Value = ConvertApplicationValueToDatabaseValue(__Item.Value);
				_Command.Parameters.Add(parameter);
			}
		}
	}
}
