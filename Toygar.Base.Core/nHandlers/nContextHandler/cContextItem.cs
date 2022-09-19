using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.Base.Core.nHandlers.nContextHandler
{
	public class cContextItem
	{
		static int RequestCounter = 0;
		public static string GetRequestID()
		{
			RequestCounter++;
			if ((Int32.MaxValue - 2) < RequestCounter)
			{
				RequestCounter = 0;
			}
			int __Count = RequestCounter;
			var __RequestID = DateTime.Now.Ticks + "_" + __Count;
			return __RequestID;
		}

		public string RequestID { get; set; }
		public int ThreadId { get; set; }
        public HttpContext Context { get; set; }
        public DateTime UpdateTime { get; set; }
        public cContextItem(HttpContext _Context, int _ThreadID)
        {
            Context = _Context;
            ThreadId = _ThreadID;
            UpdateTime = DateTime.Now;
			RequestID = GetRequestID();

		}

        public void Refresh(HttpContext _Context)
        {
            Context = _Context;
            UpdateTime = DateTime.Now;
			RequestID = GetRequestID();
		}
    }
}
