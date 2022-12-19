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
    public class ApartmentsController : ControllerBase
    {
        private IApartmentService _apartmentAdmin;

        public ApartmentsController(IApartmentService apartmentAdmin)
        {
            _apartmentAdmin = apartmentAdmin;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_apartmentAdmin.GetAll());
        }

        [HttpGet("residents")]
        public IActionResult GetResidents()
        {
            return Ok(_apartmentAdmin.GetAllResident());
        }

        // GET api/<ApartmetsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ApartmetsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApartmetsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApartmetsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
