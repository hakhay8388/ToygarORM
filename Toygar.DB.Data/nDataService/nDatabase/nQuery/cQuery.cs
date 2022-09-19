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
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGenerators;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nLeftJoin;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nJoins.nInnerJoin;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nApply.nCrossApply;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nApply.nOuterApply;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nGroupBy;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nOrderBy;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nSelectionPrefixes;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nWrappers;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nApply;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryDemonstratorInterfaces;
using System.Dynamic;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nUnionElements;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nWrappers.nTake;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nOrderByQueryElement;
using System.Text.RegularExpressions;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nResult;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nCase;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nCaseWhenElement;
using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;
using Toygar.DB.Data.nDataUtils;

namespace Toygar.DB.Data.nDataService.nDatabase.nQuery
{
    public class cQuery<TEntity> : cBaseQuery<TEntity>, IQuery, ISelectionDemonstrator<TEntity> where TEntity : cBaseEntity
    {
        public string DefaultExternalAlias { get; set; }
        public List<IQueryElement> Joins { get; private set; }
        public List<IQueryElement> Applies { get; private set; }
        public List<IQueryElement> OrderBys { get; private set; }
        public List<IQueryElement> GroupBys { get; private set; }
        public List<IQueryElement> SelectionPrefixes { get; private set; }
        public List<IQueryElement> Wrappers { get; private set; }
        public List<IQueryElement> Unions { get; private set; }

        private void Init()
        {
            Joins = new List<IQueryElement>();
            OrderBys = new List<IQueryElement>();
            GroupBys = new List<IQueryElement>();
            SelectionPrefixes = new List<IQueryElement>();
            Wrappers = new List<IQueryElement>();
            Unions = new List<IQueryElement>();
            DefaultExternalAlias = AliasGenerator.GetNewAlias(EntityTable.TableName);
        }

        public cQuery(IDatabase _Database, EQueryType _QueryType)
            : base(_Database, _QueryType)
        {
            Init();
            QueryType = _QueryType;
        }

        public cQuery(IDatabase _Database, EQueryType _QueryType, Expression<Func<TEntity>> _Alias)
            : base(_Database, _QueryType, _Alias)
        {
            Init();
            QueryType = _QueryType;
        }

        public cQuery(IDatabase _Database, EQueryType _QueryType, IQuery _Query)
            : base(_Database, _QueryType, _Query)
        {
            Init();
            QueryType = _QueryType;
        }

        public cQuery(IDatabase _Database, EQueryType _QueryType, cBaseHardCodedValues _HardCodedValues, Expression<Func<TEntity>> _SubQueryExternalAlias)
          : base(_Database, _QueryType, _HardCodedValues, _SubQueryExternalAlias)
        {
            Init();
            QueryType = _QueryType;
        }

        /*  public cQuery(IDatabase _Database, IQuery _Query, Expression<Func<TEntity>> _SubQueryExternalAlias)
              : this(_Database, _Query)
          {
              Init();
              _Query.DefaultExternalAlias = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_SubQueryExternalAlias);
          }*/

        public cQuery(IDatabase _Database, EQueryType _QueryType, Expression<Func<TEntity>> _Alias, IQuery _Query)
            : base(_Database, _QueryType, _Alias, _Query)
        {
            Init();
            QueryType = _QueryType;
            _Query.DefaultExternalAlias = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_Alias);
        }

        /*    public cQuery(IDatabase _Database, Expression<Func<TEntity>> _Alias, IQuery _Query, Expression<Func<TEntity>> _SubQueryExternalAlias)
                : this(_Database, _Alias, _Query)
            {
                Init();
                _Query.DefaultExternalAlias = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_SubQueryExternalAlias);
            }
        */

        public List<string> GetFieldNames()
        {
            List<string> __ResultList = new List<string>();
            Columns.ForEach(__Item =>
            {
                if (typeof(ISelectableColumn).IsAssignableFrom(__Item.GetType()))
                {
                    __ResultList.Add(((ISelectableColumn)__Item).GetColumnName());
                }
                else if (typeof(ISelectableColumns).IsAssignableFrom(__Item.GetType()))
                {
                    __ResultList = __ResultList.Union(((ISelectableColumns)__Item).GetColumnNameList()).ToList();
                }
            });

            return __ResultList;
        }

        public cQuery<TEntity> SelectCount()
        {
            Add(Columns, new cSelectCount_QueryElement<TEntity>(this));
            return this;
        }

