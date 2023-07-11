using Misa_TruongWeb03.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Entity.EmisStudy.Answer
{
    public class Answer : BaseModel
    {
        public Guid AnswerId { get; set; }
        public string AnswerContent { get; set; }
        public bool AnswerStatus { get; set; }
        public string AnswerImage { get; set; }
        public Guid QuestionId { get; set; }
    }
    public class AnswerModel : Answer
    {

    }
}
