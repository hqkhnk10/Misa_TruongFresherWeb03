using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Misa_TruongWeb03.Common.Enum.EmisStudy.EmisStudyEnum;
using static Misa_TruongWeb03.Common.Enum.EmulationTitleEnum;

namespace Misa_TruongWeb03.Common.DTO.EmisStudy
{
    public class ExerciseGetDTO : FilterModel
    {
        public ExerciseStatus? ExerciseStatus { get; set; } = null;
        public Guid? GradeId { get; set; } = null;
        public Guid? SubjectId { get; set; } = null;

    }
    public class ExercisePostDTO
    {
        public Guid? ExerciseId { get; set; } = null;
        [Required]
        [MaxLength(255)]
        public string ExerciseName { get; set; }
        [Required]
        [EnumDataType(typeof(ExerciseStatus))]
        public ExerciseStatus ExerciseStatus { get; set; }
        [Required]
        public Guid SubjectId { get; set; }
        [Required]
        public Guid GradeId { get; set; }
        public Guid? TopicId { get; set; } = null;
    }
    public class ExercisePutDTO : ExercisePostDTO
    {
    }

    public class QuestionPostModel
    {
        [Required]
        [EnumDataType(typeof(QuestionType))]
        public QuestionType QuestionType { get; set; }
        [Required]
        [MaxLength(255)]
        public string QuestionContent { get; set; }
        [MaxLength(255)]
        public string? QuestionNote { get; set; }
        public IFormFile? QuestionImage { get; set; } = null;
        public List<AnswerPostModel> Answers { get; set; }
    }
    public class AnswerPostModel
    {
        [Required]
        [MaxLength(255)]
        public string? AnswerContent { get; set; }
        [Required]
        public bool AnswerStatus { get; set; }
        public IFormFile? AnswerImage { get; set; } = null;
    }
}
