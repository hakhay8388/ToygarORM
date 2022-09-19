using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nEntity
{
    public class cEntityList<TBaseEntity> : IEntityList where TBaseEntity : cBaseEntity
    {
        const int PagingCount = 50;
        
        private cEntityTable OwnerTable { get; set; }
        private Type PropertyType { get; set; }
        private Type OwnerType { get; set; }
        private cEntityTable EntityTable { get; set; }

        TBaseEntity[] Entities { get; set; }
        public int Count { get; set; }
        cBaseEntity OwnerEntity { get; set; }
        IDatabase Database { get; set; }
        public cEntityList(IDatabase _Database, cBaseEntity _OwnerEntity)
        {
            Database = _Database;
            OwnerEntity = _OwnerEntity;
            PropertyType = typeof(TBaseEntity);
            OwnerType = OwnerEntity.GetType();
            OwnerTable = Database.EntityManager.GetEntityTableByEnitityType(OwnerType);
            EntityTable = Database.EntityManager.GetEntityTableByEnitityType(PropertyType);
            Count = Database.EntityManager.GetEntityCountByColumnValue(PropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, _OwnerEntity.ID);
            Entities = new TBaseEntity[Count];
        }

        public void Refresh()
        {
            Count = Database.EntityManager.GetEntityCountByColumnValue(PropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, OwnerEntity.ID);
            Entities = new TBaseEntity[Count];
        }

        public TBaseEntity this[int index] 
        { 
            get
            {
                if (Count == 0)
                {
                    Refresh();
                }
                if (index < Count)
                {
                    if (Entities[index] == null)
                    {
                        Type __PropertyType = typeof(TBaseEntity);
                        List<TBaseEntity> __List = (List<TBaseEntity>)Database.EntityManager.GetEntityByColumnValue(__PropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, OwnerEntity.ID, index + 1, index + PagingCount);
                        int __Counter = 0;
                        for (int i = index; i < (index + PagingCount); i++)
                        {
                            Entities[i] = __List[__Counter];
                            __Counter++;
                            if (__List.Count <= __Counter) break;
                        }
                    }
                    return Entities[index];
                }
                throw new Exception("index numarası Counttan büyük");
            }
        }

        public List<TBaseEntity> ToList()
        {
            Type __PropertyType = typeof(TBaseEntity);
            List<TBaseEntity> __List = (List<TBaseEntity>)Database.EntityManager.GetEntityByColumnValue(__PropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, OwnerEntity.ID);
            return __List;
        }

        public TBaseEntity CreateNew()
        {
            return Database.EntityManager.CreateNew<TBaseEntity>();
        }
    }
}
