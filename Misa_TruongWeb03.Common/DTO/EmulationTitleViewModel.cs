using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Misa_TruongWeb03.Common.Enum.EmulationTitleEnum;

namespace Misa_TruongWeb03.Common.DTO
{
    public class EmulationTitleViewModel
    {
        public int EmulationTitleID { get; set; }
        public string? EmulationTitleCode { get; set; }
        public string EmulationTitleName { get; set; } = String.Empty;
        public ApplyObject? ApplyObject { get; set; }
        public MovementType? MovementType { get; set; }
        public Inactive? Inactive { get; set; }
        public CommendationLevel? CommendationLevel { get; set; }
    }
}
