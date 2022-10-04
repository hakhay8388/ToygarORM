using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.Boundary.nData;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toygar.DB.Data.nDataService.nDatabase.nDBInfo
{
    public class cDBInfoEntity : cBaseEntity
    {
        [TDBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int MainVersion { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int DBVersion { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int ExtensitionVersion { get; set; }
    }
}
