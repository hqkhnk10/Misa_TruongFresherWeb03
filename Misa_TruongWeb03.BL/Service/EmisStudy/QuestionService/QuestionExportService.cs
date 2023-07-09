using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Misa_TruongWeb03.BL.Service.BaseExport;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.DL.Repository.FileRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.QuestionService
{
    public class QuestionExportService : BaseExportService<Question>, IQuestionExportService
    {
        public QuestionExportService(IWebHostEnvironment env, IFileRepository fileRepository, IMemoryCache memoryCache) : base(env, fileRepository, memoryCache)
        {

        }
    }
}
