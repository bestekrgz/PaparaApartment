using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Business.Constant;
using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Entity.Dtos.User;
using AutoMapper;


namespace PaparaApartment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authAdmin;
        private IMapper _mapper;

        public AuthController(IAuthService authAdmin, IMapper mapper)
        {
            _authAdmin = authAdmin;
            _mapper = mapper;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserForLoginDto userForLogin)
        {
            var loginUser = _authAdmin.Login(userForLogin);

            if (!loginUser.Success)
            {
                return BadRequest(loginUser.Message);
            }

            var token = _authAdmin.CreateAccessToken(_mapper.Map<User>(loginUser.Data));

            return Ok(token);
        }
    }
}
