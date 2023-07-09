﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Misa_TruongWeb03.Common.Enum.EmisStudy.EmisStudyEnum;

namespace Misa_TruongWeb03.Common.DTO.EmisStudy
{
    public class QuestionGetDTO
    {
    }
    public class QuestionPostDTO
    {
        public ExerciseModelDTO Exercise { get; set; }
        [Required]
        [EnumDataType(typeof(QuestionType))]
        public QuestionType QuestionType { get; set; }
        [Required]
        [MaxLength(255)]
        public string QuestionContent { get; set; }
        [MaxLength(255)]
        public string? QuestionNote { get; set; } = null;
        public IFormFile? QuestionImage { get; set; } = null;
        public List<AnswerPostModel> Answers { get; set; }
    }
    public class QuestionPutDTO : QuestionPostDTO
    {
        [Required]
        public int QuestionId { get; set; }

    }
}