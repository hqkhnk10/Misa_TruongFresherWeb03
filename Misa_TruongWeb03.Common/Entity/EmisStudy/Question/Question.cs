using Misa_TruongWeb03.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Misa_TruongWeb03.Common.Enum.EmisStudy.EmisStudyEnum;

namespace Misa_TruongWeb03.Common.Entity.EmisStudy.Question
{
    public class Question : BaseModel
    {
        public int QuestionId { get; set; }
        public QuestionType QuestionType
        {
            get; set;
        }
        public string QuestionContent { get; set; }
        public string QuestionNote { get; set; }
        public string QuestionImage { get; set; }
        public int ExerciseId { get; set; }

    }
}
