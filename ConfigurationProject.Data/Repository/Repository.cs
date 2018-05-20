using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ConfigurationProject.Data.Helper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConfigurationProject.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public IMongoCollection<T> Collection => _collection;

        public DateTime Now => DateTime.UtcNow;

        public Repository(string connectionString)
        {
            _collection = DatabaseHelper.GetCollectionFromConnectionString<T>(connectionString);
        }

        public T GetById(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);

            return _collection.Find(filter).FirstOrDefault();
        }

        public T GetSingle(Expression<Func<T, bool>> criteria)
        {
            return _collection.AsQueryable().Where(criteria).FirstOrDefault();
        }

        public IQueryable<T> All(Expression<Func<T, bool>> criteria)
        {
            return _collection.AsQueryable().Where(criteria);
        }

        public IQueryable<T> All()
        {
            return _collection.AsQueryable();
        }

        public T Add(T entity)
        {
            _collection.InsertOne(entity);

            return entity;
        }

        public void Add(IEnumerable<T> entities)
        {
            _collection.InsertMany(entities);
        }

        public T Update(T entity, ObjectId id)
        {
            return Update(entity, "_id", id);
        }

        public T Update(T entity, string key, object value, bool upsert = false)
        {
            var filter = Builders<T>.Filter.Eq(key, value);
            _collection.ReplaceOne(filter, entity, new UpdateOptions { IsUpsert = upsert });

            return entity;
        }

        public void Delete(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            _collection.DeleteOne(filter);
        }

        public bool Exists(Expression<Func<T, bool>> criteria)
        {
            return _collection.AsQueryable().Any(criteria);
        }
    }

}
