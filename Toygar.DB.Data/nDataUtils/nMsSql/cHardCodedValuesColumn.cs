using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.DB.Data.nDataUtils.nMsSql
{
    public class cHardCodedValuesColumn
    {
        List<object> Values { get; set; }
        public string ColumnName { get; set; }
        public cHardCodedValuesColumn(string _ColumnName)
        {
            ColumnName = _ColumnName;
            Values = new List<object>();
        }

        public void AddValue(object _Item)
        {
            Values.Add(_Item);
        }

        public object GetItem(int _ItemIndex)
        {
            return Values[_ItemIndex];
        }

        public int Count()
        {
            return Values.Count;
        }
    }
}
