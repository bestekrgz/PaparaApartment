using Microsoft.AspNetCore.Mvc;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Entity.Concrete;

namespace PaparaApartment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private IPaymentService _paymentAdmin;

        public PaymentsController(IPaymentService paymentAdmin)
        {
            _paymentAdmin = paymentAdmin;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _paymentAdmin.GetAll();
            return Ok(result);
        }

        [HttpGet("filter")]
        public IActionResult GetOneApartmentPayments(int apartmentId)
        {
            var result = _paymentAdmin.GetByApartmentId(apartmentId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost]
        public IActionResult Post([FromBody] Payment payment)
        {
            var result = _paymentAdmin.Add(payment);
            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
