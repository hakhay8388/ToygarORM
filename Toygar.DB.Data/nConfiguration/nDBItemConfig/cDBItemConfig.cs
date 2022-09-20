using Toygar.Base.Boundary.nData;

namespace Toygar.DB.Data.nConfiguration.nDBItemConfig
{
    public class cDBItemConfig
    {
        public string HostName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string DBName { get; set; }
        public int MaxConnectCount { get; set; }
        public EDBVendor DBVendor { get; set; }
        public string EntityType { get; set; }
    }
}
