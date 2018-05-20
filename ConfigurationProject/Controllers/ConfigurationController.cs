using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationProject.Data.Model;
using ConfigurationProject.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace ConfigurationProject.Controllers
{
    public class ConfigurationController
    {
        private readonly IConfigurationRepository _repo;
        private readonly ILogger<ConfigurationController> _logger;
        public ConfigurationController(IConfigurationRepository repository, ILogger<ConfigurationController> logger)
        {
            _repo = repository;
            _logger = logger;
        }

        [HttpGet]
        public List<Configuration> GetAllList()
        {
            try
            {
                return _repo.Repository.All().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata oluştu !.");
                return null;
            }
        }

        [HttpGet]
        public Configuration GetApplicationConfigValues(string appName)
        {
            var value = _repo.Repository.GetSingle(m => m.ApplicationName == appName);
            return value;
        }

        [HttpPut]
        public void UpdateValue(string id, [FromBody]Configuration value)
        {
            var objectId = new ObjectId(id);
            _repo.Repository.Update(value, objectId);
        }

        [HttpPost]
        public void AddNewValue([FromBody]Configuration value)
        {
            _repo.Repository.Add(value);
        }

        [HttpDelete]
        public void DeleteValue(string id)
        {
            var objectId = new ObjectId(id);
            _repo.Repository.Delete(objectId);
        }
    }

}
