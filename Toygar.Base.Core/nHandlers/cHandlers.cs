using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nApplication.nCoreLoggers;
//using Toygar.Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Toygar.Base.Core.nAttributes;
using Toygar.Base.Core.nCore;
using Toygar.Base.Core.nHandlers.nAssemblyHandler;
using Toygar.Base.Core.nHandlers.nFileHandler;
using Toygar.Base.Core.nHandlers.nLambdaHandler;
using Toygar.Base.Core.nHandlers.nReflectionHandler;
using Toygar.Base.Core.nHandlers.nStringHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.Base.Core.nHandlers.nHashTableHandler;
using Toygar.Base.Core.nHandlers.nDateTimeHandler;
using Toygar.Base.Core.nHandlers.nProcessHandler;
using Toygar.Base.Core.nHandlers.nHashHandler;
using Toygar.Base.Core.nHandlers.nValidationHandler;
using Toygar.Base.Core.nHandlers.nContextHandler;
using Toygar.Base.Core.nHandlers.nDefaultDataHandler;
using Toygar.Base.Core.nHandlers.nEmailHandler;
using Toygar.Base.Core.nHandlers.nExcelHandler;
using Toygar.Base.Core.nHandlers.nStackHandler;
using Toygar.Base.Core.nHandlers.nXmlHandler;

namespace Toygar.Base.Core.nHandlers
{
    public class cHandlers : cCoreObject
    {
        public cAssemblyHandler AssemblyHandler { get; set; }
        public cLambdaHandler LambdaHandler { get; set; }
        public cReflectionHandler ReflectionHandler { get; set; }
        public cFileHandler FileHandler { get; set; }
        public cStringHandler StringHandler { get; set; }
        public cHashTableHandler HashTableHandler { get; set; }
        public cDateTimeHandler DateTimeHandler { get; set; }
        public cProcessHandler ProcessHandler { get; set; }
        public cHashHandler HashHandler { get; set; }
        public cValidationHandler ValidationHandler { get; set; }
        public cContextHandler ContextHandler { get; set; }
        public cDefaultDataHandler DefaultDataHandler { get; set; }
        
        public cEmailHandler EmailHandler { get; set; }
		public cExcelHandler ExcelHandler { get; set; }
		public cStackHandler StackHandler { get; set; }
        public cXmlHandler XmlHandler { get; set; }

        public cHandlers(nApplication.cApp _App)
            : base(_App)
        {
            AssemblyHandler = new cAssemblyHandler(_App);
            LambdaHandler = new cLambdaHandler(_App);
            ReflectionHandler = new cReflectionHandler(_App);
            FileHandler = new cFileHandler(_App);
            StringHandler = new cStringHandler(_App);
            HashTableHandler = new cHashTableHandler(_App);
            DateTimeHandler = new cDateTimeHandler(_App);
            ProcessHandler = new cProcessHandler(_App);
            HashHandler = new cHashHandler(_App);
            ValidationHandler = new cValidationHandler(_App);
            ContextHandler = new cContextHandler(_App);
            DefaultDataHandler = new cDefaultDataHandler(_App);
            EmailHandler = new cEmailHandler(_App);
			ExcelHandler = new cExcelHandler(_App);
			StackHandler = new cStackHandler(_App);
            XmlHandler = new cXmlHandler(_App);
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cHandlers>(this);
			StackHandler.Init();
			AssemblyHandler.Init();
            LambdaHandler.Init();
            ReflectionHandler.Init();
            FileHandler.Init();
            StringHandler.Init();
            HashTableHandler.Init();
            DateTimeHandler.Init();
            ProcessHandler.Init();
            HashHandler.Init();
            ValidationHandler.Init();
            ContextHandler.Init();
            DefaultDataHandler.Init();
            EmailHandler.Init();
			ExcelHandler.Init();
		}
    }
}
