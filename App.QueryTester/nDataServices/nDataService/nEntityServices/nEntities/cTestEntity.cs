
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toygar.Base.Boundary.nData;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;

namespace App.QueryTester.nDataServices.nDataService.nEntityServices.nEntities
{
    public class cUserTempEntity : cBaseQueryTesterEntity
    {
        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Name { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Surname { get; set; }
        
        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string RealSurname { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Email { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Password { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 2, _DefaultValue: "tr")]
        public virtual string Language { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string Telephone { get; set; }

        [TDBField(_Nullable: true, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime DateOfBirth { get; set; }


        [TDBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: false)]
        public virtual bool IsSeller { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string Token { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string GuidStr { get; set; }

        [TDBField(_Nullable: true, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string OtherUniversity { get; set; }

        [TDBField(_Nullable: true, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string OtherSection { get; set; }

        [TDBField(_Nullable: true, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string Usernick { get; set; }

        [TDBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime LastSendMail { get; set; }
        [TDBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime SendMailEndDate { get; set; }
    }
}
