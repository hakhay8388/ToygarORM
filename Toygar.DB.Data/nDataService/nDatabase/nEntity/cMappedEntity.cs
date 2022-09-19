using Toygar.DB.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nEntity
{
    public class cMappedEntity<TMapEntity, TMappedToEntity> : IMappedEntity
        where TMapEntity : cBaseEntity
        where TMappedToEntity : cBaseEntity
    {
        const int PagingCount = 50;

        private cEntityTable OwnerTable { get; set; }
        private Type OwnerType { get; set; }
        private Type MapPropertyType { get; set; }

        private Type MappedPropertyType { get; set; }
        private cEntityTable MapEntityTable { get; set; }
        private cEntityTable MappedToEntityTable { get; set; }

        TMapEntity MapEntity { get; set; }
        TMappedToEntity MappedToEntity { get; set; }
        cBaseEntity OwnerEntity { get; set; }
        IDatabase Database { get; set; }

        TMappedToEntity[] Entities { get; set; }

        public int Count { get; set; }

        public cMappedEntity(IDatabase _Database, cBaseEntity _OwnerEntity)
        {
            Database = _Database;
            OwnerEntity = _OwnerEntity;
            MapPropertyType = typeof(TMapEntity);
            MappedPropertyType = typeof(TMappedToEntity);

            OwnerType = OwnerEntity.GetType();
            OwnerTable = Database.EntityManager.GetEntityTableByEnitityType(OwnerType);

            MapEntityTable = Database.EntityManager.GetEntityTableByEnitityType(MapPropertyType);
            MappedToEntityTable = Database.EntityManager.GetEntityTableByEnitityType(MappedPropertyType);

            Refresh();
        }

        public void Refresh()
        {
            Count = Database.EntityManager.GetEntityCountByColumnValueForMapped(MapPropertyType, MappedPropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, OwnerEntity.ID);
            Entities = new TMappedToEntity[Count];
        }

        public TMappedToEntity this[int index]
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
                        Type __MapPropertyType = typeof(TMapEntity);
                        Type __MappedPropertyType = typeof(TMappedToEntity);
                        List<TMappedToEntity> __List = (List<TMappedToEntity>)Database.EntityManager.GetEntityByColumnValue(__MapPropertyType, __MappedPropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, OwnerEntity.ID, index + 1, index + PagingCount);
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

        public List<TMappedToEntity> ToList()
        {
            Type __MapPropertyType = typeof(TMapEntity);
            Type __MappedPropertyType = typeof(TMappedToEntity);
            List<TMappedToEntity> __List = (List<TMappedToEntity>)Database.EntityManager.GetEntityByColumnValue(__MapPropertyType, __MappedPropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, OwnerEntity.ID);
            return __List;
        }

		public List<dynamic> ToDynamicObjectList(Action<dynamic> _Action = null)
		{
			Type __MapPropertyType = typeof(TMapEntity);
			Type __MappedPropertyType = typeof(TMappedToEntity);
			List<dynamic> __List = Database.EntityManager.GetEntityByColumnValueForMappedToDynamicObjectList(_Action, __MapPropertyType, __MappedPropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, OwnerEntity.ID);
			return __List;
		}


		public System.Collections.IList ToIList()
        {
            Type __MapPropertyType = typeof(TMapEntity);
            Type __MappedPropertyType = typeof(TMappedToEntity);
            List<TMappedToEntity> __List = (List<TMappedToEntity>)Database.EntityManager.GetEntityByColumnValue(__MapPropertyType, __MappedPropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, OwnerEntity.ID);
            return __List;
        }

        public JArray ToJArray()
        {
            Type __MapPropertyType = typeof(TMapEntity);
            Type __MappedPropertyType = typeof(TMappedToEntity);
            List<TMappedToEntity> __List = (List<TMappedToEntity>)Database.EntityManager.GetEntityByColumnValue(__MapPropertyType, __MappedPropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, OwnerEntity.ID);

            JArray __Result = new JArray();
            List<string> __RemoveNameList = new List<string>();

            List<PropertyInfo> __PropertList = typeof(TMappedToEntity).GetProperties().ToList();
            foreach (PropertyInfo __PropertyInfo in __PropertList)
            {
                bool? __IsVirtual = __PropertyInfo.IsVirtual();
                if (!(__IsVirtual != null && __IsVirtual.Value))
                {
                    try
                    {
                        __RemoveNameList.Add(__PropertyInfo.Name);
                    }
                    catch (Exception _Ex)
                    {
						Database.App.Loggers.SqlLogger.LogError(_Ex);
						// şimdilik buraya düşen varmı diye kontrol için konuldu
						// daha sonra kaldırılacak
						throw _Ex;
                    }
                }
            }

            for (int i = 0; i < __List.Count; i++)
            {
                JObject __JObject = JObject.FromObject(__List[i]);

                foreach (string __PropertyName in __RemoveNameList)
                {
                    try
                    {
                        __JObject.Remove(__PropertyName);
                    }
                    catch (Exception _Ex)
                    {
						Database.App.Loggers.SqlLogger.LogError(_Ex);
						// şimdilik buraya düşen varmı diye kontrol için konuldu
						// daha sonra kaldırılacak
						throw _Ex;
                    }

                }

                __Result.Add(__JObject);
            }


            return __Result;
        }

        public TMappedToEntity GetValue()
        {
			int __Count = Count;
            if (__Count == 0)
            {
				Refresh();
				__Count = Count;
				if (__Count == 0)
				{
					return null;
				}				
            }
            
			if (__Count == 1)
            {
                return this[0];
            }
            throw new Exception("birden fazla sonuç var");

        }

        public void SetValue(TMappedToEntity _Value)
        {
            Count = 0;
            Type __MapPropertyType = typeof(TMapEntity);
            Database.EntityManager.DeleteEntityByColumnValue(__MapPropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, OwnerEntity.ID);

            TMapEntity __UserRoleMapEntity = Database.CreateNew<TMapEntity>();
            __UserRoleMapEntity.Save(OwnerEntity, _Value);
		}

        public void Delete(TMappedToEntity _Value)
        {
            Count = 0;
            Type __MapPropertyType = typeof(TMapEntity);
            Database.EntityManager.DeleteEntityByDoubleColumnValue(__MapPropertyType, OwnerTable.TableForeing_ColumnName_For_InOtherTable, OwnerEntity.ID, MappedToEntityTable.TableForeing_ColumnName_For_InOtherTable, _Value.ID);
        }

        public void AddValue(TMappedToEntity _Value)
        {
            Count = 0;

            TMapEntity __UserRoleMapEntity = Database.CreateNew<TMapEntity>();
            __UserRoleMapEntity.Save(OwnerEntity, _Value);
        }

        public TMappedToEntity CreateNew()
        {
            return Database.EntityManager.CreateNew<TMappedToEntity>();
        }
    }
}
