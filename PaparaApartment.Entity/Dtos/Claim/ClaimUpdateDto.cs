﻿
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.Claim
{
    public class ClaimUpdateDto:IDto
    {
        public short Id { get; set; }
        public string Name { get; set; }
    }
}
