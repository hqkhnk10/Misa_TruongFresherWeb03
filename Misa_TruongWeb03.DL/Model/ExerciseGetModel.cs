using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Model
{
    public class ExerciseGetModel : Exercise
    {
        public string GradeName { get; set; }
        public string SubjectName { get; set; }
        public int Question { get; set; }

    }
}
