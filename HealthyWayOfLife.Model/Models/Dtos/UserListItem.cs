namespace HealthyWayOfLife.Model.Models.Dtos
{
    public class UserListItem
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public string UserType { get; set; }
    }
}