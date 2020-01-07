using System.Collections.Generic;

namespace HealthyWayOfLife.Model.Models.Dtos
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public List<int> PermissionsList { get; set; }
    }
}