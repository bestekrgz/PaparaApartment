using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Entity.Dtos.UserDetail;

namespace PaparaApartment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private IUserDetailService _userDetailAdmin;

        public UserDetailsController(IUserDetailService userDetailAdmin)
        {
            _userDetailAdmin = userDetailAdmin;
        }
        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _userDetailAdmin.GetById(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }


        [HttpPost]
        public IActionResult Add([FromBody] UserDetailAddDto userDetailAddDto)
        {
            var result = _userDetailAdmin.Add(userDetailAddDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }


        [HttpPut]
        public IActionResult Update([FromBody] UserDetailUpdateDto userDetailUpdateDto)
        {
            var result = _userDetailAdmin.Update(userDetailUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _userDetailAdmin.Delete(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
