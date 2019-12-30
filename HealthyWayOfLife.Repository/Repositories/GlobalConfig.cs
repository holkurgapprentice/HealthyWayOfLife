

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Model.Database;
using ValueType = HealthyWayOfLife.Model.Enums.ValueType;

namespace HealthyWayOfLife.Repository.Repositories
{
    public class GlobalConfig
    {
        public List<Configuration> ConfigurationList { get; set; }
  
        public int SessionTimeMinutes => GetConfigurationValue<int>(ConfigType.SessionTime);

        public T GetConfigurationValue<T>(ConfigType configType)
        {
            Configuration configuration = ConfigurationList.FirstOrDefault(x => x.ConfigType == configType);

            if (configuration == null)
            {
                return default(T);
            }

            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                switch (configuration.ValueType)
                {
                    case ValueType.Decimal:
                        return (T)converter.ConvertFrom(configuration.DecimalValue);
                    case ValueType.Bool:
                        return (T)converter.ConvertFrom(configuration.BoolValue);
                    case ValueType.Int:
                        return (T)(object)configuration.IntValue;
                    case ValueType.String:
                        return (T)converter.ConvertFrom(configuration.StringValue);
                    case ValueType.DateTime:
                        return (T)converter.ConvertFrom(configuration.DateTimeValue);
                }
            }
            catch (Exception e)
            {
                return default(T);
            }

            return default(T);
        }
    }
}