        public cQuery<TEntity> SelectCount(string _CustomAliasName)
        {
            Add(Columns, new cSelectCount_QueryElement<TEntity>(this, _CustomAliasName));
            return this;
        }

        public int ToCount()
        {
            if (Columns.Where(__Item => __Item.GetType() == typeof(cSelectCount_QueryElement<TEntity>)).ToList().Count > 0)
            {
                return Convert.ToInt32(ToDataTable().Rows[0][Count_AliasName]);
            }
            throw new Exception("cQuery->ToCount için cSelectCount_QueryElement elementi eklenmemiş");
        }

        public cQuery<TEntity> Select(IQuery _Query, string _ColumnAs)
        {
            string __InnerQuery = "(" + _Query.ToSql().FullSQLString + ")";
            Parameters = Parameters.Union(_Query.Parameters).ToList();
            Add(Columns, new cSelectQuery_QueryElement<TEntity>(this, __InnerQuery, _ColumnAs));
            return this;
        }

        public cQuery<TEntity> SelectAll()
        {
            Add(Columns, new cSelectAll_QueryElement<TEntity>(this));
            return this;
        }

        public cQuery<TEntity> SelectAllColumns()
        {
            EntityTable.EntityFieldList.ForEach(__Item =>
            {
                Add(Columns, new cSelectColumn_QueryElement<TEntity>(this, __Item));
            });

            return this;
        }

        public cQuery<TEntity> SelectColumn(params Expression<Func<TEntity, object>>[] _PropertyExpressions)
        {
            foreach (var _Item in _PropertyExpressions)
            {
                string __Name = Database.App.Handlers.LambdaHandler.GetParamPropName(_Item);
                Add(Columns, new cSelectColumn_QueryElement<TEntity>(this, EntityTable.GetEntityColumnByName(__Name)));
            }
            return this;
        }

        public cQuery<TEntity> SelectValue(object _Value, string _ColumnAs)
        {
            Add(Columns, new cSelectValue_QueryElement<TEntity>(this, _Value, _ColumnAs));
            return this;
        }



        public cQuery<TEntity> SelectAliasAllColumns<TAlias>(Expression<Func<TAlias>> _Alias) where TAlias : cBaseEntity
        {
            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            Add(Columns, new cSelectAliasColumn_QueryElement<TEntity, TAlias>(this, __AliasName, "*"));

            return this;
        }

        public cQuery<TEntity> SelectAliasColumn<TAlias>(Expression<Func<TAlias>> _Alias, params Expression<Func<TAlias, object>>[] _PropertyExpressions) where TAlias : cBaseEntity
        {
            foreach (var _Item in _PropertyExpressions)
            {
                string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
                string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(_Item);
                Add(Columns, new cSelectAliasColumn_QueryElement<TEntity, TAlias>(this, __AliasName, __ColumnName));
            }

            //string __EntityColumnName = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_Alias);
            //Add(Columns, new cSelectAliasColumn_QueryElement<TEntity>(this, __EntityColumnName));

            return this;
        }

