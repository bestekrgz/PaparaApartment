
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaparaApartment.Core.Utilities.Settings;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Data.Context;
using PaparaApartment.Data.Repository.Mongo;
using PaparaApartment.Entity.Concrete;

namespace ApartmentManagement.DataAccess.Concrete.Mongo
{
    public class MPaymentDal: MongoRepositoryBase<Payment>, IPaymentDal
    {
        private MongoDbContext _context;
        private IMongoCollection<Payment> _collection;
        public MPaymentDal(IOptions<MongoSettings> settings) : base(settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<Payment>();
        }
    }
}
