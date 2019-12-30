using System.Collections.Generic;
using HealthyWayOfLife.Model.Enums;

namespace HealthyWayOfLife.Model.Model.ControllerParam
{
    public class GetUsersParams
    {
        public int Page { get; set; }
        public int ToTake { get; set; }
        public string SearchText { get; set; }
    }
}