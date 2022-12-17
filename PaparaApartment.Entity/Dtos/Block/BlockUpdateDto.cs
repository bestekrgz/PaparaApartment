using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.Block
{
    public class BlockUpdateDto:IDto
    {
        public short Id { get; set; }
        public string Letter { get; set; }
    }
}