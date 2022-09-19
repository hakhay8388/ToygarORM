using System;
using System.Collections.Generic;
using System.Text;
using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;

namespace App.QueryTester.nDataServices.nDataService.nEntityServices.nEntities
{
    public class cDefaultDataChecksumEntity : cBaseQueryTesterEntity
    {
        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Code { get; set; }

		[TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 2048)]
		public virtual string CheckSum { get; set; }

	}
}
