using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Utils
{
    public abstract class ConfigurationProvider
    {
        protected string GetStringValue(string name, string defaultValue)
        {
            try
            {
                var stringValue = Environment.GetEnvironmentVariable(name);
                stringValue = string.IsNullOrEmpty(stringValue) ? ConfigurationManager.AppSettings[name] : stringValue;
                return string.IsNullOrEmpty(stringValue) ? defaultValue : stringValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        protected bool GetBoolValue(string name, bool defaultValue)
        {
            bool parsedValue;
            var isSuccess = bool.TryParse(Environment.GetEnvironmentVariable(name), out parsedValue);
            if (!isSuccess) isSuccess = bool.TryParse(ConfigurationManager.AppSettings[name], out parsedValue);
            return isSuccess ? parsedValue : defaultValue;
        }

        protected int GetIntValue(string name, int defaultValue)
        {
            int parsedValue;
            var isSuccess = int.TryParse(Environment.GetEnvironmentVariable(name), out parsedValue);
            if (!isSuccess) isSuccess = int.TryParse(ConfigurationManager.AppSettings[name], out parsedValue);
            return isSuccess ? parsedValue : defaultValue;
        }

        protected long GetLongValue(string name, long defaultValue)
        {
            long parsedValue;
            var isSuccess = long.TryParse(Environment.GetEnvironmentVariable(name), out parsedValue);
            if (!isSuccess) isSuccess = long.TryParse(ConfigurationManager.AppSettings[name], out parsedValue);
            return isSuccess ? parsedValue : defaultValue;
        }
    }
}
