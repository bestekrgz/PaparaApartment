
using PaparaApartment.Core.Entities;
using System;

namespace PaparaApartment.Entity.Dtos.UserMessage
{
    public class UserMessageIncomingViewDto:IDto
    {
        public int Id { get; set; }
        public string FromUserName { get; set; }
        public int MessageId { get; set; }
        public string MessageSubject { get; set; }
        public bool IsNew { get; set; }
        public bool IsRead { get; set; }
        public bool IsActive { get; set; }
        public DateTime MessageDate { get; set; }
        public int FromUserId { get; set; }
        public string FromUserClaim { get; set; }
        public string FromUserBlock { get; set; }
        public int FromUserDoorNumber { get; set; }
    }
}
