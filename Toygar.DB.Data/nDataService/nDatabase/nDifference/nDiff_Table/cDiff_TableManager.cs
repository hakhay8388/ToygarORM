using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable;
using Toygar.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.DB.Data.nDataService.nDatabase.nMetadata.nTable.nColumn;
using Toygar.DB.Data.nDataService.nDatabase.nSql;

namespace Toygar.DB.Data.nDataService.nDatabase.nDifference.nDiff_Table
{
    public class cDiff_TableManager
    {
        cDifferenceManager DifferenceManager { get; set; }
        List<cDiff_Table> Diff_Tables { get; set; }

        public cDiff_TableManager(cDifferenceManager _DifferenceManager)
        {
            DifferenceManager = _DifferenceManager;
        }

		public void DropAllConstraints()
		{
			DifferenceManager.Database.MetadataManager.TableManager.DropForeignKeys();
			DifferenceManager.Database.MetadataManager.TableManager.DropIndexes();
			DifferenceManager.Database.MetadataManager.TableManager.DropDefaultConstraints();
		}

		public void Synchronize()
        {
			DropAllConstraints();


			DifferenceManager.Database.MetadataManager.TableManager.Reload();

            foreach (cDiff_Table __DiffTable in Diff_Tables)
            {
                if (__DiffTable.EntityTable == null)
                {
                    __DiffTable.DBTable.Drop();
                }

                
				if (__DiffTable.DBTable == null)
                {
                    List<cColumn> __ColumnList = new List<cColumn>();
                    foreach (cEntityColumn __Field in __DiffTable.EntityTable.EntityFieldList)
                    {
                        cColumn __Column = new cColumn(__Field.ColumnName, __Field.DataType, __Field.DBField.Length, __Field.DBField.Nullable, __Field.DBField.Identity, __Field.DBField.IdentityStart, __Field.DBField.IdentityIncrement, __Field.DBField.DecimalBigger, __Field.DBField.DecimalLower);
                        __ColumnList.Add(__Column);
                    }
                    cTable __Table = DifferenceManager.Database.MetadataManager.TableManager.CreateTable(__DiffTable.EntityTable.TableName, __ColumnList);
                }
            }

            foreach (cDiff_Table __DiffTable in Diff_Tables)
            {
                if (__DiffTable.EntityTable != null && __DiffTable.DBTable != null)
                {
                    cTable __Table = __DiffTable.DBTable.TableManager.GetTableByName(__DiffTable.EntityTable.TableName);

                    foreach (cEntityColumn __Field in __DiffTable.EntityTable.EntityFieldList)
                    {
                        cColumn __Column = __Table.GetColumnByName(__Field.ColumnName);
                        if (__Column != null)
                        {
                            if ((__Column.DataType.ID != __Field.DataType.ID)
                               || (__Column.DataType.UseLength && __Column.DataType.ID == __Field.DataType.ID && __Column.ColumnEnitity.Precision != __Field.DBField.Length)
                               || (__Column.ColumnEnitity.Nullable !=  __Field.DBField.Nullable)
                               || (__Column.IdentityEnitity == null && __Field.DBField.Identity)
                               || (__Column.IdentityEnitity != null && !__Field.DBField.Identity)
                               || (__Column.IdentityEnitity != null && __Field.DBField.Identity && (__Column.IdentityEnitity.SeedValue != __Field.DBField.IdentityStart || __Column.IdentityEnitity.IncrementValue != __Field.DBField.IdentityIncrement)))
                            {
                                if (__Field.DBField.Identity && __Column.IdentityEnitity == null)
                                {
                                    __Table.ModifyColumn(true, __Field.ColumnName, __Field.DataType, __Field.DBField.Length, __Field.DBField.Nullable, __Field.DBField.DefaultValue, __Field.DBField.Identity, __Field.DBField.IdentityStart, __Field.DBField.IdentityIncrement, __Field.DBField.DecimalBigger, __Field.DBField.DecimalLower);
                                }
                                else
                                {
                                    __Table.ModifyColumn(false, __Field.ColumnName, __Field.DataType, __Field.DBField.Length, __Field.DBField.Nullable, __Field.DBField.DefaultValue, __Field.DBField.Identity, __Field.DBField.IdentityStart, __Field.DBField.IdentityIncrement, __Field.DBField.DecimalBigger, __Field.DBField.DecimalLower);
                                }
                            }
                        }
                    }

                    foreach (cEntityColumn __Field in __DiffTable.EntityTable.EntityFieldList)
                    {
                        cColumn __Column = __Table.GetColumnByName(__Field.ColumnName);
                        if (__Column == null)
                        {
                            __Table.AddColumn(__Field.ColumnName, __Field.DataType, __Field.DBField.Length, __Field.DBField.Nullable, __Field.DBField.DefaultValue, false, 1, 1, __Field.DBField.DecimalBigger, __Field.DBField.DecimalLower);
                        }
                    }

                    __Table.Reload();
                    for (int i = __Table.ColumnList.Count - 1; i > -1;i-- )
                    {
                        List<cEntityColumn> __EntityColumn = __DiffTable.EntityTable.EntityFieldList.Where(__Item => ((__Item.PropertyType.IsPrimitiveWithString() || __Item.PropertyType.IsAssignableFrom(typeof(DateTime))) && __Item.ColumnName == __Table.ColumnList[i].ColumnEnitity.ColumnName) || (DifferenceManager.Database.GetEntityType().IsAssignableFrom(__Item.PropertyType) && __Item.ColumnName == __Table.ColumnList[i].ColumnEnitity.ColumnName)).ToList();
                        if (__EntityColumn.Count < 1)
                        {
                            __Table.ColumnList[i].Drop();
                        }
                    }
                }
            }

            DifferenceManager.Database.MetadataManager.TableManager.Reload();

            foreach (cEntityTable __EntityTable in DifferenceManager.Database.EntityManager.EntityTableList)
            {
                cTable __DBTable = DifferenceManager.Database.MetadataManager.TableManager.GetTableByName(__EntityTable.TableName);
                if (__DBTable != null)
                {
                    List<cEntityColumn> __PrimaryList = __EntityTable.EntityFieldList.Where(__Item => __Item.DBField.PrimaryKey).OrderBy(__Item => (__Item.DBField.KeyOrderNo)).ToList();
                    List<cColumn> __ColumnList = new List<cColumn>();
                    foreach (cEntityColumn __Item in __PrimaryList)
                    {
                        __ColumnList.Add(__DBTable.GetColumnByName(__Item.ColumnName));
                    }
                    if (__PrimaryList.Count > 0) __DBTable.AddPrimaryKey(__ColumnList);

                    List<cEntityColumn> __UniqueList = __EntityTable.EntityFieldList.Where(__Item => __Item.DBField.UniqueKey && !__Item.DBField.Clustered && !__Item.DBField.PrimaryKey).OrderBy(__Item => (__Item.DBField.KeyOrderNo)).ToList();
                    __ColumnList = new List<cColumn>();
                    foreach (cEntityColumn __Item in __UniqueList)
                    {
                        __ColumnList.Add(__DBTable.GetColumnByName(__Item.ColumnName));
                    }
                    if (__UniqueList.Count > 0) __DBTable.AddUniqueKey(__ColumnList, false);

                    List<cEntityColumn> __ClusteredUniqueList = __EntityTable.EntityFieldList.Where(__Item => __Item.DBField.UniqueKey && __Item.DBField.Clustered && !__Item.DBField.PrimaryKey).OrderBy(__Item => (__Item.DBField.KeyOrderNo)).ToList();
                    __ColumnList = new List<cColumn>();
                    foreach (cEntityColumn __Item in __ClusteredUniqueList)
                    {
                        __ColumnList.Add(__DBTable.GetColumnByName(__Item.ColumnName));
                    }
                    if (__PrimaryList.Count < 1 && __ClusteredUniqueList.Count > 0) __DBTable.AddUniqueKey(__ColumnList, true);
                }
            }

            foreach (cEntityTable __EntityTable in DifferenceManager.Database.EntityManager.EntityTableList)
            {
                cTable __DBTable = DifferenceManager.Database.MetadataManager.TableManager.GetTableByName(__EntityTable.TableName);
                if (__DBTable != null)
                {
                    List<cEntityColumn> __ForeignKeyList = __EntityTable.EntityFieldList.Where(__Item => __Item.DBField.ToForeingKey != null).ToList();
                    foreach (cEntityColumn __Item in __ForeignKeyList)
                    {
                        cColumn __ReferencedColumn = DifferenceManager.Database.MetadataManager.TableManager.GetTableByName(__Item.ForeignKey_Reference_TableName).GetColumnByName(__Item.ForeignKey_Reference_ColumnName);
                        __DBTable.AddForeignKey(__DBTable.GetColumnByName(__Item.ColumnName), __ReferencedColumn);
                    }
                }
            }

            DifferenceManager.Database.MetadataManager.TableManager.Reload();
        }
        
/*        public void SynchronizeColumn()
        {
            foreach (cDiff_Table __DiffTable in Diff_Tables)
            {
                if (__DiffTable.EntityTable != null && __DiffTable.DBTable != null)
                {
                    cTable __Table = __DiffTable.DBTable.TableManager.GetTableByName(__DiffTable.EntityTable.TableName);
                    foreach (cEntityColumn __Field in __DiffTable.EntityTable.EntityFieldList)
                    {
                        cColumn __Column = __Table.GetColumnByName(__Field.ColumnName);
                        if (__Column == null)
                        {
                            __Table.AddColumn(__Field.ColumnName, __Field.DataType, __Field.DBField.Length, __Field.DBField.Nullable, __Field.DBField.DefaultValue);
                        }
                    }
                    __Table.Reload();


                }
            }
        }


        public void DropNotHaved()
        {
            foreach (cDiff_Table __DiffTable in Diff_Tables)
            {
                if (__DiffTable.EntityTable != null && __DiffTable.DBTable != null)
                {
                    for (int i = __DiffTable.DBTable.ParentedForeignKeyList.Count -1; i > -1;i--)
                    {
                        List<cEntityColumn> __EntityColumns = __DiffTable.EntityTable.EntityFieldList.Where(__Item => __Item.ColumnName == __DiffTable.DBTable.ParentedForeignKeyList[i].ForeignKeyEnitity.ParentColumnName && __Item.DBField.ForeignKeyEntity.Name == "c" + __DiffTable.DBTable.ParentedForeignKeyList[i].ForeignKeyEnitity.ReferencedColumnName + "Entity").ToList();
                        if (__EntityColumns.Count > 0)
                        {
                            __DiffTable.DBTable.ParentedForeignKeyList[i].Drop();
                        }
                    }
                }
            }
        }
        */

        public void Calculate()
        {
            Diff_Tables = new List<cDiff_Table>();
            foreach (cEntityTable __EntityTable in DifferenceManager.Database.EntityManager.EntityTableList)
            {
                if (__EntityTable.EntityType.Name.StartsWith("c") && __EntityTable.EntityType.Name.EndsWith("Entity"))
                {
                    cTable __DBTable = DifferenceManager.Database.MetadataManager.TableManager.GetTableByName(__EntityTable.TableName);
                    Diff_Tables.Add(new cDiff_Table(__EntityTable, __DBTable));
                }
                else
                {
                    throw new Exception(__EntityTable.EntityType.Name + " : Bir DB Entity 'c' harfiyle başlayıp 'Entity ile bitmelidir!'");
                }
            }

            foreach (cTable __DBTable in DifferenceManager.Database.MetadataManager.TableManager.TableList)
            {
                if (Diff_Tables.Where((__DiffTable) => __DiffTable.DBTable == __DBTable).ToList().Count < 1)
                {
                    Diff_Tables.Add(new cDiff_Table(null, __DBTable));
                }
            }

        }
    }
}
