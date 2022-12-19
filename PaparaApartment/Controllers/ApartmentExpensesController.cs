using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaparaApartment.Business.Abstract;


namespace PaparaApartment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentExpensesController : ControllerBase
    {
        private IApartmentExpenseService _apartmentExpenseAdmin;

        public ApartmentExpensesController(IApartmentExpenseService apartmentExpenseAdmin)
        {
            _apartmentExpenseAdmin = apartmentExpenseAdmin;
        }


        [HttpGet("unpaid/{apartmentId}")]
        public IActionResult GetUnPaidPayments(int apartmentId)
        {
            return Ok(_apartmentExpenseAdmin.GetUnPaidPayments(apartmentId));
        }

        [HttpGet("paid/{apartmentId}")]
        public IActionResult GetPaidPayments(int apartmentId)
        {
            return Ok(_apartmentExpenseAdmin.GetPaidPayments(apartmentId));
        }

        [HttpGet("unpaid")]
        public IActionResult GetMyUnPaidPayments()
        {
            return Ok(_apartmentExpenseAdmin.GetMyUnPaidPayments());
        }

        [HttpGet("paid")]
        public IActionResult GetMyPaidPayments()
        {
            return Ok(_apartmentExpenseAdmin.GetMyPaidPayments());
        }


        [HttpPost]
        public void Post([FromBody] string value)
        {
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
