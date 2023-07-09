using Microsoft.AspNetCore.Hosting;
using Misa_TruongWeb03.BL.Service.BaseImport;
using Misa_TruongWeb03.BL.Service.FileServices;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
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
        public QuestionImportService(IWebHostEnvironment env, IFileRepository fileRepository, IFileService fileService) : base(env, fileRepository, fileService)
        {

        }
    }
}
