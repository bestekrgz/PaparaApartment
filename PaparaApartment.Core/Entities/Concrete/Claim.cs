﻿using System;


namespace PaparaApartment.Core.Entities.Concrete
{

    public partial class Claim : IEntity
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int IuserId { get; set; }
        public DateTime Idate { get; set; }
        public int? UuserId { get; set; }
        public DateTime? Udate { get; set; }
    }
}
