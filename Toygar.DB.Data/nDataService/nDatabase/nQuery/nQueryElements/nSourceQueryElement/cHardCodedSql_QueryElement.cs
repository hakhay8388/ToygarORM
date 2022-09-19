using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nSourceQueryElement
{
    public class cHardCodedSql_QueryElement<TEntity> : cBaseQueryElement where TEntity : cBaseEntity
    {
        public cBaseHardCodedValues HardCodedValues { get; set; }
        string m_SubQueryExternalAlias { get; set; }
        string m_ElementResult { get; set; }
        public cHardCodedSql_QueryElement(IBaseQuery _Query, cBaseHardCodedValues _HardCodedValues, Expression<Func<TEntity>> _SubQueryExternalAlias)
            : base(_Query)
        {
            HardCodedValues = _HardCodedValues;
            m_SubQueryExternalAlias = _Query.Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_SubQueryExternalAlias);
            BuildSql();
        }

        private void BuildSql()
        {
            cSql __Sql = HardCodedValues.ToSql();
            m_ElementResult = "(" + __Sql.FullSQLString + ") " + m_SubQueryExternalAlias;
            Query.Parameters = Query.Parameters.Union(__Sql.ToParametersList()).ToList();
        }

        public override string ToElementString(params object[] _Params)
        {
            return m_ElementResult;
        }
    }
}
