using System;
using System.Collections.Generic;
using System.Text;
using ConfigurationProject.Data.Attribute;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConfigurationProject.Data.Model
{
    [CollectionName(Name = "configuration")]
    public class Configuration
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        public string Name { get; set; }
        public DbType Type { get; set; }
        public String Value { get; set; }
        public bool IsActive { get; set; }
        public string ApplicationName { get; set; }
        [BsonIgnore]
        public string MongoId { get; set; }

        public bool CheckType<T>()
        {
            switch (Type)
            {
                case DbType.String:
                    return typeof(T) == typeof(string);
                case DbType.Bool:
                    return typeof(T) == typeof(bool);
                case DbType.Double:
                    return typeof(T) == typeof(double);
                case DbType.Int:
                    return typeof(T) == typeof(int);
                default:
                    throw new Exception($"Unsupported config type:{Type}");
            }
        }

        public T GetValue<T>()
        {
            switch (Type)
            {
                case DbType.String:
                    return (T)Convert.ChangeType(Value, typeof(T));
                case DbType.Bool:
                    return (T)Convert.ChangeType(bool.Parse(Value), typeof(T));
                case DbType.Double:
                    return (T)Convert.ChangeType(double.Parse(Value), typeof(T));
                case DbType.Int:
                    return (T)Convert.ChangeType(int.Parse(Value), typeof(T));
                default:
                    throw new Exception($"Unsupported config type:{Type}");
            }
        }
    }
}
