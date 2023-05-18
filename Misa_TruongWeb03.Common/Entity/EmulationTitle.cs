using static Misa_TruongWeb03.Common.Enum.EmulationTitleEnum;

namespace Misa_TruongWeb03.Common.Entity
{
    public class EmulationTitleModel
    {
        public int EmulationTitleID { get; set; }
        public string? EmulationTitleCode { get; set; }
        public string EmulationTitleName { get; set; } = String.Empty;
        public ApplyObject? ApplyObject { get; set; }
        public MovementType? MovementType { get; set; }
        public Inactive? Inactive { get; set; }
        public CommendationLevel? CommendationLevel { get; set; }
        public int Count { get; set; }
    }

}
