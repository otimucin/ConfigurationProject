using System;
using System.Collections.Generic;
using System.Text;
using ConfigurationProject.Data.Model;

namespace ConfigurationProject.Data.Repository
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        public IRepository<Configuration> Repository { get; set; }

        public ConfigurationRepository(IRepository<Configuration> repository)
        {
            Repository = repository;
        }

        public ConfigurationRepository(string connectionString)
        {
            var repository = new Repository<Configuration>(connectionString);
            Repository = repository;
        }
    }
}
