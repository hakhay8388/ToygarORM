using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;
using System;
using System.Collections.Generic;
using System.Text;
using Toygar.Boundary.nData;

namespace Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nEntityServices.nEntities
{
    public class cDBSettingEntity : cBaseGlobalEntity
    {
        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Server { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string UserId { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Password { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string DBName { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual int MaxConnectionCount { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1000, _DefaultValue:"")]
        public virtual string EntityType { get; set; }
    }
}
