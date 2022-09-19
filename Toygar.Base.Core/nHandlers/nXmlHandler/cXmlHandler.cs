using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nAttributes;
using Toygar.Base.Core.nCore;
using Toygar.Base.Core.nExceptions;
using Toygar.Base.Core.nHandlers.nStringHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using Ionic.Zip;
using OfficeOpenXml;

namespace Toygar.Base.Core.nHandlers.nXmlHandler
{
    public class cXmlHandler : cCoreObject
    {
        public cXmlHandler(nApplication.cApp _App)
            : base(_App)
        {
        }

		public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cXmlHandler>(this);
        }

        public void WriteObjectToXML<T>(T _Object, string _FullPath)
        {
            var __Writer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var __File = new System.IO.StreamWriter(_FullPath);
            __Writer.Serialize(__File, _Object);
            __File.Close();
        }

        public T ReadXMLToObject<T>(string _FullPath)
        {
            System.Xml.Serialization.XmlSerializer __Reader = new System.Xml.Serialization.XmlSerializer(typeof(T));
            System.IO.StreamReader __File = new System.IO.StreamReader(_FullPath);
            T __Object = (T)__Reader.Deserialize(__File);
            __File.Close();
            return __Object;
        }
    }
}
