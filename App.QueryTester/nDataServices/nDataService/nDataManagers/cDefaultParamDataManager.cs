using Bootstrapper.Core.nCore;
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
    public class cDefaultParamDataManager<TBaseEntity> : cBaseDataManager<TBaseEntity>
        where TBaseEntity : cBaseEntity
    {
        public cDefaultParamDataManager(IDataServiceManager _DataServiceManager)
          : base(_DataServiceManager)
        {
        }

		public cDefaultParamEntity GetParamEntityByCode(IDataService _DataService,  string _Code)
		{

			cDefaultParamEntity __DefaultParamEntity = _DataService.Database.Query<cDefaultParamEntity>()
					.SelectAll()
					.Where()
					.Operand(__Item => __Item.Code).Eq(_Code)
					.ToQuery()
					.ToList()
					.FirstOrDefault();
			return __DefaultParamEntity;
		}


		public cDefaultParamEntity AddParam(IDataService _DataService, string _Code, string _Value)
		{
            cDefaultParamEntity __DefaultDataChecksumEntity = _DataService.Database.CreateNew<cDefaultParamEntity>();
			__DefaultDataChecksumEntity.Code = _Code;
			__DefaultDataChecksumEntity.Value = _Value;
			__DefaultDataChecksumEntity.Save();
			return __DefaultDataChecksumEntity;
		}
		public cDefaultParamEntity UpdateParamValue(cDefaultParamEntity _DefaultDataChecksumEntity, string _Code, string _Value)
		{
			_DefaultDataChecksumEntity.Code = _Code;
			_DefaultDataChecksumEntity.Value = _Value;
			_DefaultDataChecksumEntity.Save();
			return _DefaultDataChecksumEntity;
		}

		public void CreateParamIfNotExists(IDataService _DataService, string _Code, string _Value)
		{
            cDefaultParamEntity __DefaultDataChecksumEntity = GetParamEntityByCode(_DataService, _Code);
			if (__DefaultDataChecksumEntity == null)
			{
				AddParam(_DataService, _Code, _Value);
			}
			else
			{
				UpdateParamValue(__DefaultDataChecksumEntity, _Code, _Value);
			}
		}
	}
}
