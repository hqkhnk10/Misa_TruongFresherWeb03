using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Resource;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Misa_TruongWeb03.Common.Enum.EmulationTitleEnum;

namespace Misa_TruongWeb03.Common.DTO
{
    /// <summary>
    /// Model cho GET của danh hiệu thi đua
    /// </summary>
    /// CreatedBy: NQTruong (24/05/2023)
    public class GetEmulationTitle : FilterModel
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
    /// CreatedBy: NQTruong (24/05/2023)
    public class PostEmulationTitle
    {
        [Required(ErrorMessage = "Tên danh hiệu không được để trống")]
        [MaxLength(255, ErrorMessage = "Tên danh hiệu tối đa 255 kí tự")]
        [DefaultValue("test")]
        public string EmulationTitleName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mã danh hiệu không được để trống")]
        [MaxLength(20, ErrorMessage = "Mã danh hiệu tối đa 20 kí tự")]
        [DefaultValue("test")]
        public string EmulationTitleCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Đối tượng khen thưởng không được để trống")]
        [EnumDataType(typeof(ApplyObject), ErrorMessage = "Dữ liệu không phù hợp cho Đối tượng khen thưởng")]
        [DefaultValue(2)]
        public int? ApplyObject { get; set; } = null;
        [Required(ErrorMessage = "Cấp khen thưởng không được để trống")]
        [EnumDataType(typeof(CommendationLevel), ErrorMessage = "Dữ liệu không phù hợp cho Cấp khen thưởng")]
        [DefaultValue(1)]
        public int? CommendationLevel { get; set; } = null;
        [Required(ErrorMessage = "Cấp phong trào không được để trống")]
        [EnumDataType(typeof(MovementType), ErrorMessage = "Dữ liệu không phù hợp cho Cấp phong trào")]
        [DefaultValue(1)]
        public int? MovementType { get; set; } = null;
        [EnumDataType(typeof(Inactive), ErrorMessage = "Dữ liệu không phù hợp cho Trạng thái")]
        [DefaultValue(1)]
        public int? Inactive { get; set; } = null;

        [MaxLength(255, ErrorMessage = "Tối đa 255 kí tự")]
        public string? EmulationTitleNote { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = "demo";
    }
    /// <summary>
    /// Model cho PUT của danh hiệu thi đua
    /// </summary>
    /// CreatedBy: NQTruong (24/05/2023)
    public class UpdateEmulationTitle : PostEmulationTitle
    {
        [Required]
        public int EmulationTitleID { get; set; }

    }
    /// <summary>
    /// Model cho xóa nhiều của danh hiệu thi đua
    /// </summary>
    /// CreatedBy: NQTruong (24/05/2023)
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
        public List<int> Id { get; set; }
        [Required]
        [EnumDataType(typeof(Inactive))]
        public int? Inactive { get; set; }
    }

}
