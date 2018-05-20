using System;
using System.Collections.Generic;
using System.Text;
using ConfigurationProject.Data.Model;

namespace ConfigurationProject.Data.Repository
{
    public interface IConfigurationRepository
    {
        IRepository<Configuration> Repository { get; }
    }
}
