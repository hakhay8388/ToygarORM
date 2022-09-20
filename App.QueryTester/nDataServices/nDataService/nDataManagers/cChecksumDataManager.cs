using Toygar.Base.Core.nCore;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase.nQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataServiceManager;
using Toygar.DB.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using App.QueryTester.nDataServices.nDataService;
using App.QueryTester.nDataServices.nDataService.nEntityServices.nEntities;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;

namespace App.QueryTester.nDataServices.nDataService.nDataManagers
{
    public class cChecksumDataManager<TBaseEntity> : cBaseDataManager<TBaseEntity>
        where TBaseEntity : cBaseEntity
    {
        public cChecksumDataManager(IDataServiceManager _DataServiceManager)
          : base(_DataServiceManager)
        {
        }

		public cDefaultDataChecksumEntity GetCheckSumByCode(string _Host,  string _CheckSumCode)
		{
			IDataService __DataService = DataServiceManager.GetDataService<TBaseEntity>(_Host);

			cDefaultDataChecksumEntity __DefaultDataChecksumEntity = __DataService.Database.Query<cDefaultDataChecksumEntity>()
					.SelectAll()
					.Where()
					.Operand(__Item => __Item.Code).Eq(_CheckSumCode)
					.ToQuery()
					.ToList()
					.FirstOrDefault();
			return __DefaultDataChecksumEntity;
		}


		public cDefaultDataChecksumEntity AddCheckSum(string _Host, string _Code, string _CheckSum)
		{
			IDataService __DataService = DataServiceManager.GetDataService<TBaseEntity>(_Host);

            cDefaultDataChecksumEntity __DefaultDataChecksumEntity = __DataService.Database.CreateNew<cDefaultDataChecksumEntity>();
			__DefaultDataChecksumEntity.Code = _Code;
			__DefaultDataChecksumEntity.CheckSum = _CheckSum;
			__DefaultDataChecksumEntity.Save();
			return __DefaultDataChecksumEntity;
		}
		public cDefaultDataChecksumEntity UpdateCheckSum(cDefaultDataChecksumEntity _DefaultDataChecksumEntity, string _Code, string _CheckSum)
		{
			_DefaultDataChecksumEntity.Code = _Code;
			_DefaultDataChecksumEntity.CheckSum = _CheckSum;
			_DefaultDataChecksumEntity.Save();
			return _DefaultDataChecksumEntity;
		}

		public void CreateCheckSumIfNotExists(string _Host, string _Code, string _CheckSum)
		{
			IDataService __DataService = DataServiceManager.GetDataService<TBaseEntity>(_Host);

            cDefaultDataChecksumEntity __DefaultDataChecksumEntity = GetCheckSumByCode(_Host, _Code);
			if (__DefaultDataChecksumEntity == null)
			{
				AddCheckSum(_Host, _Code, _CheckSum);
			}
			else
			{
				UpdateCheckSum(__DefaultDataChecksumEntity, _Code, _CheckSum);
			}
		}
	}
}
