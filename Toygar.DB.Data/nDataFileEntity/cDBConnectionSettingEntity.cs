
using Toygar.Base.Boundary.nData;

namespace Toygar.DB.Data.nDataFileEntity
{
    public class cDBConnectionSettingEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public int MaxConnectCount { get; set; }
        public EDBVendor DBVendor { get; set; }
        public string GlobalDBName { get; set; }
        public int DBVersion { get; set; }
        public string EntityType { get; set; }
    }
}
