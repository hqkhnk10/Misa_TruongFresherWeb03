using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Misa_TruongWeb03.Common.Enum.EmulationTitleEnum;

namespace Misa_TruongWeb03.Common.DTO
{
    public class GetEmulationTitle
    {
        public int pageSize { get; set; } = 10;
        public int pageIndex { get; set; } = 1;
        public string? keyword { get; set; } = null;
        public ApplyObject? ApplyObject { get; set; } = null;
        public CommendationLevel? CommendationLevel { get; set; } = null;
        public MovementType? MovementType { get; set; } = null;
        public Inactive? Inactive { get; set; } = null;
    }
    public class PostEmulationTitle
    {
        [Required]
        [DefaultValue("test")]
        public string EmulationTitleName { get; set; } = string.Empty;
        [Required]
        [DefaultValue("test")]
        public string EmulationTitleCode { get; set; } = string.Empty;
        [EnumDataType(typeof(ApplyObject))]
        [DefaultValue(2)]
        public int? ApplyObject { get; set; } = null;
        [Required]
        [EnumDataType(typeof(CommendationLevel))]
        [DefaultValue(1)]
        public int? CommendationLevel { get; set; } = null;
        [EnumDataType(typeof(MovementType))]
        [DefaultValue(1)]
        public int? MovementType { get; set; } = null;
        [EnumDataType(typeof(Inactive))]
        [DefaultValue(1)]
        public int? Inactive { get; set; } = null;
        public string CreatedBy { get; set; } = "demo";
    }
    public class UpdateEmulationTitle : PostEmulationTitle
    {
        [Required]
        public int EmulationTitleID { get; set; }
    }
    public class DeleteEmulationTitle
    {
        [Required]
        public List<int> Id { get; set; } = new List<int>();
    }
}
