namespace HealthyWayOfLife.Model.Models.Dtos
{
    public class GetUsersParams
    {
        public int Page { get; set; }
        public int ToTake { get; set; }
        public string SearchText { get; set; }
    }
}