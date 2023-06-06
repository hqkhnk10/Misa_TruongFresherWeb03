using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.BL.Service.EmulationTitleService;
using Misa_TruongWeb03.BL.Service.FileServices;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.DL.Repository.EmulationTitleRepository;
using Misa_TruongWeb03.DL.Repository.FileRepository;

namespace Misa_TruongWeb03.BL.Service.Import
{
    public class EmulationTitleImportService : BaseImportService<EmulationTitle>, IEmulationTitleImportService
    {
        private IEmulationTitleService _emulationTitleService;
        private IEmulationTitleRepository _emulationTitleRepository;
        private IMapper _mapper;
        public EmulationTitleImportService(IWebHostEnvironment env, IFileRepository fileRepository, IMemoryCache memoryCache, IFileService fileService, IEmulationTitleService emulationTitleService, IEmulationTitleRepository emulationTitleRepository, IMapper mapper) : base(env, fileRepository, memoryCache, fileService)
        {
            _emulationTitleService = emulationTitleService;
            _emulationTitleRepository = emulationTitleRepository;
            _mapper = mapper;
        }
        public override async Task<dynamic> Get(dynamic model)
        {
            var getModel = _mapper.Map<GetEmulationTitle>(model);
            var res = await _emulationTitleRepository.Get(getModel);
            return res.Data;
        }
    public override async Task<bool> IsDuplicateRecord(object cellValue)
        {
            return await _emulationTitleService.CheckDuplicateCode((string)cellValue);
        }
    }
}
