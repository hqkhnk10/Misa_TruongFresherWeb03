using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Misa_TruongWeb03.BL.Service.BaseImport;
using Misa_TruongWeb03.BL.Service.EmulationTitleService;
using Misa_TruongWeb03.BL.Service.FileServices;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.EmulationTitle;
using Misa_TruongWeb03.Common.Helper;
using Misa_TruongWeb03.DL.Repository.EmulationTitleRepository;
using Misa_TruongWeb03.DL.Repository.FileRepository;
using Newtonsoft.Json;

namespace Misa_TruongWeb03.BL.Service.Import
{
    public class EmulationTitleImportService : BaseImportService<EmulationTitle>, IEmulationTitleImportService
    {
        #region Property
        private IEmulationTitleService _emulationTitleService;
        private IEmulationTitleRepository _emulationTitleRepository;
        #endregion
        #region Constructor
        public EmulationTitleImportService(IWebHostEnvironment env, IFileRepository fileRepository, IMemoryCache memoryCache, IFileService fileService, IEmulationTitleService emulationTitleService, IEmulationTitleRepository emulationTitleRepository) : base(env, fileRepository, memoryCache, fileService)
        {
            _emulationTitleService = emulationTitleService;
            _emulationTitleRepository = emulationTitleRepository;
        }
        #endregion
        #region Method
        /// <summary>
        /// Override hàm get dữ liệu, để xuất file
        /// </summary>
        /// <param name="model"></param>
        /// <returns>List<EmulationTitle></returns>
        /// Created By: NQTruong (01/06/2023)
        public override async Task<dynamic> Get(dynamic model)
        {
            var customMap = new CustomMap();
            var stringModel = model.ToString();
            var jsonModel = JsonConvert.DeserializeObject<dynamic>(stringModel);
            var getModel = customMap.MapDynamicToObject<GetEmulationTitle>(jsonModel);
            var res = await _emulationTitleRepository.Get(getModel);
            return res.Data;
        }
        /// <summary>
        /// Override kiểm tra trùng code trong DB
        /// </summary>
        /// <param name="cellValue"></param>
        /// <returns>true:không trùng/false:trùng</returns>
        /// Created By: NQTruong (01/06/2023)
        public override async Task<bool> IsDuplicateRecord(object cellValue)
        {
            return await _emulationTitleService.CheckDuplicateCode((string)cellValue);
        } 
        #endregion
    }
}
