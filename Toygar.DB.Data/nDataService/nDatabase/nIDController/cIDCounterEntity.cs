using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.Boundary.nData;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.Boundary.nData;

namespace Toygar.DB.Data.nDataService.nDatabase.nIDController
{
    public class cIDCounterEntity : cBaseEntity
    {
        [TDBField(_PrimaryKey: true, _KeyOrderNo: 1, _Nullable: false, _DataType: EDataType.Bigint, _DefaultValue: 0, _Identity: true, _IdentityStart: 1, _IdentityIncrement: 1)]
        public virtual new long ID { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _PrimaryKey: true, _KeyOrderNo: 2, _DefaultValue: "default")]
        public virtual string TableName { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Bigint, _DefaultValue: -1)]
        public virtual long Count { get; set; }
    }
}
