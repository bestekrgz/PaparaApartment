using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PaparaApartment.Core.DataAccess;
using PaparaApartment.Core.Entities;
using PaparaApartment.Core.Utilities.Settings;
using PaparaApartment.Data.Context;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace PaparaApartment.Data.Repository.Mongo
{
    public class MongoRepositoryBase<TEntity>:IEntityRepository<TEntity> 
        where TEntity : class, IEntity, new()
    {
        private MongoDbContext _context;
        private IMongoCollection<TEntity> _collection;
        public MongoRepositoryBase(IOptions<MongoSettings> settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<TEntity>();
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter==null? _collection.AsQueryable().ToList(): _collection.AsQueryable().Where(filter).ToList();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _collection.Find(filter).SingleOrDefault();
        } 

        public void Add(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(TEntity entity)
        {
  

        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<TEntity, bool>> filter)
        {
            _collection.FindOneAndDeleteAsync(filter);
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            if (_collection.Find(filter) != null)
            {
                return false;
            }

            return true;
        }
    }
}
