using System.Reflection;
using System;
using System.Linq.Expressions;
using Toygar.Base.Core.nHandlers.nAssemblyHandler;
using Toygar.Base.Core.nAttributes;
using Toygar.Base.Core.nApplication.nCoreLoggers;
//using Toygar.Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Toygar.Base.Core.nCore;
using Toygar.Base.Core.nApplication;
using System.Diagnostics;

namespace Toygar.Base.Core.nHandlers.nReflectionHandler
{
    public class cReflectionHandler : cCoreObject
    {
        public cReflectionHandler(nApplication.cApp _App)
            : base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cReflectionHandler>(this);
        }


        public MethodInfo GetMethodInfo(Type _Type, string _Name)
        {
            return _Type.SearchMethod(_Name);
        }

        public FieldInfo[] GetFields(Type _Type)
        {
            return _Type.GetAllFields();
        }

        public Type GetClassTypeByName(string _Name)
        {
            Assembly __Assembly = Assembly.GetExecutingAssembly();
            Type __Type = __Assembly.GetType(_Name);
            return __Type;
        }

        public string GetVariableName<T>(Expression<Func<T>> _Expr)
        {
            var __Body = (MemberExpression)_Expr.Body;
            return __Body.Member.Name;
        }


        public string GetCallerMethodName()
        {
            return GetCallerMethodName(3); // each caller is a frame
        }
        public string GetCallerMethodName(int _SkipFrames)
        {
            StackFrame __Frame = new StackFrame(_SkipFrames);
            var __CallerMethod = __Frame.GetMethod();
            return __CallerMethod.DeclaringType.Name + "." + __CallerMethod.Name;
        }


    }
}
