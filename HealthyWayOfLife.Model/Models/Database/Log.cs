using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Models.Database.Base;

namespace HealthyWayOfLife.Model.Models.Database
{
    public class Log : BaseDatabaseFieldInfo
    {
        public string LogText { get; set; }
        public string Exception { get; set; }
        public string InnerException { get; set; }
        public string Stack { get; set; }
        public LogType LogType { get; set; }
    }
}