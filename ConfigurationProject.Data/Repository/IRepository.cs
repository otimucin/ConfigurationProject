using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConfigurationProject.Data.Repository
{
    public interface IRepository<T>
    {
        IMongoCollection<T> Collection { get; }
        DateTime Now { get; }
        T GetById(ObjectId id);
        T GetSingle(Expression<Func<T, bool>> criteria);
        IQueryable<T> All();
        IQueryable<T> All(Expression<Func<T, bool>> criteria);
        T Add(T entity);
        void Add(IEnumerable<T> entities);
        T Update(T entity, ObjectId id);
        T Update(T entity, string key, object value, bool upsert);
        void Delete(ObjectId id);
        bool Exists(Expression<Func<T, bool>> criteria);
    }

}
