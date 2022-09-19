using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nOrderByQueryElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber.nOver.nOrderBy.nAsc
{
    public class cDesc<TEntity> : cAsc<TEntity> where TEntity : cBaseEntity
    {
        public cDesc(cRowNumber<TEntity> _RowNumber, params Expression<Func<object>>[] _Columns)
            : base(_RowNumber, _Columns)
        {            
        }

        public cDesc(cRowNumber<TEntity> _RowNumber, params Expression<Func<TEntity, object>>[] _Columns)
            : base(_RowNumber, _Columns)
        {           
        }

        public cDesc(cRowNumber<TEntity> _RowNumber, string _ColumnName)
            : base(_RowNumber, _ColumnName)
        {
        }

        public override string ToElementString(params object[] _Params)
        {
            string __Result = "";
            foreach (var __Item in NameList)
            {
                __Result += __Result.IsNullOrEmpty() ? __Item : ", " + __Item;
            }
            return " " + __Result + " DESC";
        }
    }
}
