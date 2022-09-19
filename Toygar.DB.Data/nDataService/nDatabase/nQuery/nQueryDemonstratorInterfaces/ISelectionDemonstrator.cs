using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryDemonstratorInterfaces
{
    public interface ISelectionDemonstrator<TEntity> where TEntity : cBaseEntity
    {
        cQuery<TEntity> SelectCount();

        cQuery<TEntity> SelectCount(string _CustomAliasName);
        cQuery<TEntity> SelectAll();
        cQuery<TEntity> SelectAllColumns();
        cQuery<TEntity> SelectColumn(params Expression<Func<TEntity, object>>[] _PropertyExpressions);
        cQuery<TEntity> SelectValue(object _Value, string _ColumnAs);
        cQuery<TEntity> SelectColumn<TRelationEntity>() where TRelationEntity : cBaseEntity;
        cQuery<TEntity> SelectColumn<TRelationEntity>(params Expression<Func<TEntity, object>>[] _PropertyExpressions) where TRelationEntity : cBaseEntity;

        cQuery<TEntity> SelectAliasAllColumns<TAlias>(Expression<Func<TAlias>> _Alias) where TAlias : cBaseEntity;
        cQuery<TEntity> SelectAliasColumn<TAlias>(Expression<Func<TAlias>> _Alias, params Expression<Func<TAlias, object>>[] _PropertyExpressions) where TAlias : cBaseEntity;
        cQuery<TEntity> SelectAliasColumn<TAlias>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpressions, string _ColumnAs = "") where TAlias : cBaseEntity;

        cQuery<TEntity> SelectAliasColumnMerge<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnAs = "", string _Seperator = " ", params Expression<Func<TAlias, object>>[] _PropertyExpressions) where TAlias : cBaseEntity;
        
        cQuery<TEntity> SelectAliasColumnWithName<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnName, string _ColumnAs = "") where TAlias : cBaseEntity;

        cQuery<TEntity> Max(Expression<Func<TEntity, object>> _PropertyExpressions);
        cQuery<TEntity> Max<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnAs, params Expression<Func<TAlias, object>>[] _PropertyExpressions) where TAlias : cBaseEntity;
        cQuery<TEntity> Avg<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnAs, params Expression<Func<TAlias, object>>[] _PropertyExpressions) where TAlias : cBaseEntity;
        cQuery<TEntity> Min(Expression<Func<TEntity, object>> _PropertyExpressions);
        cQuery<TEntity> Sum(Expression<Func<TEntity, object>> _PropertyExpressions);
        cQuery<TEntity> IfIsNull<TAlias, TAlias2>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpressions, Expression<Func<TAlias2>> _Alias2, Expression<Func<TAlias2, object>> _PropertyExpressions2, string _ColumnAs)
            where TAlias : cBaseEntity
            where TAlias2 : cBaseEntity;
        cQuery<TEntity> CastAsVarchar<TAlias>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpressions, string _ColumnAs)
            where TAlias : cBaseEntity;

        cQuery<TEntity> Sum<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnAs, params Expression<Func<TAlias, object>>[] _PropertyExpressions) where TAlias : cBaseEntity;

        cQuery<TEntity> Avg(Expression<Func<TEntity, object>> _PropertyExpressions);
        cQuery<TEntity> Avg<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnAs, Expression<Func<TAlias, object>> _PropertyExpression) where TAlias : cBaseEntity;

        cQuery<TEntity> SelectID(string _ColumnAs = "");
        cRowNumber<TEntity> RowNumber(string _TotalCountColumName);
        ISelectionDemonstrator<TEntity> Top(int _Count);
        ISelectionDemonstrator<TEntity> Distinct();
    }
}
