using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Misa_TruongWeb03.Common.Enum.EmisStudy.EmisStudyEnum;

namespace Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise
{
    public class Exercise :BaseModel
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set;}
        public int SubjectId { get; set; }
        public int GradeId { get; set; }
        public int? TopicId { get; set; }
        public string ExerciseImage { get; set; }
        public ExerciseStatus ExerciseStatus { get; set; }
        public string JsonData { get; set; }
        public string GradeName { get; set; }
        public string SubjectName { get; set; }
        public string SubjectImage { get; set; }
        public int Question { get; set; }
    }
    public class DetailExerciseModel : Exercise
    {
        public List<QuestionDetailModel> Questions { get; set; } = new List<QuestionDetailModel>();
    }
}
