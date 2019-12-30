using System;

namespace HealthyWayOfLife.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]

    public class ValueTypeAttribute : Attribute
    {
        public Enums.ValueType ValueType { get; }

        public ValueTypeAttribute()
        {

        }
        public ValueTypeAttribute(Enums.ValueType valueType)
        {
            ValueType = valueType;
        }


    }
}