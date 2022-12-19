using Microsoft.AspNetCore.Mvc;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Entity.Dtos.Message;


namespace PaparaApartment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMessageService _messageAdmin;

        public MessagesController(IMessageService messageAdmin)
        {
            _messageAdmin = messageAdmin;
        }

        [HttpGet("{id}")]
        public IActionResult GetMessageById(int id)
        {
            return Ok(_messageAdmin.GetMessageById(id));
        }

        [HttpPost("new-mass")]
        public IActionResult SendMessageToAll(MessageAddDto newMessageAdd)
        {
            return Ok(_messageAdmin.SendMessageToAll(newMessageAdd));
        }

        [HttpPost("new")]
        public IActionResult SendMessageToOne(MessageAddForOneDto newMessageAddForOne)
        {
            return Ok(_messageAdmin.SendMessageToOne(newMessageAddForOne));
        }



    }
}
