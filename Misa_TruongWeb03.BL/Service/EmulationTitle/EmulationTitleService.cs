using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmulationTitle;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Repository.EmulationTitleRepository;
using System.Reflection;

namespace Misa_TruongWeb03.BL.Service.EmulationTitleService
{
    /// <summary>
    /// Tầng Service của danh hiệu thi đua
    /// Kế thừa CRUD từ base
    /// </summary>
    /// CreatedBy: NQTruong (24/05/2023)
    public class EmulationTitleService : BaseService<EmulationTitle,GetEmulationTitle,PostEmulationTitle,UpdateEmulationTitle>,IEmulationTitleService
    {
        #region Property
        private readonly IEmulationTitleRepository _emulationTitleRepository;
        #endregion
        #region Constructor
        public EmulationTitleService(IEmulationTitleRepository emulationTitleRepository, IMapper mapper) : base(emulationTitleRepository, mapper)
        {
            _emulationTitleRepository = emulationTitleRepository;
        }
        #endregion
        #region Method
        /// <summary>
        /// Thêm danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Created By: NQTruong (23/05/2023)
        public override async Task<BaseEntity> Post(PostEmulationTitle model)
        {
            //Check trùng mã danh hiệu
            var updateModel = _mapper.Map<EmulationTitle>(model);
            var check = await CheckDuplicate(updateModel);
            if (check.ErrorCode == StatusCodes.Status409Conflict)
            {
                return check;
            }
            var result = await _baseRepository.Post(model);
            if (result.Data == null || (int)result.Data == 0)
            {
                return new DatabaseError();
            }
            return result;

        }
        /// <summary>
        /// Sửa danh hiệu thi đua
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Created By: NQTruong (23/05/2023)
        public override async Task<BaseEntity> Put(int id, PostEmulationTitle model)
        {
            //Check trùng mã danh hiệu
            var et = _mapper.Map<EmulationTitle>(model);
            et.EmulationTitleID = id;
            var check = await CheckDuplicate(et);
            if (check.ErrorCode == StatusCodes.Status409Conflict)
            {
                return check;
            }
            var updateModel = _mapper.Map<UpdateEmulationTitle>(et);
            var result = await _baseRepository.Put(updateModel);
            if (result.Data == null || (int)result.Data == 0)
            {
                return new DatabaseError();
            }
            return result;
        }
        /// <summary>
        /// Xóa nhiều danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Created By: NQTruong (23/05/2023)
        public async Task<BaseEntity> DeleteMultiple(DeleteEmulationTitle model)
        {
            var result = await _emulationTitleRepository.DeleteMultiple(model);
            if (result.Data == null || (int)result.Data == 0)
            {
                return new DatabaseError();
            }
            return result;
        }
        /// <summary>
        /// Thay đổi trạng thái nhiều danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Created By: NQTruong (23/05/2023)
        public async Task<BaseEntity> UpdateStatus(UpdateEmulationTitleStatusDto model)
        {
            var result = await _emulationTitleRepository.UpdateStatus(model);
            if (result.Data == null || (int)result.Data == 0)
            {
                return new DatabaseError();
            }
            return result;
        }
        /// <summary>
        /// Thay đổi trạng thái nhiều danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Created By: NQTruong (23/05/2023)
        public async Task<BaseEntity> UpdateMultipleStatus(UpdateMultipleEmulationTitleStatusDto model)
        {
            var result = await _emulationTitleRepository.UpdateMultipleStatus(model);
            if (result.Data == null || (int)result.Data == 0)
            {
                return new DatabaseError();
            }
            return result;
        }
        /// <summary>
        /// Thêm danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Created By: NQTruong (23/05/2023)
        public async Task<BaseEntity> InsertMultiple(IEnumerable<PostEmulationTitle> models)
        {
            //Check trùng mã danh hiệu
            var listCode = models.Select(model => model.EmulationTitleCode);
            var check = await _emulationTitleRepository.CheckDuplicateMultiple(listCode);
            if(check.ErrorCode == StatusCodes.Status200OK)
            {
                var result = await _emulationTitleRepository.InsertMultiple(models);
                return result;
            }
            else
            {
                check.DevMsg = VN.DuplicateError;
                check.UserMsg = VN.DuplicateError;
                return check;
            }


        }
        public async Task<bool> CheckDuplicateCode(string code)
        {
            return await _emulationTitleRepository.CheckDuplicateCode(code);
        }
        #endregion
    }
}
