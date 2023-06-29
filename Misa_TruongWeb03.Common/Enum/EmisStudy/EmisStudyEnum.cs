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
            Preparing = 0,
            Prepared = 1,
            Shared = 2,
            NotShared = 3,
            FromLibrary = 4,
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
