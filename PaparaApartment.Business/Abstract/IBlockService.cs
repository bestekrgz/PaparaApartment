using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
