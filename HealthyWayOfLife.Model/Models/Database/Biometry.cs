using System;
using System.ComponentModel.DataAnnotations.Schema;
using HealthyWayOfLife.Model.Models.Database.Base;

namespace HealthyWayOfLife.Model.Models.Database
{
    public class Biometry : BaseDatabaseFieldInfo
    {
        public User User { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal WeightInKgs { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal HeightInCm { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal ChestInCm { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal ArmInCm { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal LegInCm { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal WaistInCm { get; set; }
        public DateTime DateFor { get; set; }
    }
}
