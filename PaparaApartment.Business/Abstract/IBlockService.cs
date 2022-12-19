using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entities.Dtos.Block;
using PaparaApartment.Entity.Dtos.Block;
using System.Collections.Generic;


namespace PaparaApartment.Business.Abstract
{
    public  interface IBlockService
    {
        IDataResult<List<BlockViewDto>> GetAll();
        IResult Add(BlockAddDto blockAddDto);
        IResult Update(BlockUpdateDto updateBlockDto);
        IResult Delete(int blockId);
    }
}
