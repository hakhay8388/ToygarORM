using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nCase.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nCaseWhenElement;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nCase
{
    public class cCase<TEntity>  where TEntity : cBaseEntity       
    {
        public cQuery<TEntity> OwnerQuery { get; set; }
        public List<IBaseQuery> WhenList { get; set; }
        cElse<TEntity> m_Else { get; set; }

        public string ColumnName { get; set; }

        public cCase(cQuery<TEntity> _OwnerQuery, string _ColumnName)
        {
            OwnerQuery = _OwnerQuery;
            WhenList = new List<IBaseQuery>();
            ColumnName = _ColumnName;
        }

        public cWhenFilter<TEntity> When()
        {
            cWhen <TEntity> __When = new cWhen<TEntity>(this);
            WhenList.Add(__When);
            return __When.WhenFilter();
        }

        public cElse<TEntity> Else()
        {
            m_Else = new cElse<TEntity>(this);
            return m_Else;
        }

        public cSql ToSql()
        {
            string __WhenList = CollectWhens();
            
            if (m_Else != null)
            {
                __WhenList += m_Else.ToSql().FullSQLString;
                OwnerQuery.Parameters = OwnerQuery.Parameters.Union(m_Else.Parameters).ToList();
            }            

            cSql __Sql = OwnerQuery.Database.Catalogs.RowOperationSQLCatalog.SQLCase(__WhenList, ColumnName);

             //OwnerQuery.Parameters = OwnerQuery.Parameters.Union(Parameters).ToList();
             return __Sql;
        }

        protected string CollectWhens()
        {
            string __Result = "";
            WhenList.ForEach(__Item =>
            {
                __Result += __Item.ToSql().FullSQLString;
                OwnerQuery.Parameters = OwnerQuery.Parameters.Union(__Item.Parameters).ToList();
            });
            return __Result;
        }
        
        public cQuery<TEntity> ToQuery()
        {
            return OwnerQuery;
        }
    }
}
