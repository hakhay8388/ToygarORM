using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.DB.Data.nDataUtils.nMsSql
{
    public class cHardCodedValues : cBaseHardCodedValues
    {
        public List<cHardCodedValuesColumn> ColumnList { get; set; }

        IDataService DataService { get; set; }
        public cHardCodedValues(IDataService _DataService)
            :base(_DataService)
        {
            ColumnList = new List<cHardCodedValuesColumn>();
            DataService = _DataService;
        }

        //cSql __Sql = new cSql(__DataService.Database.DefaultConnection, "select * from (values ('test-a1', 'test-a2'), ('test-b1', 'test-b2'), ('test-c1', :deneme)) x(Files, FileCreateDate)");
        //__Sql.Parameters.Add("deneme", "hayri");

        public string GetValueList()
        {
            string __Result = "";
            for (int i = 0; i < ColumnList[0].Count(); i++)
            {
                if (i > 0)
                {
                    __Result += ",";
                }
                __Result += "(";
                for (int j = 0; j < ColumnList.Count; j++)
                {
                    if (j > 0)
                    {
                        __Result += ",";
                    }
                    __Result += ":" + ColumnList[j].ColumnName + "_" + i.ToString() ;
                }
                __Result += ")";
            }
            return __Result;
        }

        public string GetColumns()
        {
            string __Result = "";
            for (int i = 0; i < ColumnList.Count; i++)
            {
                if (i > 0)
                {
                    __Result += ",";
                }
                __Result += ColumnList[i].ColumnName;
            }
            return __Result;
        }
        public void SetSqlParameter(cSql _Sql)
        {
            for (int i = 0; i < ColumnList[0].Count(); i++)
            {
                for (int j = 0; j < ColumnList.Count; j++)
                {
                    _Sql.SetParameter( ColumnList[j].ColumnName + "_" + i.ToString(), ColumnList[j].GetItem(i));
                }
            }
        }

        public override cSql ToSql()
        {
            string __Values = GetValueList();
            string __Columns = GetColumns();

            string __SqlString = "select * from(values " + __Values + ") x(" + __Columns + ")";

            cSql __Sql = new cSql(DataService.Database.DefaultConnection, __SqlString);
            SetSqlParameter(__Sql);
            return __Sql;
        }

        public override void AddValue(params object[] _Value)
        {
            for (int i = 0; i < ColumnList.Count; i++)
            {
                ColumnList[i].AddValue(_Value[i]);
            }
        }


        public override void DefineColumns(params string[] _Columns)
        {
            for (int i = 0; i < _Columns.Length; i++)
            {
                ColumnList.Add(new cHardCodedValuesColumn(_Columns[i]));
            }
        }
    }
}
