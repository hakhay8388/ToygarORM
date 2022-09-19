using Toygar.DB.Data.nDataService.nDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nConnection
{
    public class cConnectionPoolingManager : cBaseDatabaseComponent
    {
        protected Dictionary<int, cBaseConnection > Connections { get; set; }

        protected Type ConnectionType { get; set; }

        public cConnectionPoolingManager(IDatabase _Database, Type _ConnectionType)
            :base(_Database)
        {
            ConnectionType = _ConnectionType;
            Connections = new Dictionary<int,cBaseConnection>();
        }

        public cBaseConnection DefaultConnection
        {
            get
            {
                return GetConnectionByThreadID(Thread.CurrentThread.ManagedThreadId);
            }
        }

        public void RemoveConnection(cBaseConnection _BaseConnection)
        {
            lock (Connections)
            {
                try
                {
                    _BaseConnection.Close();
                    int __Key = Connections.Where(__Item => __Item.Value == _BaseConnection).FirstOrDefault().Key;
                    Connections.Remove(__Key);
                }
                catch(Exception _Ex)
                {
					Database.App.Loggers.SqlLogger.LogError(_Ex);
				}
            }
        }

        public int GetMinID()
        {
			lock (Connections)
			{
				Connections = Connections.OrderBy(__Item1 => __Item1.Key).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
				if (Connections.Count > 0)
				{
					return Connections.First().Key;
				}
				return 0;
			}
        }

        public void ClearUnusedConnection()
        {
            lock (Connections)
            {
                List<KeyValuePair<int, cBaseConnection>> __DeleteList = new List<KeyValuePair<int, cBaseConnection>>();
                foreach (var __Item in Connections)
                {
                    if (!__Item.Value.InUse && (DateTime.Now - __Item.Value.LastUsedTime).TotalSeconds > 600)
                    {
                        __DeleteList.Add(__Item);
                    }                  
                }
                foreach (var __Item in __DeleteList)
                {
                    __Item.Value.Close();
                    Connections.Remove(__Item.Key);
                }
            }
        }

        public KeyValuePair<int, cBaseConnection>? GetOldConnection(bool _CustomConnection)
        {
            lock (Connections)
            {
                var __OrderedList = Connections.Where(__Item => (_CustomConnection ? __Item.Key < 0 : __Item.Key > 0) && !__Item.Value.InUse).OrderBy(__Item => __Item.Value.LastUsedTime).ToList();
                return __OrderedList.FirstOrDefault();
            }
        }


        public cBaseConnection GetConnectionByThreadID(int _ThreadID)
        {
            cBaseConnection __BaseConnection = null;
            lock (Connections)
            {
                ClearUnusedConnection();

                if (Connections.ContainsKey(_ThreadID))
                {
                    __BaseConnection = Connections[_ThreadID];
                    __BaseConnection.Open();
                }
                else
                {
                    KeyValuePair<int, cBaseConnection>? __OldConnection = GetOldConnection(false);
                    if (__OldConnection.Value.Value != null && !__OldConnection.Value.Value.InUse && Connections.Count > (Database.ServiceContext.MaxConnectionCount - 1))
                    {
                        Connections.Remove(__OldConnection.Value.Key);
                        Connections.Add(_ThreadID, __OldConnection.Value.Value);
                        __BaseConnection = __OldConnection.Value.Value;
                    }
                    else
                    {
                        if (Connections.Count < Database.ServiceContext.MaxConnectionCount)
                        {
                            __BaseConnection = __BaseConnection ?? (cBaseConnection)ConnectionType.GetConstructor(new Type[] { typeof(cConnectionPoolingManager) }).Invoke(new object[] { this });
                            Connections.Add(_ThreadID, __BaseConnection);
                        }
                        else
                        {
                            throw new Exception("Maksimum baglanti ssyisina ulasildi! : {0}".FormatEx(Database.ServiceContext.MaxConnectionCount.ToString()));
                        }
                    }
                }
                __BaseConnection.InUse = true;
            }
            return __BaseConnection;
        }
    }
}
