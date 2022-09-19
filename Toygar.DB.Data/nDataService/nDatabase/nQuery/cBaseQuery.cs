using Toygar.Base.Core.nApplication;
using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nSourceQueryElement;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nGeneralElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements;
using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataUtils;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery
{
    public abstract class cBaseQuery<TEntity> : IBaseQuery where TEntity : cBaseEntity
    {
        public EQueryType QueryType { get; set; }

        public string DefaultAlias { get; set; }
        public IDatabase Database { get; private set; }
        public cEntityTable EntityTable { get; private set; }

        public List<IQueryElement> DataSource { get; private set; }
        public List<IQueryElement> Columns { get; protected set; }
        public List<IFilterElement> Filters { get; private set; }
        public List<cParameter> Parameters { get; set; }


        public abstract cSql ToSql();

        private void Init(IDatabase _Database)
        {
            Database = _Database;
            DataSource = new List<IQueryElement>();
            Columns = new List<IQueryElement>();
            Filters = new List<IFilterElement>();
            Parameters = new List<cParameter>();
            EntityTable = Database.EntityManager.GetEntityTableByEnitityType<TEntity>();
        }

        public cBaseQuery(IDatabase _Database, EQueryType _QueryType)
        {
            Init(_Database);
            QueryType = _QueryType;
            Add(DataSource, new cTableSource_QueryElement<TEntity>(this));
            DefaultAlias = AliasGenerator.GetNewAlias(EntityTable.TableName);
        }

        public cBaseQuery(IDatabase _Database, EQueryType _QueryType, Expression<Func<TEntity>> _Alias)
            : this(_Database, _QueryType)
        {
            DefaultAlias = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_Alias);
        }

        public cBaseQuery(IDatabase _Database, EQueryType _QueryType, IQuery _Query)
        {
            Init(_Database);
            QueryType = _QueryType;
            Add(DataSource, new cSubQuerySource_QueryElement<TEntity>(this, _Query));
            DefaultAlias = _Query.DefaultExternalAlias;
        }

        public cBaseQuery(IDatabase _Database, EQueryType _QueryType, IQuery _Query, Expression<Func<TEntity>> _SubQueryExternalAlias)
        {
            Init(_Database);
            QueryType = _QueryType;
            Add(DataSource, new cSubQuerySource_QueryElement<TEntity>(this, _Query));
            _Query.DefaultExternalAlias = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_SubQueryExternalAlias);
            DefaultAlias = _Query.DefaultExternalAlias;
        }

        public cBaseQuery(IDatabase _Database, EQueryType _QueryType, cBaseHardCodedValues _HardCodedValues, Expression<Func<TEntity>> _SubQueryExternalAlias)
        {
            Init(_Database);
            QueryType = _QueryType;
            Add(DataSource, new cHardCodedSql_QueryElement<TEntity>(this, _HardCodedValues, _SubQueryExternalAlias));
            DefaultAlias = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_SubQueryExternalAlias);
        }


        public cBaseQuery(IDatabase _Database, EQueryType _QueryType, Expression<Func<TEntity>> _Alias, IQuery _Query)
            : this(_Database, _QueryType,  _Query)
        {
            DefaultAlias = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_Alias);
            _Query.DefaultExternalAlias = DefaultAlias;
        }

        public cBaseQuery(IDatabase _Database, EQueryType _QueryType, Expression<Func<TEntity>> _Alias, IQuery _Query, Expression<Func<TEntity>> _SubQueryExternalAlias)
            : this(_Database, _QueryType, _Alias, _Query)
        {
            _Query.DefaultExternalAlias = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_SubQueryExternalAlias);
        }

        public cBaseQuery<TEntity> UnmanagedSelection(string _Value)
        {
            Add(Columns, new cUnmanagedSelection_QueryElement<TEntity>(this, _Value));
            return this;
        }



        public void Add(List<IQueryElement> _List, IQueryElement _Item)
        {
            if (_List.Count > 0)
            {
                _List.Add(new cSeparator_QueryElement<TEntity>(this));
                _List.Add(_Item);
            }
            else
            {
                _List.Add(_Item);
            }
        }

        protected string SetParameters(cSql _Sql)
        {
            string __Result = "";
            Parameters.ForEach(__Item =>
            {
                _Sql.SetParameter(__Item.ParamName, __Item.ParamValue);
            });
            return __Result;
        }

        protected string CollectDataSource()
        {
            string __Result = "";
            DataSource.ForEach(__Item =>
            {
                __Result += __Item.ToElementString();
            });
            return __Result;
        }

        protected string CollectColumns()
        {
            string __Result = "";
            Columns.ForEach(__Item =>
            {
                __Result += __Item.ToElementString();
            });
            return __Result;
        }


        protected string CollectFilters()
        {
            string __Result = "";
            Filters.ForEach(__Item =>
            {
                __Result += __Item.ToElementString();

            });
            return __Result;
        }

      

        /* public void Test<T>(Expression<Func<T, object>> _PropertyExpression)
         {
             string __Name = App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpression);
             Type __ProbType = App.Handlers.LambdaHandler.GetParamPropType(_PropertyExpression);
             //string __ProbName = App.Handlers.LambdaHandler.GetObjectNameAndPropName(_PropertyExpression);
             //string __ProbName = App.Handlers.LambdaHandler.(_PropertyExpression);
         }

         public void Test2<T>(Expression<Func<T>> _PropertyExpression) where T : class
         {
             string __ObjectName = App.Handlers.LambdaHandler.GetObjectName(_PropertyExpression);
             string __ObjectName2 = App.Handlers.LambdaHandler.GetObjectName<T>(_PropertyExpression);
         }

         public void Test3(Expression<Func<object>> _PropertyExpression)
         {
             string __ObjectName = App.Handlers.LambdaHandler.GetObjectNameAndPropName(_PropertyExpression);
             string __ObjectName2 = App.Handlers.LambdaHandler.GetObjectPropName(_PropertyExpression);
             Type __ObjectType = App.Handlers.LambdaHandler.GetObjectType(_PropertyExpression);
         }*/
    }
}
