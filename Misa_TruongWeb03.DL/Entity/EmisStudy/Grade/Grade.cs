using Misa_TruongWeb03.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Entity.EmisStudy.Grade
{
    public class Grade : BaseModel
    {
        public int GradeId { get; set; }
        public string GradeName { get; set;}
    }
}
