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

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nOrderBy.nAsc
{
    public class cDesc<TEntity> : cAsc<TEntity> where TEntity : cBaseEntity
    {
         public cDesc(cQuery<TEntity> _OwnerQuery, params Expression<Func<object>>[] _Columns)
            :base(_OwnerQuery, _Columns)
        {            
        }

        public cDesc(cQuery<TEntity> _OwnerQuery, params Expression<Func<TEntity, object>>[] _Columns)
            : base(_OwnerQuery, _Columns)
        {           
        }

        public cDesc(cQuery<TEntity> _OwnerQuery, string _ColumnName, bool _UseAlias)
            : base(_OwnerQuery, _ColumnName, _UseAlias)
        {
        }

        public cDesc(cQuery<TEntity> _OwnerQuery, string _AliasName, string _ColumnName)
          : base(_OwnerQuery, _AliasName, _ColumnName)
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
