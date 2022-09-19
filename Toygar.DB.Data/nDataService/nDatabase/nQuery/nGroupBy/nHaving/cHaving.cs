using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy.nHaving.nOperators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy.nHaving
{
    public class cHaving<TOwnerEntity, TEntity, TAlias> : cBaseFilter<TOwnerEntity, TEntity>, IHaving<TOwnerEntity, TEntity, TAlias>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
        where TAlias : cBaseEntity
    {
        public cGroupBy<TEntity, TAlias> GroupBy { get; set; }
        public cHaving(cGroupBy<TEntity, TAlias> _GroupBy)
            : base(_GroupBy)
        {
            GroupBy = _GroupBy;
        }
        
        public override cQuery<TOwnerEntity> ToQuery()
        {
            return ((cQuery<TOwnerEntity>)(IQuery)GroupBy.Query);
        }

        public IQueryFilterOperand<TOwnerEntity, TEntity> Avg(Expression<Func<TEntity, object>> _PropertyExpression)
        {
            return new cAvg<TOwnerEntity, TEntity, TAlias>(this, _PropertyExpression);
        }

        public IQueryFilterOperand<TOwnerEntity, TEntity> Avg(string _Name)
        {
            return new cAvg<TOwnerEntity, TEntity, TAlias>(this, _Name);
        }

        public IQueryFilterOperand<TOwnerEntity, TEntity> Avg()
        {
            return new cAvg<TOwnerEntity, TEntity, TAlias>(this);
        }

        public IQueryFilterOperand<TOwnerEntity, TEntity> Avg<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            cEntityTable __Table = Query.Database.EntityManager.GetEntityTableByEnitityType<TRelationEntity>();
            return new cAvg<TOwnerEntity, TEntity, TAlias>(this, __Table.TableForeing_ColumnName_For_InOtherTable);
        }

        public IQueryFilterOperand<TOwnerEntity, TEntity> Count(Expression<Func<TEntity, object>> _PropertyExpression)
        {
            return new cCount<TOwnerEntity, TEntity, TAlias>(this, _PropertyExpression);
        }

        public IQueryFilterOperand<TOwnerEntity, TEntity> Count(string _Name)
        {
            return new cCount<TOwnerEntity, TEntity, TAlias>(this, _Name);
        }

        public IQueryFilterOperand<TOwnerEntity, TEntity> Count()
        {
            return new cCount<TOwnerEntity, TEntity, TAlias>(this);
        }

        public IQueryFilterOperand<TOwnerEntity, TEntity> Count<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            cEntityTable __Table = Query.Database.EntityManager.GetEntityTableByEnitityType<TRelationEntity>();
            return new cCount<TOwnerEntity, TEntity, TAlias>(this, __Table.TableForeing_ColumnName_For_InOtherTable);
        }

    }
}
