
using System.Linq;
using PaparaApartment.Core.Data.EntitiyFramework;
using PaparaApartment.Data.Context;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Data.Abstract;

namespace ApartmentManagement.DataAccess.Concrete.EntityFramework
{
    public class EfMessageDal : EfEntityRepositoryBase<Message, PaparaApartmentDbContext>, IMessageDal
    {
        public int GetLastMessageId()
        {
            using (var context=new PaparaApartmentDbContext())
            {
                var id = context.Set<Message>().ToList().Last().Id;
                return id;
            }
        }
    }
}
