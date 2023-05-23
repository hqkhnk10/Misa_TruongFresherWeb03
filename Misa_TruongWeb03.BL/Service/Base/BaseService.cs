using AutoMapper;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.DL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.Base
{
    public abstract class BaseService<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto> : IBaseService<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto>
    {
        protected readonly IBaseRepository<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity,TEntityGetDto,TEntityPostDto,TEntityPutDto> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<BaseEntity> Get(TEntityGetDto model)
        {
            var result = await _baseRepository.Get(model);
            return result;
        }

        public async Task<BaseEntity> GetDetail(int id)
        {
            var result = await _baseRepository.GetById(id);
            return result;
        }

        public virtual async Task<BaseEntity> Post(TEntityPostDto model)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<BaseEntity> Put(int id, TEntityPostDto model)
        {
            throw new NotImplementedException();
        }
        public async Task<BaseEntity> Delete(int id)
        {
            var result = await _baseRepository.Delete(id);
            return result;
        }
        public async Task<BaseEntity> CheckDuplicate(TEntity model)
        {
            return await _baseRepository.CheckDuplicate(model);
        }
    }
}
