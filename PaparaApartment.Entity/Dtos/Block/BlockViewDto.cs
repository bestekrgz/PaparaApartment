using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.Block
{
    public class BlockViewDto:IDto
    {
        public short Id { get; set; }
        public string Letter { get; set; }
    }
}
