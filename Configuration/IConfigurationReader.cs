using System;

namespace Configuration
{
    public interface IConfigurationReader
    {
        T GetValue<T>(string key);
    }
}
