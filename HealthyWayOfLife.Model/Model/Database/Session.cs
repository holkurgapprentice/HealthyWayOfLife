using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Model.Database.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthyWayOfLife.Model.Model.Database
{
    public class Session : BaseDatabaseFieldInfo
    {
        [Required]
        public string RemoteAddress { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public SessionState SessionState { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? LastRefreshDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        [Required]
        public User User { get; set; }
        public int UserId { get; set; }
    }
}