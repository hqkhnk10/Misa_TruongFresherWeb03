﻿using AutoMapper;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.DL.Repository.EmulationTitle;
using System.Reflection;

namespace Misa_TruongWeb03.BL.Service.EmulationTitle
{
    public class EmulationTitleService : IEmulationTitleService
    {
        private readonly IEmulationTitleRepository _emulationTitleRepository;
        private readonly IMapper _mapper;
        public EmulationTitleService(IEmulationTitleRepository emulationTitleRepository, IMapper mapper)
        {
            _emulationTitleRepository = emulationTitleRepository;
            _mapper = mapper;
        }
        public async Task<BaseEntity> Get(GetEmulationTitle model)
        {
            var result = await _emulationTitleRepository.Get(model);
            return result;
        }
        public async Task<BaseEntity> GetDetail(int id)
        {
            var result = await _emulationTitleRepository.GetDetail(id);
            return result;
        }
        public async Task<BaseEntity> Post(PostEmulationTitle model)
        {
            var et = _mapper.Map<EmulationTitleModel>(model);
            et.EmulationTitleID = 0;
            var check = await _emulationTitleRepository.CheckDuplicate(et);
            if(check.StatusCode == 200)
            {
                var result = await _emulationTitleRepository.Post(model);
                return result;
            }
            return check;

        }
        public async Task<BaseEntity> Put(int id, PostEmulationTitle model)
        {
            var et = _mapper.Map<EmulationTitleModel>(model);
            et.EmulationTitleID = id;
            var check = await _emulationTitleRepository.CheckDuplicate(et);
            if (check.StatusCode == 200)
            {
                var result = await _emulationTitleRepository.Put(id, model);
                return result;
            }
            return check;
        }
        public async Task<BaseEntity> Delete(int id)
        {
            var result = await _emulationTitleRepository.Delete(id);
            return result;
        }
        public async Task<BaseEntity> DeleteMultiple(DeleteEmulationTitle model)
        {
            var result = await _emulationTitleRepository.DeleteMultiple(model);
            return result;
        }
    }
}
