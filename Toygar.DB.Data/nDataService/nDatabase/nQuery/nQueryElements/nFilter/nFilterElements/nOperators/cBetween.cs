using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators
{
    public class cBetween<TOwnerEntity, TEntity> 
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        public cBetween(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, object _Value, object _Value2)
        {
            _QueryFilterOperand.Ge(_Value);
            _QueryFilterOperand.Filter.And.Operand(_QueryFilterOperand.ColumnName).Le(_Value2);
        }

        public cBetween(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, IQuery _Value, IQuery _Value2)
        {
            _QueryFilterOperand.Ge(_Value);
            _QueryFilterOperand.Filter.And.Operand(_QueryFilterOperand.ColumnName).Le(_Value2);
        }

        public cBetween(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Expression<Func<object>> _PropertyExpression, Expression<Func<object>> _PropertyExpression2)
        {
            _QueryFilterOperand.Ge(_PropertyExpression);
            _QueryFilterOperand.Filter.And.Operand(_QueryFilterOperand.ColumnName).Le(_PropertyExpression2);
        }

        public cBetween(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, object _Value, IQuery _Value2)
        {
            _QueryFilterOperand.Ge(_Value);
            _QueryFilterOperand.Filter.And.Operand(_QueryFilterOperand.ColumnName).Le(_Value2);
        }

        public cBetween(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, IQuery _Value, object _Value2)
        {
            _QueryFilterOperand.Ge(_Value);
            _QueryFilterOperand.Filter.And.Operand(_QueryFilterOperand.ColumnName).Le(_Value2);
        }

        public cBetween(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Expression<Func<object>> _PropertyExpression, IQuery _Value2)
        {
            _QueryFilterOperand.Ge(_PropertyExpression);
            _QueryFilterOperand.Filter.And.Operand(_QueryFilterOperand.ColumnName).Le(_Value2);
        }


        public cBetween(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, object _Value, Expression<Func<object>> _PropertyExpression)
        {
            _QueryFilterOperand.Ge(_Value);
            _QueryFilterOperand.Filter.And.Operand(_QueryFilterOperand.ColumnName).Le(_PropertyExpression);
        }

        public cBetween(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Expression<Func<object>> _PropertyExpression, object _Value )
        {
            _QueryFilterOperand.Ge(_PropertyExpression);
            _QueryFilterOperand.Filter.And.Operand(_QueryFilterOperand.ColumnName).Le(_Value);
        }

        public cBetween(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, IQuery _Value, Expression<Func<object>> _PropertyExpression)
        {
            _QueryFilterOperand.Ge(_Value);
            _QueryFilterOperand.Filter.And.Operand(_QueryFilterOperand.ColumnName).Le(_PropertyExpression);
        }
    }
}
