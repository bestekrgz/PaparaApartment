using AutoMapper;
using Microsoft.AspNetCore.Http;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Business.Constant;
using PaparaApartment.Core.Extensions;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Entities.Dtos.Block;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Entity.Dtos.Block;
using System;
using System.Collections.Generic;


namespace PaparaApartment.Business.Concrete
{

    public class BlockAdmin : IBlockService
    {
        private IBlockDal _blockDal;
        private IHttpContextAccessor _httpContextAccessor;
        private IMapper _mapper;

        public BlockAdmin(IBlockDal blockDal, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _blockDal = blockDal;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public IDataResult<List<BlockViewDto>> GetAll()
        {
            var blockList = _blockDal.GetList(x => x.IsActive == true);
            var blockViewList = _mapper.Map<List<BlockViewDto>>(blockList);
            return new SuccessDataResult<List<BlockViewDto>>(blockViewList);
        }

        public IResult Add(BlockAddDto blockAddDto)
        {
            var blockCheck = _blockDal.Any(x => x.Letter == blockAddDto.Letter);
            if (blockCheck)
            {
                return new ErrorResult(Messages.BlockLetterAlreadyExist);
            }

            var newBlock = _mapper.Map<Block>(blockAddDto);
            newBlock.IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newBlock.Idate = DateTime.Now;
            _blockDal.Add(newBlock);
            return new SuccessResult(Messages.BlockAdded);
        }

        public IResult Update(BlockUpdateDto updateBlockDto)
        {
            var updateBlock = _blockDal.Get(x => x.Id == updateBlockDto.Id);
            if (updateBlock is null)
            {
                return new ErrorResult(Messages.BlockNotFound);
            }

            updateBlock = _mapper.Map(updateBlockDto, updateBlock);
            updateBlock.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            updateBlock.Udate = DateTime.Now;
            _blockDal.Update(updateBlock);
            return new SuccessResult(Messages.BlockUpdated);
        }

        public IResult Delete(int blockId)
        {
            var deleteBlock = _blockDal.Get(x => x.Id == blockId);
            if (deleteBlock is null)
            {
                return new ErrorResult(Messages.BlockNotFound);
            }

            deleteBlock.IsActive = false;
            deleteBlock.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            deleteBlock.Udate = DateTime.Now;
            _blockDal.Update(deleteBlock);
            return new SuccessResult(Messages.BlockRemoved);
        }
    }
}
