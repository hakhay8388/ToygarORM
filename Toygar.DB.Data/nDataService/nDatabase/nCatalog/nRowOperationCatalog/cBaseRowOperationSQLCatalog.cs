using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog
{
    public abstract class cBaseRowOperationSQLCatalog : cBaseCatalogOperations
    {
        public cBaseRowOperationSQLCatalog(IDatabase _Database)
            : base(_Database)
        {
        }
        public abstract cSql SQLInsertSingleRow(string _TableName, string _ColumnList, string _ParamList);
        public abstract cSql SQLUpdateSingleRow(string _TableName, string _SetList);
        public abstract cSql SQLUpdateByCondition(string _TableName, string _SetList, string _Condition);
        public abstract cSql SQLDeleteSingleRow(string _TableName);
        public abstract cSql SQLDeleteByCondition(string _TableName, string _Condition);
        public abstract cSql SQLIncrementBy(string _TableName, string _ColumnName, long _Value, string _Condition);
        
        public abstract cSql SQLSelect(string _TableName, string _Condition);
		public abstract cSql SQLSelectLockedRows(string _TableName, string _Condition);
		
		public abstract string SQLDifference(string _Column, string _Value, bool _WrapValueWithString);
        public abstract string SQLDateDiff(string _Interval, string _Column1, string _Column2);
        


        public abstract cSql SQLSelectMapped(string _MapTableName, string _MapCondition, string _MappedTableName, string _MappedCondition);
        public abstract cSql SQLSelect(string _PreQueries, string _Columns, string _Source, string _Joins, string _Conditions, string _Groups, string _Orders);
        public abstract cSql SQLSelect(string _HardCodeSql);
        public abstract cSql SQLLeftJoin(string _Source, string _Conditions);
        public abstract cSql SQLInnerJoin(string _Source, string _Conditions);
        public abstract cSql SQLCrossApply(string _Source);
        public abstract cSql SQLOuterApply(string _Source);
        public abstract cSql SQLCase(string _CaseContitions, string _ColumnName);
        public abstract cSql SQLCaseWhen(string _Conditions, string _Result);
        public abstract cSql SQLCaseElse(string _ElseResult);
        public abstract cSql SQLGroupByHaving(string _GroupBy, string _Having);
        public abstract cSql SQLSelectRowRange(string _SqlText);
        public abstract cSql SQLSelectTop(string _SqlText);
        public abstract cSql SQLSelectCount(cSql _Sql, string _CountAliasName);
        public abstract cSql WrapForRowNumber(string _TotalCountColumName, string _PartitioningColumnName, string _OrderColumnForRowNumber, string _AliasName, string _RowNumberColumnName, string _WrapingSql);
    }
}
