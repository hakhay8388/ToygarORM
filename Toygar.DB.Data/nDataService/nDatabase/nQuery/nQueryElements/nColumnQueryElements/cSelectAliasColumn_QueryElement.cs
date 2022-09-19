using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements
{
    public class cSelectAliasColumn_QueryElement<TEntity, TAlias> : cBaseQueryElement, ISelectableColumns
        where TEntity : cBaseEntity
        where TAlias : cBaseEntity
    {
        public string ColumnAs { get; set; }
        public string ColumnName { get; set; }
        public string AliasName { get; set; }
        public cSelectAliasColumn_QueryElement(IBaseQuery _Query, string _AliasName, string _ColumnName, string _ColumnAs = "")
            : base(_Query)
        {
            ColumnName = _ColumnName;
            AliasName = _AliasName;
            ColumnAs = _ColumnAs;
        }


        public override string ToElementString(params object[] _Params)
        {
            if (ColumnAs.IsNullOrEmpty())
            {
                return AliasName + "." + ColumnName;
            }
            else
            { 
                if (AliasName.IsNullOrEmpty())
                {
                    return ColumnName + " AS " + ColumnAs; 
                }
                return AliasName + "." + ColumnName + " AS " + ColumnAs;
            }
        }
        public List<string> GetColumnNameList()
        {
            if (ColumnName != "*")
            {
                if (ColumnAs.IsNullOrEmpty())
                {
                    return new List<string>() { ColumnName };
                }
                else
                {
                    return new List<string>() { ColumnAs };
                };
            }
            else
            {
                cEntityTable __Table = Query.Database.EntityManager.GetEntityTableByEnitityType<TAlias>();
                List<string> __Result = new List<string>();
                for (int i = 0; i < __Table.EntityFieldList.Count; i++)
                {
                    __Result.Add(__Table.EntityFieldList[i].ColumnName);
                }
                return __Result;
            }
        }
    }
}
