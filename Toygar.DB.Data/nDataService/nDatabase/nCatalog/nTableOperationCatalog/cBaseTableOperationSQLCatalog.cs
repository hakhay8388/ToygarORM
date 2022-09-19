using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nTableOperationCatalog
{
    public abstract class cBaseTableOperationSQLCatalog : cBaseCatalogOperations
    {
        public cBaseTableOperationSQLCatalog(IDatabase _Database)
            :base(_Database)
        {
        }

        
        public abstract cSql SQLDropNotNull(string _TableName, string _ColumnName, string _ColumnDefinition);
        public abstract cSql SQLDropIndex(string _TableName, string _IndexName);
        public abstract cSql SQLRebuildIndex(string _TableName, string _IndexName);


        public abstract string GetNotNullColumnString();
        public abstract string GetIdentityString(int _StartValue, int _IncrementValue);
        public abstract string GetIsNullColumnString();

        public abstract cSql SQLAlterColumn(string _TableName, string _ColumnName, string _ColumnDefinition);
        public abstract cSql SQLAlterNotNullColumn(string _TableName, string _ColumnName, string _ColumnDefinition, string _Default, bool _Identity = false, int _IdentityStart = 1, int _IncrementValue = 1);
        public abstract cSql SQLAddNotNullColumn(string _TableName, string _ColumnName, string _ColumnDefinition, string _Default, bool _Identity = false, int _IdentityStart = 1, int _IncrementValue = 1);
        public abstract cSql SQLAddPrimaryKey(string _TableName, string _ConstraintName, string _PrimaryColumnName);
        public abstract cSql SQLAddUniqueConstraint(string _TableName, string _ConstraintName, bool _Clustered, string _ColumnNames);
        public abstract cSql SQLAddPrimaryConstraint(string _TableName, string _ConstraintName, string _ColumnNames);

        public abstract cSql SQLAddFullTextIndex(string _TableName, string _PK_ConstraintName, string _IndexExpression);
        public abstract cSql SQLAddIndex(string _TableName, string _IndexName, bool _Unique, bool _Clustered, string _IndexExpression);

        public cSql SQLDropTable(string _TableName)
        {
            return CreateSql("DROP TABLE " + _TableName);
        }
        public cSql SQLDropConstraint(string _TableName, string _ConstraintName)
        {
            return CreateSql("ALTER TABLE " + _TableName + " DROP CONSTRAINT " + _ConstraintName);
        }
        public cSql SQLDropColumn(string _TableName, string _ColumnName)
        {
            return CreateSql("ALTER TABLE " + _TableName + " DROP COLUMN " + _ColumnName);
        }
        public cSql SQLAddTable(string _TableName, string _ColumnDefinitionList)
        {
            return CreateSql("CREATE TABLE " + _TableName + " (" + _ColumnDefinitionList + ")");
        }
        public cSql SQLAddColumn(string _TableName, string _ColumnName, string _ColumnDefinition)
        {
            return CreateSql("ALTER TABLE " + _TableName + " ADD " + _ColumnName + " " + _ColumnDefinition);
        }
        public cSql SQLAddForeignKey(string _ConstraintName, string _ParantedTableName, string _ParantedColumnName, string _ReferencedTableName, string _ReferencedColumnName)
        {
            return CreateSql("ALTER TABLE " + _ParantedTableName + " ADD CONSTRAINT " + _ConstraintName + " FOREIGN KEY (" + _ParantedColumnName + ") REFERENCES " + _ReferencedTableName + "(" + _ReferencedColumnName + ")");
        }


    }
}
