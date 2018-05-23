using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationProject.Data.Model;
using ConfigurationProject.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        [HttpPost]
        public Configuration GetValueById([FromBody] JObject result)
        {
            var item = result.ToObject<MongoId>();
            var value = _repo.Repository.GetById(new ObjectId(item.id));
            return value;
        }

        [HttpGet]
        public Configuration GetApplicationConfigValues(string appName)
        {
            var value = _repo.Repository.GetSingle(m => m.ApplicationName == appName);
            return value;
        }

        [HttpPut]
        public bool UpdateValue([FromBody] JObject result)
        {
            try
            {
                Configuration data = result.ToObject<Configuration>();
                var objId = new ObjectId(data.MongoId);
                var newItem = new Configuration
                {
                    ApplicationName = data.ApplicationName,
                    IsActive = data.IsActive,
                    Name = data.Name,
                    Type = data.Type,
                    Value = data.Value
                };
                _repo.Repository.Update(newItem, objId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Güncelleme işleminde hata alındı");
                return false;
            }
    
        }

        [HttpPost]
        public bool AddNewValue([FromBody] JObject result)
        {
            try
            {
                Configuration data = result.ToObject<Configuration>();
                _repo.Repository.Add(data);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Kayıt ekleme işleminde hata oluştu");
                return false;
            }
        }

        [HttpDelete]
        public void DeleteValue(string id)
        {
            var objectId = new ObjectId(id);
            _repo.Repository.Delete(objectId);
        }
    }

    public class MongoId
    {
        public string id { get; set; }
    }

}
