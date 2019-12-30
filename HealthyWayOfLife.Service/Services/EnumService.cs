using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HealthyWayOfLife.Model.Attributes;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Model;

namespace HealthyWayOfLife.Service.Services
{
    public class EnumService
    {
        public EnumDescription GetEnumDescription<T>(T enumeratorValue) where T : struct
        {
            Type enumeratorType = typeof(T);

            string enumeratorName = enumeratorValue.ToString();

            MemberInfo[] memberInformation = enumeratorType.GetMember(enumeratorName);

            if (memberInformation.Length > 0)
            {
                object[] enumeratorValueDescriptionAttributes = memberInformation[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (enumeratorValueDescriptionAttributes.Length > 0)
                {
                    return new EnumDescription(Convert.ToInt32(enumeratorValue), (enumeratorValueDescriptionAttributes[0] as DescriptionAttribute).Description);
                }
            }

            return new EnumDescription(Convert.ToInt32(enumeratorValue), enumeratorName);
        }

        public List<T> GetEnumList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public TAttribute GetAttribute<TAttribute, TEnum>(TEnum enumeratorValue)
        {
            Type enumeratorType = typeof(TEnum);

            string enumeratorName = enumeratorValue.ToString();

            MemberInfo[] memberInformation = enumeratorType.GetMember(enumeratorName);

            if (memberInformation.Length > 0)
            {
                object[] enumeratorValueDescriptionAttributes = memberInformation[0].GetCustomAttributes(typeof(TAttribute), false);

                if (enumeratorValueDescriptionAttributes.Length > 0)
                {
                    return (TAttribute)enumeratorValueDescriptionAttributes[0];
                }
            }

            return default(TAttribute);
        }
    }
}