        public cQuery<TEntity> SelectAliasColumn<TAlias>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpressions, string _ColumnAs = "") where TAlias : cBaseEntity
        {
            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpressions);
            Add(Columns, new cSelectAliasColumn_QueryElement<TEntity, TAlias>(this, __AliasName, __ColumnName, _ColumnAs));

            //string __EntityColumnName = Database.App.Handlers.LambdaHandler.GetObjectName<TEntity>(_Alias);
            //Add(Columns, new cSelectAliasColumn_QueryElement<TEntity>(this, __EntityColumnName));

            return this;
        }


        /// <summary>
        /// Bu metodu map tablolarında diğer entity nin adını çekmek için kullanılıyor.
        /// </summary>
        /// <typeparam name="TAlias"></typeparam>
        /// <typeparam name="TRelationEntity"></typeparam>
        /// <param name="_Alias"></param>
        /// <param name="_ColumnAs"></param>
        /// <returns></returns>
        public cQuery<TEntity> SelectAliasRelatedColumn<TAlias, TRelationEntity>(Expression<Func<TAlias>> _Alias, string _ColumnAs = "")
            where TAlias : cBaseEntity
            where TRelationEntity : cBaseEntity
        {
            cEntityTable __RelationTable = Database.EntityManager.GetEntityTableByEnitityType<TRelationEntity>();
            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            Add(Columns, new cSelectAliasColumn_QueryElement<TEntity, TAlias>(this, __AliasName, __RelationTable.TableForeing_ColumnName_For_InOtherTable, _ColumnAs));

            return this;
        }




        public cQuery<TEntity> SelectAliasColumnMerge<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnAs = "", string _Seperator = " ", params Expression<Func<TAlias, object>>[] _PropertyExpressions) where TAlias : cBaseEntity
        {
            string __MergeColumns = "";
            foreach (var __Item in _PropertyExpressions)
            {
                string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
                string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(__Item);
                if (!__MergeColumns.IsNullOrEmpty())
                {
                    __MergeColumns += "+";
                    if (!_Seperator.IsNullOrEmpty())
                    {
                        __MergeColumns += "'" + _Seperator + "'+";
                    }
                }
                __MergeColumns += __AliasName + "." + __ColumnName;
            }
            Add(Columns, new cSelectAliasColumn_QueryElement<TEntity, TAlias>(this, "", __MergeColumns, _ColumnAs));



            return this;
        }

        public cQuery<TEntity> SelectAliasColumnWithName<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnName, string _ColumnAs = "") where TAlias : cBaseEntity
        {
            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            Add(Columns, new cSelectAliasColumn_QueryElement<TEntity, TAlias>(this, __AliasName, _ColumnName, _ColumnAs));
            return this;
        }

        public cQuery<TEntity> SelectColumn<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            cEntityTable __RelationTable = Database.EntityManager.GetEntityTableByEnitityType<TRelationEntity>();
            Add(Columns, new cSelectColumn_QueryElement<TEntity>(this, EntityTable.GetEntityColumnByName(__RelationTable.TableForeing_ColumnName_For_InOtherTable)));
            return this;
        }

        public cQuery<TEntity> SelectColumn<TRelationEntity>(params Expression<Func<TEntity, object>>[] _PropertyExpressions) where TRelationEntity : cBaseEntity
        {
            SelectColumn<TRelationEntity>();
            SelectColumn(_PropertyExpressions);
            return this;
        }

        public string Max_AliasName
        {
            get
            {
                foreach (var __Item in Columns)
                {
                    if (typeof(cMaxValueColumn_QueryElement<TEntity>).IsAssignableFrom(__Item.GetType()))
                    {
                        return ((cMaxValueColumn_QueryElement<TEntity>)__Item).MaxValueAlias;
                    }
                }
                return "";
            }
        }

        public string Min_AliasName
        {
            get
            {
                foreach (var __Item in Columns)
                {
                    if (typeof(cMinValueColumn_QueryElement<TEntity>).IsAssignableFrom(__Item.GetType()))
                    {
                        return ((cMinValueColumn_QueryElement<TEntity>)__Item).MinValueAlias;
                    }
                }
                return "";
            }
        }

        public string Sum_AliasName
        {
            get
            {
                foreach (var __Item in Columns)
                {
                    if (typeof(cSumValueColumn_QueryElement<TEntity>).IsAssignableFrom(__Item.GetType()))
                    {
                        return ((cSumValueColumn_QueryElement<TEntity>)__Item).SumValueAlias;
                    }
                }
                return "";
            }
        }

        public string Count_AliasName
        {
            get
            {
                foreach (var __Item in Columns)
                {
                    if (typeof(cSelectCount_QueryElement<TEntity>).IsAssignableFrom(__Item.GetType()))
                    {
                        return ((cSelectCount_QueryElement<TEntity>)__Item).CountValueAlias;
                    }
                }
                return "";
            }
        }


        public cQuery<TEntity> Max(Expression<Func<TEntity, object>> _PropertyExpressions)
        {
            string __Name = Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpressions);
            Add(Columns, new cMaxValueColumn_QueryElement<TEntity>(this, EntityTable.GetEntityColumnByName(__Name)));
            return this;
        }
        public cQuery<TEntity> Max<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnAs, params Expression<Func<TAlias, object>>[] _PropertyExpressions) where TAlias : cBaseEntity
        {


            foreach (var _Item in _PropertyExpressions)
            {
                string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
                string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(_Item);

                Add(Columns, new cMaxValueColumn_QueryElement<TEntity>(this, __AliasName + "." + __ColumnName, _ColumnAs, false));

            }
            return this;

        }
        public cQuery<TEntity> Avg<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnAs, params Expression<Func<TAlias, object>>[] _PropertyExpressions) where TAlias : cBaseEntity
        {


            foreach (var _Item in _PropertyExpressions)
            {
                string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
                string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(_Item);

                Add(Columns, new cAvgValueColumn_QueryElement<TEntity>(this, __AliasName + "." + __ColumnName, _ColumnAs, false));

            }
            return this;

        }

        public cQuery<TEntity> Min(Expression<Func<TEntity, object>> _PropertyExpressions)
        {
            string __Name = Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpressions);
            Add(Columns, new cMinValueColumn_QueryElement<TEntity>(this, EntityTable.GetEntityColumnByName(__Name)));
            return this;
        }

        public cQuery<TEntity> Sum(Expression<Func<TEntity, object>> _PropertyExpressions)
        {
            string __Name = Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpressions);
            Add(Columns, new cSumValueColumn_QueryElement<TEntity>(this, EntityTable.GetEntityColumnByName(__Name)));
            return this;
        }
        public cQuery<TEntity> IfIsNull<TAlias, TAlias2>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpressions, Expression<Func<TAlias2>> _Alias2, Expression<Func<TAlias2, object>> _PropertyExpressions2, string _ColumnAs) 
            where TAlias : cBaseEntity
            where TAlias2 : cBaseEntity
        {
            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpressions);
            string __AliasName2 = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias2>(_Alias2);
            string __ColumnName2 = Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpressions2);

            Add(Columns, new cIfIsNullValueColumn_QueryElement<TEntity>(this, __AliasName+"."+__ColumnName, __AliasName2 + "." + __ColumnName2,_ColumnAs));
            return this;
        }
        public cQuery<TEntity> CastAsVarchar<TAlias>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpressions,string _ColumnAs)
            where TAlias : cBaseEntity
        {
            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpressions);

            Add(Columns, new cCastAsVarcharValueColumn_QueryElement<TEntity>(this, __AliasName + "." + __ColumnName, _ColumnAs));
            return this;
        }

        public cQuery<TEntity> Sum<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnAs, params Expression<Func<TAlias, object>>[] _PropertyExpressions) where TAlias : cBaseEntity
        {


            foreach (var _Item in _PropertyExpressions)
            {
                string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
                string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(_Item);

                Add(Columns, new cSumValueColumn_QueryElement<TEntity>(this, __AliasName + "." + __ColumnName, _ColumnAs, false));

            }
            return this;

        }


        public cQuery<TEntity> Avg(Expression<Func<TEntity, object>> _PropertyExpressions)
        {
            string __Name = Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpressions);
            Add(Columns, new cAvgValueColumn_QueryElement<TEntity>(this, EntityTable.GetEntityColumnByName(__Name)));
            return this;
        }

        public cQuery<TEntity> Avg<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnAs, Expression<Func<TAlias, object>> _PropertyExpression) where TAlias : cBaseEntity
        {
            string __AliasName = Database.App.Handlers.LambdaHandler.GetObjectName<TAlias>(_Alias);
            string __ColumnName = Database.App.Handlers.LambdaHandler.GetParamPropName(_PropertyExpression);

            Add(Columns, new cAvgValueColumn_QueryElement<TEntity>(this, __AliasName + "." + __ColumnName, _ColumnAs, false));

            return this;
        }

        /*  public void Cast(Action<cQuery<TEntity>> _Action, DBField _CastAtribute)
          {
              List<IQueryElement>  __TempColumns = Columns; 
              Columns = new List<IQueryElement>();
              _Action(this);
              if (Columns.Count != 1)
              {
                  throw new Exception("Cast işlemi içerisinde sadece 1 tane select işlemi yapılmalı.!");
              }
              IQueryElement __QueryElement = Columns[0];
              Columns = __TempColumns;
          }*/


        public cQuery<TEntity> SelectID(string _ColumnAs = "")
        {
            Add(Columns, new cSelectColumn_QueryElement<TEntity>(this, EntityTable.GetEntityIDColumn(), _ColumnAs));
            return this;
        }

        public override cSql ToSql()
        {
            if (QueryType.ID == EQueryType.Select.ID)
            {
                string __Unions = CollectUnions();
                string __Columns = CollectPrefixes() + CollectColumns();
                string __DataSources = CollectDataSource();
                string __Joins = CollectJoins();
                string __OrderBy = CollectOrderBy();
                string __GroupBy = CollectGroupBy();
                string __Filters = CollectFilters();
                cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLSelect(__Unions, __Columns, __DataSources, __Joins, __Filters, __GroupBy, __OrderBy);
                __Sql = ApplyWrapper(0, __Sql);
                SetParameters(__Sql);
                return __Sql;
            }
            else if (QueryType.ID == EQueryType.Delete.ID)
            {
                string __DataSources = CollectDataSource();
                string __Filters = CollectFilters();
                cSql __Sql = Database.Catalogs.RowOperationSQLCatalog.SQLDeleteByCondition(__DataSources, __Filters);
                SetParameters(__Sql);
                return __Sql;
            }
            throw new Exception("QueryType Error!");
        }


        public cRowNumber<TEntity> RowNumber(string _TotalCountColumName = null)
        {
            //if (OrderBys.Count > 0) throw new Exception("RowNumber ile OrderBy beraber kullanılamaz");
            cRowNumber<TEntity> __RowNumber = new cRowNumber<TEntity>(this, _TotalCountColumName);
            if (OrderBys.Count > 0)
            {
                for (int i = OrderBys.Count - 1; i > -1; i--)
                {
                    string[] __Direction = OrderBys[i].ToElementString().Trim().Split(" ");
                    string[] __WithAlias = __Direction[0].Split(".");
                    if (__WithAlias.Length > 1)
                    {
                        if (__Direction[__Direction.Length - 1].Trim().ToUpper() == "ASC")
                        {
                            __RowNumber.OrderBy().Asc(__WithAlias[1]);
                        }
                        else
                        {
                            __RowNumber.OrderBy().Desc(__WithAlias[1]);
                        }
                    }
                    else
                    {
                        if (__Direction[__Direction.Length - 1].Trim().ToUpper() == "ASC")
                        {
                            __RowNumber.OrderBy().Asc(__WithAlias[0]);
                        }
                        else
                        {
                            __RowNumber.OrderBy().Desc(__WithAlias[0]);
                        }
                    }

                    //string __Tem = OrderBys[i].ToElementString();
                    //__RowNumber.OrderBys.Insert(0, OrderBys[i]);

                }
                OrderBys.Clear();
            }

            Wrappers.Add(new cRowNumberWrapperElement<TEntity>(this, __RowNumber));
            return __RowNumber;
        }

        public cQuery<TEntity> Take(int _Start, int _End, string _TotalCountColumName, bool _Asc = true, string _OrderColumnName = null)
        {
            bool __Found = false;
            for (int i = Wrappers.Count - 1; i > -1; i--)
            {
                if (typeof(cRowNumberWrapperElement<TEntity>).IsAssignableFrom(Wrappers[i].GetType()))
                {
                    __Found = true;
                    break;
                }

            }

            if (!__Found)
            {
                if (_Asc)
                {
                    if (_OrderColumnName == null)
                    {
                        RowNumber(_TotalCountColumName).OrderBy().Asc(__Item => __Item.ID);
                    }
                    else
                    {
                        RowNumber(_TotalCountColumName).OrderBy().Asc(_OrderColumnName);
                    }
                }
                else
                {
                    if (_OrderColumnName == null)
                    {
                        RowNumber(_TotalCountColumName).OrderBy().Desc(__Item => __Item.ID);
                    }
                    else
                    {
                        RowNumber(_TotalCountColumName).OrderBy().Desc(_OrderColumnName);
                    }

                }
            }

            cTake<TEntity> __Take = new cTake<TEntity>(this, _Start, _End);
            Wrappers.Add(new cTakeWrapperElement<TEntity>(this, __Take));
            return this;
        }

        protected cSql ApplyWrapper(int _Index, cSql _Sql)
        {
            if (Wrappers.Count > _Index)
            {
                cSql __Sql = new cSql(Database.DefaultConnection, Wrappers[_Index].ToElementString(_Sql));
                if (_Index + 1 < Wrappers.Count)
                {
                    return ApplyWrapper(_Index + 1, __Sql);
                }
                return __Sql;
            }
            return _Sql;
        }

        public IBaseFilterForOperands<TEntity, TEntity> Where()
        {
            return new cWhere<TEntity>(this);
        }

        protected string CollectUnions()
        {
            string __Result = "";
            Unions.ForEach(__Item =>
            {
                __Result += __Item.ToElementString();
            });
            return __Result;
        }

        private string CollectJoins()
        {
            string __Result = "";
            Joins.ForEach(__Item =>
            {
                __Result += __Item.ToElementString();
                __Result += " \n";
            });
            return __Result;
        }

        private string CollectOrderBy()
        {
            string __Result = "";
            OrderBys.ForEach(__Item =>
            {
                __Result += __Item.ToElementString();
            });
            return __Result;
        }

        private string CollectPrefixes()
        {
            string __Result = "";
            SelectionPrefixes.ForEach(__Item =>
            {
                __Result += __Item.ToElementString();
            });
            return __Result;
        }

        private string CollectGroupBy()
        {
            string __Result = "";
            GroupBys.ForEach(__Item =>
            {
                __Result += __Item.ToElementString();
            });
            return __Result;
        }

        public IJoinable<TEntity, TJoin> Left<TJoin>()
             where TJoin : cBaseEntity
        {
            cLeft<TEntity, TJoin> __LeftJoin = new cLeft<TEntity, TJoin>(this);
            return __LeftJoin;
        }

        public IJoinable<TEntity, TJoin> Inner<TJoin>()
           where TJoin : cBaseEntity
        {
            cInner<TEntity, TJoin> __InnerJoin = new cInner<TEntity, TJoin>(this);
            return __InnerJoin;
        }

        public cCase<TEntity> Case(string _ColumnName)
        {
            cCase<TEntity> __Case = new cCase<TEntity>(this, _ColumnName);
            cCaseWhen_QueryElement<TEntity> __CaseWhen_QueryElement = new cCaseWhen_QueryElement<TEntity>(this, __Case);
            Add(Columns, __CaseWhen_QueryElement);
            return __Case;
        }

        public IApplyable<TEntity> Cross()
        {
            cCross<TEntity> __Cross = new cCross<TEntity>(this);
            return __Cross;
        }

        public IApplyable<TEntity> Outer()
        {
            cOuter<TEntity> __Outer = new cOuter<TEntity>(this);
            return __Outer;
        }

        public List<TEntity> ToList()
        {
            return ToList<TEntity>();
        }

        public object[] ToArray(object _IfNoResultSendToBack = null)
        {
            List<TEntity> __Temp = ToList();
            if (__Temp.Count > 0)
            {
                return __Temp.ToArray(); 
            }
            else
            {
                if (_IfNoResultSendToBack == null)
                {
                    return __Temp.ToArray();
                }
                else
                {
                    return new object[] { _IfNoResultSendToBack };
                }
            }

        }

        public List<TProjection> ToList<TProjection>(Action<TProjection> _Action = null) where TProjection : cBaseEntity
        {
            DataTable __DataTable = ToDataTable();
            List<TProjection> __Result = new List<TProjection>();
            foreach (DataRow __Row in __DataTable.Rows)
            {
                TProjection __Entity = (TProjection)Database.App.Factories.HookedObjectFactory.PropertyHookedObjectFactory.GetInstance<TProjection>();
                __Row.Fill(__Entity);
                __Entity.GetType().SetPropertyValue(__Entity, "Database", Database);
                __Entity.GetType().SetPropertyValue(__Entity, "App", Database.App);
                __Entity.GetType().SetPropertyValue(__Entity, "IsValid", true);
                __Result.Add(__Entity);

                _Action?.Invoke(__Entity);
            }
            return __Result;
        }

        public List<dynamic> ToDynamicObjectList(Action<dynamic> _Action = null)
        {
            DataTable __DataTable = ToDataTable();
            List<object> __Result = new List<object>();
            foreach (DataRow __Row in __DataTable.Rows)
            {

                ExpandoObject __Dynamic = new ExpandoObject();
                IDictionary<string, object> __UnderlyingObject = __Dynamic;

                for (int i = 0; i < __DataTable.Columns.Count; i++)
                {
                    __UnderlyingObject.Add(__DataTable.Columns[i].ColumnName, __Row[__DataTable.Columns[i].ColumnName]);
                }

                __Result.Add(__Dynamic);
                _Action?.Invoke(__Dynamic);
            }
            return __Result;
        }

        public cResultList<List<dynamic>> ToDynamicObjectListInResultList(string _TotalRecordColumnName, Action<dynamic> _Action = null)
        {
            List<dynamic> __ResultList = ToDynamicObjectList(_Action);
            if (__ResultList.Count > 0)
            {
                IDictionary<string, object> __UnderlyingObject = __ResultList[0];
                int __Count = (int)__UnderlyingObject[_TotalRecordColumnName];
                return new cResultList<List<dynamic>>(__ResultList, __Count);
            }
            else
            {
                return new cResultList<List<dynamic>>(__ResultList, 0);
            }
        }

        public DataTable ToDataTable()
        {
            cSql __Sql = ToSql();
            DataTable __Result = Database.DefaultConnection.Query(__Sql);
            return __Result;
        }

        public int ExecuteForDeleteAndUpdate()
        {
            cSql __Sql = ToSql();
            int __Result = Database.DefaultConnection.Execute(__Sql);
            return __Result;
        }


        public cGroupBy<TEntity, TEntity> GroupBy(params Expression<Func<object>>[] _Columns)
        {
            return new cGroupBy<TEntity, TEntity>(this, _Columns);
        }

        public cGroupBy<TEntity, TEntity> GroupBy(params Expression<Func<TEntity, object>>[] _Columns)
        {
            return new cGroupBy<TEntity, TEntity>(this, _Columns);
        }

        public cGroupBy<TEntity, TEntity> GroupBy(params string[] _Columns)
        {
            return new cGroupBy<TEntity, TEntity>(this, _Columns);
        }

        public cGroupBy<TEntity, TEntity> GroupBy(string _Column)
        {
            return new cGroupBy<TEntity, TEntity>(this, _Column);
        }

        public cGroupBy<TEntity, TEntity> GroupBy<TRelationEntity>() where TRelationEntity : cBaseEntity
        {
            cEntityTable __RelationTable = Database.EntityManager.GetEntityTableByEnitityType<TRelationEntity>();
            cEntityTable __Table = Database.EntityManager.GetEntityTableByEnitityType<TEntity>();
            cEntityColumn __EntityColumn = __Table.GetEntityColumnByName(__RelationTable.TableForeing_ColumnName_For_InOtherTable);
            if (__EntityColumn != null)
            {
                return new cGroupBy<TEntity, TEntity>(this, __RelationTable.TableForeing_ColumnName_For_InOtherTable);
            }
            else
            {
                throw new Exception(__Table.TableName + " tablosunun içinde " + __RelationTable.TableForeing_ColumnName_For_InOtherTable + " kolonu bulunamadı!");
            }
        }


        public cGroupBy<TEntity, TAlias> GroupBy<TAlias>(Expression<Func<TAlias>> _Alias, Expression<Func<TAlias, object>> _PropertyExpressions) where TAlias : cBaseEntity
        {
            return new cGroupBy<TEntity, TAlias>(this, _Alias, _PropertyExpressions);
        }

        public cGroupBy<TEntity, TAlias> GroupBy<TAlias>(Expression<Func<TAlias>> _Alias, string _ColumnName) where TAlias : cBaseEntity
        {
            return new cGroupBy<TEntity, TAlias>(this, _Alias, _ColumnName);
        }


        public IGroupBy<TEntity, TEntity> GroupBy<TRelationEntity>(params Expression<Func<TEntity, object>>[] _Columns) where TRelationEntity : cBaseEntity
        {
            GroupBy(_Columns);
            return GroupBy<TRelationEntity>();
        }


        public cGroupBy<TEntity, TAlias> GroupBy<TAlias, TRelationEntity>(Expression<Func<TAlias>> _Alias)
            where TAlias : cBaseEntity
            where TRelationEntity : cBaseEntity
        {
            cEntityTable __RelationTable = Database.EntityManager.GetEntityTableByEnitityType<TRelationEntity>();
            return new cGroupBy<TEntity, TAlias>(this, _Alias, __RelationTable.TableForeing_ColumnName_For_InOtherTable);
        }


        public IOrderBy<TEntity> OrderBy()
        {
            return new cOrderBy<TEntity>(this);
        }

        public ISelectionDemonstrator<TEntity> Top(int _Count)
        {
            SelectionPrefixes.Add(new cTop_QueryElement<TEntity>(this, _Count));
            return this;
        }

        public ISelectionDemonstrator<TEntity> Distinct()
        {
            SelectionPrefixes.Add(new cDistinct_QueryElement<TEntity>(this));
            return this;
        }

        public cQuery<TEntity> Union(IQuery _Query)
        {
            Unions.Add(new cUnion_QueryElement<TEntity>(this, _Query));
            return this;
        }


    }
}
