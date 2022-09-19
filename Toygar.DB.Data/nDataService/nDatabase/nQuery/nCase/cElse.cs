using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nCase.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nCase
{
    public class cElse<TEntity> : cBaseQuery<TEntity>
        where TEntity : cBaseEntity
    {
        cCase<TEntity> Case { get; set; }
         
        public cElse(cCase<TEntity> _Case)
            : base(_Case.OwnerQuery.Database, EQueryType.Select)
        {
            Case = _Case;
            DefaultAlias = _Case.OwnerQuery.DefaultAlias;
        }

        public override cSql ToSql()
        {
            string __Column = CollectColumns();

            cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLCaseElse(__Column);

            return __Sql;
        }

        public cCase<TEntity> Select(IQuery _Query)
        {
            string __InnerQuery = "(" + _Query.ToSql().FullSQLString + ")";
            Parameters = Parameters.Union(_Query.Parameters).ToList();
            Add(Columns, new cSelectQuery_QueryElement<TEntity>(this, __InnerQuery, ""));
            return Case;
        }

        public cCase<TEntity> SelectColumn(params Expression<Func<TEntity, object>>[] _PropertyExpressions)
        {
            foreach (var _Item in _PropertyExpressions)
            {
                string __Name = Database.App.Handlers.LambdaHandler.GetParamPropName(_Item);
                Add(Columns, new cSelectColumn_QueryElement<TEntity>(this, EntityTable.GetEntityColumnByName(__Name)));
            }
            return Case;
        }

        public cCase<TEntity> SelectValue(object _Value)
        {
            Add(Columns, new cSelectValue_QueryElement<TEntity>(this, _Value, ""));
            return Case;
        }

        /*public cCase<TEntity> SelectAliasAllColumns<TAlias>(Expression<Func<TAlias>> _Alias) where TAlias : cBaseEntity
        {
            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            Add(Columns, new cSelectAliasColumn_QueryElement<TEntity>(this, __AliasName, "*"));

            return Case;
        }*/

        public cCase<TEntity> SelectAliasColumn<TAlias>(Expression<Func<TAlias>> _Alias, params Expression<Func<TAlias, object>>[] _PropertyExpressions) where TAlias : cBaseEntity
        {
            foreach (var _Item in _PropertyExpressions)
            {
                string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
                string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(_Item);
                Add(Columns, new cSelectAliasColumn_QueryElement<TEntity, TAlias>(this, __AliasName, __ColumnName));
            }

            //string __EntityColumnName = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_Alias);
            //Add(Columns, new cSelectAliasColumn_QueryElement<TEntity>(this, __EntityColumnName));

            return Case;
        }

        public cCase<TEntity> SelectAliasColumn<TAlias>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpressions) where TAlias : cBaseEntity
        {
            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpressions);
            Add(Columns, new cSelectAliasColumn_QueryElement<TEntity, TAlias>(this, __AliasName, __ColumnName, ""));

            //string __EntityColumnName = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_Alias);
            //Add(Columns, new cSelectAliasColumn_QueryElement<TEntity>(this, __EntityColumnName));

            return Case;
        }

        public cCase<TEntity> SelectAliasColumnWithName<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnName) where TAlias : cBaseEntity
        {
            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            Add(Columns, new cSelectAliasColumn_QueryElement<TEntity, TAlias>(this, __AliasName, _ColumnName, ""));
            return Case;
        }

        public cCase<TEntity> SelectColumn<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            cEntityTable __RelationTable = Database.EntityManager.GetEntityTableByEnitityType<TRelationEntity>();
            Add(Columns, new cSelectColumn_QueryElement<TEntity>(this, EntityTable.GetEntityColumnByName(__RelationTable.TableForeing_ColumnName_For_InOtherTable)));
            return Case;
        }

        public cCase<TEntity> SelectColumn<TRelationEntity>(params Expression<Func<TEntity, object>>[] _PropertyExpressions) where TRelationEntity : cBaseEntity
        {
            SelectColumn<TRelationEntity>();
            SelectColumn(_PropertyExpressions);
            return Case;
        }
    }
}
