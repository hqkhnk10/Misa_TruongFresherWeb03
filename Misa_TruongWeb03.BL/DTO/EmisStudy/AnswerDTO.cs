using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.DTO.EmisStudy
{
    public class AnswerDTO : Answer
    {
    }
    public class AnswerGetDTO { }
    public class AnswerPostDTO
    {
        [Required]
        [MaxLength(255)]
        public string? AnswerContent { get; set; }
        [Required]
        public bool AnswerStatus { get; set; }
        public IFormFile? AnswerImage { get; set; } = null;
    }
    public class AnswerPutDTO
    {
    }
}
