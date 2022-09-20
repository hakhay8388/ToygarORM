
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;

namespace App.QueryTester.nDataServices.nDataService.nOtherEntityServices.nEntities
{
    public class cTestFriendEntity : cBaseOtherTestEntity
    {
        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Name { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Surname { get; set; }
      
    }
}
