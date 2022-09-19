using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements
{
    public class cQueryFilterAliasOperand<TOwnerEntity, TEntity, TAlias> : cQueryFilterOperand<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {

        public cQueryFilterAliasOperand(cBaseFilter<TOwnerEntity, TEntity> _Filter, Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpression)
           : base(_Filter)
        {
            string __AliasName = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            string __ColumnName = Query.Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpression);

            Filter = _Filter;
            FullName = __AliasName;
            ColumnName = __ColumnName;
            FullName += "." + ColumnName;
        }

        public cQueryFilterAliasOperand(cBaseFilter<TOwnerEntity, TEntity> _Filter, Expression<Func<TAlias>> _Alias, string _ColumnName)
                  : base(_Filter)
        {
            string __AliasName = Query.Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);

            Filter = _Filter;
            FullName = __AliasName;
            ColumnName = _ColumnName;
            FullName += "." + ColumnName;
        }


    }
}
