using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.Common.DTO;

namespace Misa_TruongWeb03.Controller
{
    public interface IFileController
    {
        public Task<IActionResult> ValidateFile(ValidateFileDTO model);
        public IActionResult GetSampleFile();
    }
}
