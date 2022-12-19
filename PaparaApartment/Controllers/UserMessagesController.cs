using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Entity.Dtos.UserMessage;


namespace PaparaApartment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessagesController : ControllerBase
    {
        private IUserMessageService _userMessageAdmin;

        public UserMessagesController(IUserMessageService userMessageAdmin)
        {
            _userMessageAdmin = userMessageAdmin;
        }

 
        [HttpGet("inbox")]
        public IActionResult GetIncomingMessages()
        {
            return Ok(_userMessageAdmin.GetUserIncomingMessages());
        }

        [HttpGet("sent")]
        public IActionResult GetSentMessages()
        {
            return Ok(_userMessageAdmin.GetUserSentMessages());
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPut("{id:int}&status={status:bool}")]
        public IActionResult UpdateReadStatus(int id,bool status)
        {
            return Ok(_userMessageAdmin.UpdateReadStatus(id,status));
        }


        [HttpDelete("{id}&isSender={isSender:bool}")]
        public IActionResult Delete(int id,bool isSender)
        {
            var result = _userMessageAdmin.Delete(id, isSender);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
