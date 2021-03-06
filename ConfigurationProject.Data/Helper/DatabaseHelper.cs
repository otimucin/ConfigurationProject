﻿using System;
using System.Collections.Generic;
using System.Text;
using ConfigurationProject.Data.Attribute;
using ConfigurationProject.Data.Model;
using MongoDB.Driver;

namespace ConfigurationProject.Data.Helper
{
    public static class DatabaseHelper
    {
        public static IMongoDatabase DatabaseConnection(string connectionstring)
        {
            var client = new MongoClient(connectionstring);
            var cnn = new MongoUrl(connectionstring);
            var database = client.GetDatabase(cnn.DatabaseName);

            return database;
        }

        public static IMongoCollection<T> GetCollectionFromConnectionString<T>(string connectionstring) where T : class
        {
            var collectionName = typeof(T).BaseType == typeof(object) ? CollectioNameInterface<T>() : CollectionNameType(typeof(T));

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }

            return DatabaseConnection(connectionstring).GetCollection<T>(collectionName);
        }

        private static string CollectioNameInterface<T>()
        {
            string collectionname;

            var att = System.Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));

            if (att != null)
            {
                collectionname = ((CollectionName)att).Name;
            }
            else
            {
                collectionname = typeof(T).Name;
            }

            return collectionname;
        }

        private static string CollectionNameType(Type entitytype)
        {
            string collectionname;

            var att = System.Attribute.GetCustomAttribute(entitytype, typeof(CollectionName));

            if (att != null)
            {
                collectionname = ((CollectionName)att).Name;
            }
            else
            {

                entitytype = entitytype.BaseType;

                return entitytype?.Name;
            }

            return collectionname;
        }

        public static void CreateDatabase(string connectionStr)
        {
            IMongoDatabase db = DatabaseConnection(connectionStr);

            IMongoCollection<Configuration> configs = db.GetCollection<Configuration>("configuration");

            if (configs.Count(x => true) != 0) //Eğer collection boş ise db'yi boş sayıp indexleri yaratıyoruz.
                return;

            configs.Indexes.CreateOneAsync(Builders<Configuration>.IndexKeys.Ascending(nameof(Configuration.Name)).Ascending(nameof(Configuration.ApplicationName)));
            configs.Indexes.CreateOneAsync(Builders<Configuration>.IndexKeys.Ascending(nameof(Configuration.ApplicationName)));

            var setting1 = new Configuration
            {
                ApplicationName = "SERVICE-A",
                Name = "SiteName",
                Type = DbType.String,
                Value = "Boyner.com.tr",
                IsActive = true
            };
            var setting2 = new Configuration
            {
                ApplicationName = "SERVICE-B",
                Name = "IsBasketEnabled",
                Type = DbType.Bool,
                Value = "1",
                IsActive = true
            };
            var setting3 = new Configuration
            {
                ApplicationName = "SERVISE-A",
                Name = "MaxItemCount",
                Type = DbType.Int,
                Value = "50",
                IsActive = false
            };
           
            configs.InsertOne(setting1);
            configs.InsertOne(setting2);
            configs.InsertOne(setting3);
        }
    }

}
