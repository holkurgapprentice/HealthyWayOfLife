using System.ComponentModel.DataAnnotations;
using HealthyWayOfLife.Model.Models.Database.Base;

namespace HealthyWayOfLife.Model.Models.Database
{
    public class User : BaseDatabaseFieldInfo
    {
        public string Login { get; set; }
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}