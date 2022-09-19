using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nApplication.nCoreLoggers;
//using Toygar.Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Toygar.Base.Core.nAttributes;
using Toygar.Base.Core.nCore;
using Toygar.Base.Core.nExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toygar.Base.Core.nHandlers.nContextHandler
{
    public class cContextHandler : cCoreObject
    {
        List<cContextItem> ContextList { get; set; }
        public cContextHandler(nApplication.cApp _App)
            :base(_App)
        {
            ContextList = new List<cContextItem>();
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cContextHandler>(this);
        }


        public void AddContext(HttpContext _Context)
        {
            lock(ContextList)
            {
                ContextList.RemoveAll(__Item => DateTime.Now.Subtract(__Item.UpdateTime).TotalSeconds > 36000);
                cContextItem __ContextItem = ContextList.Where(__Item => __Item.ThreadId == Thread.CurrentThread.ManagedThreadId).FirstOrDefault();
                if (__ContextItem == null)
                {
                    ContextList.Add(new cContextItem(_Context, Thread.CurrentThread.ManagedThreadId));
                }
                else
                {
                    __ContextItem.Refresh(_Context);
                }
            }
            
        }

        public cContextItem CurrentContextItem
        {
            get
            {
                lock (ContextList)
                {
                    List<cContextItem> __ContextItems = ContextList.Where(__Item => __Item.ThreadId == Thread.CurrentThread.ManagedThreadId).ToList();
                    if (__ContextItems.Count == 1)
                    {
                        return __ContextItems[0];
                    }
                    else
                    {
                        throw new Exception("Aynı Thread'e bağlı birden fazla context var yada hiç bir context yok");
                    }                   
                }
            }
        }
    }
}
