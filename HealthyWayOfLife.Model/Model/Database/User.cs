using System.ComponentModel.DataAnnotations;
using HealthyWayOfLife.Model.Model.Database.Base;

namespace HealthyWayOfLife.Model.Model.Database
{
    public class User : BaseDatabaseFieldInfo
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}