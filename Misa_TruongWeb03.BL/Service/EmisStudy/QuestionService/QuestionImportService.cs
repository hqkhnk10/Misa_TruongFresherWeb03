using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.BL.Service.BaseImport;
using Misa_TruongWeb03.BL.Service.FileServices;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.Common.Entity.FileEntity;
using Misa_TruongWeb03.DL.Repository.FileRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.QuestionService
{
    public class QuestionImportService : BaseImportService<Question>, IQuestionImportService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileService _fileService;

        public QuestionImportService(IWebHostEnvironment env, IFileRepository fileRepository, IFileService fileService) : base(env, fileRepository, fileService)
        {
            _fileRepository = fileRepository;
            _fileService = fileService;
        }
        /// <summary>
        /// Xác thực dữ liệu của file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        /// Created By: NQTruong (01/06/2023)
        public override async Task<FileValidateModel> Validate(IFormFile file, int sheetIndex, int header)
        {
            try
            {
                //upload file
                var res = await _fileService.Upload(file);

                //get config
                var configEntity = await _fileRepository.MappingConfig("question");
                var mapper = new ExcelConfigMapper();
                var config = mapper.MapToExcelConfig(configEntity);

                var data = await ReadExcel(res.FilePath, sheetIndex, header, config);
                data.FileName = res.FileStoreName;

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
