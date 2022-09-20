using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;
using System;
using System.Collections.Generic;
using System.Text;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;

namespace Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nEntityServices.nEntities
{
    public class cProfileEntity : cBaseGlobalEntity
    {
        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string HostName { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual cEntityList<cDBSettingEntity> DBSetting { get; set; }

    }
}
