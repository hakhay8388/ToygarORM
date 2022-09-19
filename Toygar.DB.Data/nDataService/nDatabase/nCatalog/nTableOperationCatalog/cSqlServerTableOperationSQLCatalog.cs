using Toygar.DB.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nCatalog.nTableOperationCatalog
{
    public class cSqlServerTableOperationSQLCatalog : cBaseTableOperationSQLCatalog
    {
        public cSqlServerTableOperationSQLCatalog(IDatabase _Database)
            : base(_Database)
        { 
        }

        public override cSql SQLAddNotNullColumn(string _TableName, string _ColumnName, string _ColumnDefinition, string _Default, bool _Identity = false, int _IdentityStart = 1, int _IncrementValue = 1)
        {
            if (_Identity)
            {
                return CreateSql("ALTER TABLE " + _TableName + " ADD " + _ColumnName + " " + _ColumnDefinition + " IDENTITY(" + _IdentityStart.ToString() + "," + _IncrementValue.ToString() + ") NOT NULL ");
            }
            else
            {
                return CreateSql("ALTER TABLE " + _TableName + " ADD " + _ColumnName + " " + _ColumnDefinition + " NOT NULL DEFAULT '" + _Default + "'");
            }            
        }
        public override cSql SQLAlterColumn(string _TableName, string _ColumnName, string _ColumnDefinition)
        {
            return CreateSql("ALTER TABLE " + _TableName + " ALTER COLUMN " + _ColumnName + " " + _ColumnDefinition);
        }


        public override cSql SQLAlterNotNullColumn(string _TableName, string _ColumnName, string _ColumnDefinition, string _Default, bool _Identity = false, int _IdentityStart = 1, int _IncrementValue = 1)
        {
            //return CreateSql("ALTER TABLE " + _TableName + " ALTER COLUMN " + _ColumnName + " " + _ColumnDefinition + " NOT NULL");
            if (_Identity)
            {
                return CreateSql("ALTER TABLE " + _TableName + " ALTER COLUMN " + _ColumnName + " " + _ColumnDefinition + " IDENTITY(" + _IdentityStart.ToString() + "," + _IncrementValue.ToString() + ") NOT NULL ");
            }
            else
            {
                return CreateSql("ALTER TABLE " + _TableName + " ALTER COLUMN " + _ColumnName + " " + _ColumnDefinition + " NOT NULL");
            }            
        }


        public override cSql SQLAddIndex(string _TableName, string _IndexName, bool _Unique, bool _Clustered, string _IndexExpression)
        {
            return CreateSql("CREATE " + (_Unique ? " UNIQUE " : "") + (_Clustered ? " CLUSTERED " : "") + " INDEX " + _IndexName + " ON " + _TableName + " (" + _IndexExpression + ")");            
        }

        public override cSql SQLAddFullTextIndex(string _TableName, string _PK_ConstraintName, string _IndexExpression)
        {
            return CreateSql("CREATE FULLTEXT INDEX ON " + _TableName + " (" + _IndexExpression + " TYPE COLUMN FILEEXTENSION) KEY INDEX " + _PK_ConstraintName);     
        }

        public override cSql SQLDropIndex(string _TableName, string _IndexName)
        {
            if (_IndexName.Equals("FULLTEXT01"))
            {
                return CreateSql("DROP FULLTEXT INDEX ON " + _TableName);
            }
            else
            {
                return CreateSql("DROP INDEX " + _TableName + "." + _IndexName);
            }
        }
        public override cSql SQLRebuildIndex(string _TableName, string _IndexName)
        {
            return CreateSql("ALTER INDEX " + _IndexName + " ON " + _TableName + " REBUILD");
        }
        public override cSql SQLDropNotNull(string _TableName, string _ColumnName, string _ColumnDefinition)
        {
            return CreateSql("ALTER TABLE " + _TableName + " ALTER COLUMN " + _ColumnName + " " + _ColumnDefinition + " NULL");
        }
       
        public override cSql SQLAddPrimaryKey(string _TableName, string _ConstraintName, string _PrimaryColumnName)
        {
            return CreateSql("ALTER TABLE " + _TableName + " ADD CONSTRAINT " + _ConstraintName + " PRIMARY KEY(" + _PrimaryColumnName + ")");
        }

        public override cSql SQLAddUniqueConstraint(string _TableName, string _ConstraintName, bool _Clustered, string _ColumnNames)
        {
            if (_Clustered)
            {
                return CreateSql("ALTER TABLE " + _TableName + " ADD CONSTRAINT " + _ConstraintName + " UNIQUE CLUSTERED (" + _ColumnNames + ")");
            }
            else
            {
                return CreateSql("ALTER TABLE " + _TableName + " ADD CONSTRAINT " + _ConstraintName + " UNIQUE NONCLUSTERED (" + _ColumnNames + ")");
            }

        }

        public override cSql SQLAddPrimaryConstraint(string _TableName, string _ConstraintName, string _ColumnNames)
        {
            return CreateSql("ALTER TABLE " + _TableName + " ADD CONSTRAINT " + _ConstraintName + " PRIMARY KEY (" + _ColumnNames + ")");
        }

        public override string GetNotNullColumnString()
        {
            return "not null";
        }

        public override string GetIsNullColumnString()
        {
            return "is null";
        }

        public override string GetIdentityString(int _StartValue, int _IncrementValue)
        {
            return "IDENTITY(" + _StartValue.ToString() + "," + _IncrementValue.ToString() + ")";
        }
    }
}
