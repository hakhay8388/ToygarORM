using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy.nHaving;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nOrderBy.nAsc;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nGroupByQueryElement;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nWrappers;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nTake
{
    public class cTake<TEntity> : cBaseWrapper<TEntity> where TEntity : cBaseEntity
    {
        public string TakeTempAlias { get; set; }
        int Start { get; set; }
        int End { get; set; }

        public cTake(cQuery<TEntity> _Query, int _Start, int _End)
            : base(_Query)
        {
            TakeTempAlias = AliasGenerator.GetNewAlias("TakeTempAlias");
            Start = _Start;
            End = _End;
        }

        public override string Wrap(cSql _Sql)
        {
            string __RowNumberColumnName = "";
            bool __Found = false;
            for (int i = Query.Wrappers.Count - 1; i > -1; i--)
            {
                if (typeof(cTakeWrapperElement<TEntity>).IsAssignableFrom(Query.Wrappers[i].GetType()))
                {
                    if (((cTakeWrapperElement<TEntity>)Query.Wrappers[i]).Take == this)
                    {
                        __Found = true;
                    }
                }
                if (__Found)
                {
                    if (typeof(cRowNumberWrapperElement<TEntity>).IsAssignableFrom(Query.Wrappers[i].GetType()))
                    {
                        __RowNumberColumnName = ((cRowNumberWrapperElement<TEntity>)Query.Wrappers[i]).RowNumber.RowNumberColumnName;
                        break;
                    }
                }
            }
            string __StartParam = ParameterNameGenerator.GetNewParamName();
            string __EndParam = ParameterNameGenerator.GetNewParamName();
            Query.Parameters.Add(new cParameter(__StartParam, Start));
            Query.Parameters.Add(new cParameter(__EndParam, End));

            _Sql = Query.Database.Catalogs.RowOperationSQLCatalog.SQLSelect("", TakeTempAlias + ".*", "(" + _Sql.FullSQLString + ") AS " + TakeTempAlias, "", TakeTempAlias + "." + __RowNumberColumnName + ">=:" + __StartParam + " AND " + TakeTempAlias + "." + __RowNumberColumnName + "<=:" + __EndParam, "", "");
            return _Sql.FullSQLString;
        }
    }
}
