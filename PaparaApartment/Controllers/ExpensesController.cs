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
    public class ExpensesController : ControllerBase
    {
        private IExpenseService _expenseAdmin;

        public ExpensesController(IExpenseService expenseAdmin)
        {
            _expenseAdmin = expenseAdmin;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_expenseAdmin.GetList());
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
