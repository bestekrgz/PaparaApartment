using System;
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.UserMessage
{
    public class UserMessageSentViewDto:IDto
    {
        public int Id { get; set; }
        public string ToUserName { get; set; }
        public int MessageId { get; set; }
        public string MessageSubject { get; set; }
        public bool IsNew { get; set; }
        public bool IsActive { get; set; }
        public DateTime MessageDate { get; set; }
        public string ToUserClaim { get; set; }
        public string ToUserBlock { get; set; }
        public short ToUserDoorNumber { get; set; }
        public int ToUserId { get; set; }
    }
}
