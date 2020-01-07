namespace HealthyWayOfLife.Model.Models
{
    public class EnumDescription
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public EnumDescription(int? enumeratorId, string enumeratorName)
        {
            Id = enumeratorId;
            Name = enumeratorName;
        }
    }
}