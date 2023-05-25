using Misa_TruongWeb03.Common.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Misa_TruongWeb03.Common.Enum.EmulationTitleEnum;

namespace Misa_TruongWeb03.Common.DTO
{
    /// <summary>
    /// Model cho GET của danh hiệu thi đua
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
    public class GetEmulationTitle : GetModel
    {
        public ApplyObject? ApplyObject { get; set; } = null;
        public CommendationLevel? CommendationLevel { get; set; } = null;
        public MovementType? MovementType { get; set; } = null;
        public Inactive? Inactive { get; set; } = null;
        public bool? ApplyObjectSort { get; set; } = null;
        public bool? CommendationLevelSort { get; set; } = null;
        public bool? MovementTypeSort { get; set; } = null;
        public bool? InactiveSort { get; set; } = null;
        public bool? EmulationTitleNameSort { get; set; } = null;
    }
    /// <summary>
    /// Model cho POST của danh hiệu thi đua
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
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
        public string EmulationTitleNote { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = "demo";
    }
    /// <summary>
    /// Model cho PUT của danh hiệu thi đua
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
    public class UpdateEmulationTitle : PostEmulationTitle
    {
        [Required]
        public int EmulationTitleID { get; set; }
    }
    /// <summary>
    /// Model cho xóa nhiều của danh hiệu thi đua
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
    public class DeleteEmulationTitle
    {
        [Required]
        public List<int> Id { get; set; } = new List<int>();
    }
    public class UpdateEmulationTitleStatusDto
    {
        [Required]
        public int EmulationTitleID { get; set; }
        [Required]
        [EnumDataType(typeof(Inactive))]
        [DefaultValue(0)]
        public int? Inactive { get; set; }
    }
    public class UpdateMultipleEmulationTitleStatusDto
    {
        [Required]
        public List<int> Id { get; set; } = new List<int>();
        [Required]
        [EnumDataType(typeof(Inactive))]
        public int? Inactive { get; set; }
    }
}
