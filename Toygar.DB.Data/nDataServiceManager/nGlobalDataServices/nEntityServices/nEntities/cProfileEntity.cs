using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nEntityServices.nEntities
{
    public class cProfileEntity : cBaseGlobalEntity
    {
        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string HostName { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual cDBSettingEntity DBSetting { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "Now")]
        public virtual DateTime EndDate { get; set; }




    }
}
