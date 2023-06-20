using Misa_TruongWeb03.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Entity.EmisStudy.Supject
{
    public class Subject : BaseModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectImage { get; set; }
    }
}
