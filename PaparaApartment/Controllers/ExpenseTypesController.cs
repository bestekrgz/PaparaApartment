using PaparaApartment.Business.Abstract;
using PaparaApartment.Entities.Dtos.ExpenseType;
using Microsoft.AspNetCore.Mvc;
using PaparaApartment.Business.Concrete;


namespace PaparaApartment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypesController : ControllerBase
    {
        private IExpenseTypeService _expenseTypeAdmin;

        public ExpenseTypesController(IExpenseTypeService expenseTypeAdmin)
        {
            _expenseTypeAdmin = expenseTypeAdmin;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _expenseTypeAdmin.GetAll();

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ExpenseTypeAddDto newExpenseType)
        {
            var result = _expenseTypeAdmin.Add(newExpenseType);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ExpenseTypeUpdateDto updateExpenseType)
        {
            var result = _expenseTypeAdmin.Update(updateExpenseType);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int deleteExpenseTypeId)
        {
            var result = _expenseTypeAdmin.Delete(deleteExpenseTypeId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
