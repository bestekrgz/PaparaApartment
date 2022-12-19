using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaparaApartment.Core.DataAccess;
using PaparaApartment.Entity.Concrete;

namespace PaparaApartment.Data.Abstract
{
    public interface IPaymentDal: IEntityRepository<Payment>
    {
    }
}
