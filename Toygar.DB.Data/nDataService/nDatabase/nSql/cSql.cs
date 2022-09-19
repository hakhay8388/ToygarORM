using Toygar.DB.Data.nDataService.nDatabase.nConnection;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nSql
{
    public class cSql
    {
        protected cBaseConnection Connection { get; set; }
        public string FullSQLString { get; set; }
        public string SqlStartWith { get; set; }
        public List<string> ErrorList { get; set; }
        public Dictionary<string, object> Parameters { get; set; }

        public cSql(cBaseConnection _Connection, string _FullSQLString)
        {
            Connection = _Connection;
            FullSQLString = _FullSQLString;
            ErrorList = new List<string>();
            Parameters = new Dictionary<string, object>();
            Parse(_FullSQLString);
        }


        public List<cParameter> ToParametersList()
        {
            List<cParameter> __Result = new List<cParameter>();
            foreach (var __ParameterItem in Parameters)
            {
                __Result.Add(new cParameter(__ParameterItem.Key, __ParameterItem.Value)); 
            }
            return __Result;
        }

        public cSql SetParameter(string _ParamName, object _Value)
        {
            Parameters.Add(_ParamName, _Value);
            return this;
        }

        private void Parse(string _Sql)
        {
            string[] __Start = Regex.Split(_Sql, "\\s+");
            SqlStartWith = __Start[0];
        }

        public bool ContainToken(string _Command)
        {
            return SqlStartWith.ToLower() == _Command.ToLower();
        }

        private string GetConnectionMarkedSqlString()
        {
            string __Result = FullSQLString;
            foreach (var __Item in Parameters)
            {
                __Result = __Result.Replace(":" + __Item.Key, Connection.GetParameterMarker() + __Item.Key);
            }
            return __Result;
        }

        public bool IsTransactionalCommand
        {
            get
            {
                return IsDML;
            }
        }

        public bool IsBackup
        {
            get
            {
                return ContainToken("Backup");
            }
        }

        public bool IsPragma
        {
            get
            {
                return ContainToken("Pragma");
            }
        }

        public bool IsDDL
        {
            get
            {
                return (!IsDML && !IsQuery);
            }
        }
        public bool IsDML
        {
            get
            {
                return (ContainToken("Insert") || ContainToken("Update") || ContainToken("Delete"));
            }
        }

        public bool IsQuery
        {
            get
            {
                return ContainToken("Select");
            }
        }

    }
}
