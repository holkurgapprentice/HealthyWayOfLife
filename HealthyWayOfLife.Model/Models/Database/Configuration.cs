using System;
using System.ComponentModel.DataAnnotations.Schema;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Models.Database.Base;
using ValueType = HealthyWayOfLife.Model.Enums.ValueType;

namespace HealthyWayOfLife.Model.Models.Database
{
    public class Configuration : BaseDatabaseFieldInfo
    {
        public ConfigType ConfigType { get; set; }

        public string ConfigTypeName
        {
            get => ConfigType.ToString();
            private set { }
        }
        public ValueType ValueType { get; set; }
        public int? IntValue { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal? DecimalValue { get; set; }
        public string StringValue { get; set; }
        public bool? BoolValue { get; set; }
        public DateTime? DateTimeValue { get; set; }

        [NotMapped]
        public string ConfigName { get; set; }

        public int? OrderNumber { get; set; }


        public object GetValue()
        {
            switch (ValueType)
            {
                case ValueType.Decimal:
                    return DecimalValue;
                case ValueType.Bool:
                    return BoolValue;
                case ValueType.Int:
                    return IntValue;
                case ValueType.String:
                    return StringValue;
                case ValueType.DateTime:
                    return DateTimeValue;
            }
            return null;
        }

        public void SetValue(object obj)
        {
            switch (ValueType)
            {
                case ValueType.Decimal:
                    DecimalValue = Convert.ToDecimal(obj);
                    break;
                case ValueType.Bool:
                    BoolValue = Convert.ToBoolean(obj);
                    break;
                case ValueType.Int:
                    IntValue = Convert.ToInt32(obj);
                    break;
                case ValueType.String:
                    StringValue = Convert.ToString(obj);
                    break;
                case ValueType.DateTime:
                    DateTimeValue = Convert.ToDateTime(obj);
                    break;
            }
        }
    }
}