using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.BL.Service.BaseImport;
using Misa_TruongWeb03.BL.Service.FileServices;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
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
    public class QuestionImportService : BaseImportService<QuestionImportDTO>, IQuestionImportService
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
        public override dynamic FormatReturnData(List<dynamic>? data)
        {
            var returnData = new List<QuestionPostDTO>();
            foreach (var question in data)
            {
                var questionDto = new QuestionPostDTO
                {
                    QuestionType = question.QuestionType,
                    QuestionContent = question.QuestionContent,
                    QuestionNote = question.QuestionNote,
                };
            }
            return data;
        }
        public override void SetProperty(QuestionImportDTO instance, string propertyName, object value)
        {
            var propertyInfo = typeof(QuestionImportDTO).GetProperty(propertyName);

            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                Type propertyType = propertyInfo.PropertyType;
                object convertedValue = null;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // Property type is nullable, get the underlying type
                    propertyType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                }
                try
                {
                    convertedValue = Convert.ChangeType(value, propertyType);
                }
                catch (InvalidCastException)
                {
                    // Handle the case where the value cannot be converted to the desired type
                    // Set a default value or null based on the property type
                    if (propertyType.IsValueType)
                    {
                        // Value type, set default value
                        convertedValue = Activator.CreateInstance(propertyType);
                    }
                    else
                    {
                        // Reference type, set null
                        convertedValue = null;
                    }
                }
                if (propertyName == "AnswerContent")
                {
                    if(value is null)
                    {
                        return;
                    }
                    instance.Answers.Add(new AnswerPostDTO
                    {
                        AnswerContent = value.ToString()
                    });
                }
                if(propertyName == "Result")
                {
                    if(value is null)
                    {
                        return;
                    }
                    var answerResult = value.ToString().Split("||");
                    foreach (var answer in answerResult)
                    {
                        instance.Answers[Int32.Parse(answer) - 1].AnswerStatus = true;
                    }
                    
                }
                else
                {
                    propertyInfo.SetValue(instance, convertedValue);
                }

            }
        }
    }
}
