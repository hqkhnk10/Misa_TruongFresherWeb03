using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Enum.EmisStudy
{
    public class EmisStudyEnum
    {
        public enum ExerciseStatus
        {
            Preparing = 1,
            Prepared = 2,
            Shared = 3,
            NotShared = 4,
            FromLibrary = 5,
        }
        public enum QuestionType
        {
            Choosing = 1,
            TrueOrFalse = 2,
            Fill = 3,
            Connect = 4,
            Group = 5,
            Write = 6,
        }
    }
}
