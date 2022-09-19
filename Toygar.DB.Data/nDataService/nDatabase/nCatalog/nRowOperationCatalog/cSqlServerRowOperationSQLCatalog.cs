using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog
{
    public class cSqlServerRowOperationSQLCatalog : cBaseRowOperationSQLCatalog
    {
        public const string StartRowNumber = "StartRowNumber";
        public const string EndRowNumber = "EndRowNumber";

        public cSqlServerRowOperationSQLCatalog(IDatabase _Database)
            : base(_Database)
        {
        }

        public override cSql SQLSelectRowRange(string _SqlText)
        {
            string __RangeSqlText = _SqlText.TrimStart();

            if (__RangeSqlText.ToUpperInvariant().Substring(0, 15) == "SELECT DISTINCT")
            {
                __RangeSqlText = "SELECT DISTINCT TOP (9999999999)" + __RangeSqlText.Substring(16);
            }
            else
            {
                __RangeSqlText = "SELECT TOP (9999999999)" + __RangeSqlText.Substring(7);
            }

            __RangeSqlText = "SELECT * FROM" +
                          " (SELECT T1.*, ROW_NUMBER() OVER (ORDER BY (SELECT 0)) AS 'ROW_NUMBER'" +
                          " FROM (" + __RangeSqlText + ") T1) T2" +
                          " WHERE ROW_NUMBER>=:" + StartRowNumber + " AND ROW_NUMBER<=:" + EndRowNumber;

            return CreateSql(__RangeSqlText);
        }
        public override cSql SQLSelectTop(string _SqlText)
        {
            string __RangeSqlText = _SqlText.TrimStart();

            if (__RangeSqlText.ToUpperInvariant().Substring(0, 15) == "SELECT DISTINCT")
            {
                __RangeSqlText = "SELECT DISTINCT TOP (:" + EndRowNumber + ")" + __RangeSqlText.Substring(15);
            }
            else
            {
                __RangeSqlText = "SELECT TOP (:" + EndRowNumber + ")" + __RangeSqlText.Substring(6);
            }

            return CreateSql(__RangeSqlText);
        }

        public override cSql SQLSelect(string _TableName, string _Condition)
        {
            return CreateSql("SELECT * FROM " + _TableName + (!string.IsNullOrEmpty(_Condition) ? " WHERE " + _Condition : ""));
        }

        public override cSql SQLSelectLockedRows(string _TableName, string _Condition)
        {
            return CreateSql("select * from " + _TableName +
                        @" where %%lockres%% in 
						(
							select l.resource_description
							from sys.dm_tran_locks as l
							join sys.partitions as p on l.resource_associated_entity_id = p.partition_id
							where l.resource_type in ('KEY', 'RID')
							and p.object_id = object_id('" + _TableName + @"')
						) AND " + _Condition);
        }



        public override cSql SQLSelectMapped(string _MapTableName, string _MapCondition, string _MappedTableName, string _MappedCondition)
        {
            return CreateSql("SELECT " + _MappedTableName + ".* FROM " + _MapTableName +


                " INNER JOIN " + _MappedTableName + " ON " + _MappedCondition +

                (!string.IsNullOrEmpty(_MapCondition) ? " WHERE " + _MapCondition : ""));
        }

        public override cSql SQLDeleteByCondition(string _TableName, string _Condition = null)
        {
            return CreateSql("DELETE FROM " + _TableName + " WITH(ROWLOCK) " + (!string.IsNullOrEmpty(_Condition) ? " WHERE " + _Condition : ""));
        }
        public override cSql SQLDeleteSingleRow(string _TableName)
        {
            return CreateSql("DELETE FROM " + _TableName + " WITH(ROWLOCK) WHERE ID=:ID");
        }
        public override cSql SQLInsertSingleRow(string _TableName, string _ColumnList, string _ParamList)
        {
            return CreateSql("INSERT INTO " + _TableName + " WITH(ROWLOCK) (" + _ColumnList + ")" + " values (" + _ParamList + ")");
        }
        public override cSql SQLUpdateByCondition(string _TableName, string _SetList, string _Condition)
        {
            return CreateSql("UPDATE " + _TableName + " WITH(ROWLOCK) SET " + _SetList + (!string.IsNullOrEmpty(_Condition) ? " WHERE " + _Condition : ""));
        }
        public override cSql SQLUpdateSingleRow(string _TableName, string _SetList)
        {
            return CreateSql("UPDATE " + _TableName + " WITH(ROWLOCK) SET " + _SetList + " WHERE " + cEntityColumn.ID_ColumnName + "=:" + cEntityColumn.ID_ColumnName);
        }
        public override cSql SQLIncrementBy(string _TableName, string _ColumnName, long _Value, string _Condition)
        {
            return CreateSql("UPDATE " + _TableName + " WITH(ROWLOCK) SET " + _ColumnName + "=" + _ColumnName + "+" + _Value.ToString() + " WHERE " + _Condition);
        }
        public override cSql SQLSelect(string _PreQueries, string _Columns, string _Source, string _Joins, string _Conditions, string _Groups, string _Orders)
        {
            return CreateSql((!string.IsNullOrEmpty(_PreQueries) ? _PreQueries + " " : "") + "SELECT " + _Columns + " FROM " + _Source + _Joins + (!string.IsNullOrEmpty(_Conditions) ? " WHERE " + _Conditions : "") + (!string.IsNullOrEmpty(_Groups) ? " GROUP BY " + _Groups : "") + (!string.IsNullOrEmpty(_Orders) ? " ORDER BY " + _Orders : ""));
        }

        public override cSql SQLSelect(string _HardCodeSql)
        {
            return CreateSql(_HardCodeSql);
        }

        public override cSql SQLSelectCount(cSql _Sql, string _CountAliasName)
        {
            return CreateSql("SELECT Count(*) " + _CountAliasName + " FROM (" + _Sql.FullSQLString + ") CountTable");
        }

        public override cSql SQLLeftJoin(string _Source, string _Conditions)
        {
            return CreateSql("LEFT JOIN " + _Source + " ON " + _Conditions);
        }

        public override cSql SQLInnerJoin(string _Source, string _Conditions)
        {
            return CreateSql("INNER JOIN " + _Source + " ON " + _Conditions);
        }

        public override string SQLDifference(string _Column, string _Value, bool _WrapValueWithString)
        {
            return "DIFFERENCE(" + _Column + " , " + (_WrapValueWithString ? "'" + _Value + "'" : _Value) + ")";
        }

        public override string SQLDateDiff(string _Interval, string _Column1, string _Column2)
        {
            return $"DATEDIFF({_Interval},{_Column1},{_Column2})";
        }


        public override cSql SQLCase(string _CaseContitions, string _ColumnName)
        {
            return CreateSql("CASE " + _CaseContitions + " END AS " + _ColumnName);
        }

        public override cSql SQLCaseWhen(string _Conditions, string _Result)
        {
            return CreateSql(" WHEN " + _Conditions + " THEN " + _Result);
        }

        public override cSql SQLCaseElse(string _ElseResult)
        {
            return CreateSql(" ELSE " + _ElseResult);
        }
        public override cSql SQLCrossApply(string _Source)
        {
            return CreateSql("CROSS APPLY " + _Source + " ");
        }

        public override cSql SQLOuterApply(string _Source)
        {
            return CreateSql("OUTER APPLY " + _Source + " ");
        }

        public override cSql SQLGroupByHaving(string _GroupBy, string _Having)
        {
            return CreateSql(_GroupBy + (!string.IsNullOrEmpty(_Having) ? " HAVING " + _Having : ""));
        }

        public override cSql WrapForRowNumber(string _TotalCountColumName, string _PartitioningColumnName, string _OrderColumnForRowNumber, string _AliasName, string _RowNumberColumnName, string _WrapingSql)
        {
            string __Sql = "SELECT " + (_TotalCountColumName != null ? _TotalCountColumName + " = COUNT(*) OVER()," : "") + "ROW_NUMBER() OVER(" + (!string.IsNullOrEmpty(_PartitioningColumnName) ? " PARTITION BY " + _PartitioningColumnName : "") + "ORDER BY " + _OrderColumnForRowNumber + ") AS '" + _RowNumberColumnName + "', " + _AliasName + ".* FROM (" + _WrapingSql + ") AS " + _AliasName;
            return CreateSql(__Sql);
        }
    }
}
