using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
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
    public class ExerciseDTO : Exercise
    {
        public string GradeName { get; set; }
        public string SubjectName { get; set; }
        public string SubjectImage { get; set; }
        public int Question { get; set; }
        public List<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();
    }
    public class ExerciseGetDTO 
    {
        public ExerciseStatus? ExerciseStatus { get; set; }
        public Guid? SubjectId { get; set; }
        public Guid? GradeId { get; set; }

    }
    public class ExercisePostDTO
    {
        public Guid? ExerciseId { get; set; }
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

}
