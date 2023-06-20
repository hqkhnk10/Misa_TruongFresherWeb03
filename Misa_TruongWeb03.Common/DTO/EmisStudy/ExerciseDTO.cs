using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Misa_TruongWeb03.Common.Enum.EmisStudy.EmisStudyEnum;

namespace Misa_TruongWeb03.Common.DTO.EmisStudy
{
    public class ExerciseGetDTO : GetModel
    {
    }
    public class ExercisePostDTO
    {
        [Required]
        public string ExerciseName { get; set; }
        [Required]
        public ExerciseStatus ExerciseStatus { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int GradeId { get; set; }
        public int TopicId { get; set; }
        public IFormFile? ExerciseImage { get; set; } = null;
        public List<QuestionPostModel> Questions { get; set; }
    }
    public class ExercisePutDTO
    {
    }
    public class QuestionPostModel
    {
        [Required]
        public QuestionType QuestionType { get; set; }
        [Required]
        public string QuestionContent { get; set; }
        public string? QuestionNote { get; set; }
        public IFormFile? QuestionImage { get; set; } = null;
        public List<AnswerPostModel> Answers { get; set; }
    }
    public class AnswerPostModel
    {
        [Required]
        public string AnswerContent { get; set; }
        [Required]
        public bool Status { get; set; }
        public IFormFile? AnswerImage { get; set; } = null;
    }
}
