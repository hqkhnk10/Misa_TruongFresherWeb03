using Misa_TruongWeb03.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Entity.EmisStudy.Topic
{
    public class Topic : BaseModel
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int SubjectId { get; set; }
        public int GradeId { get; set; }
    }
}
