using System.ComponentModel;

namespace Misa_TruongWeb03.Common.Enum
{
    /// <summary>
    /// Enum của danh hiệu thi đua
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
    public class EmulationTitleEnum
    {
        /// <summary>
        /// Đối tượng khen thưởng
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public enum ApplyObject
        {
            [Description("Tập thể")]
            Group = 2,
            [Description("Cá nhân")]
            Personal = 1,
            [Description("Cá nhân và tập thể")]
            PersonalAndGroup = 3,
        }
        /// <summary>
        /// Loại phong trào
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public enum MovementType
        {
            [Description("Thường xuyên")]
            Regular = 0,
            [Description("Theo đợt")]
            Period = 1,
            [Description("Thường xuyên;Theo đợt")]
            RegularAndPeriod = 2,
        }
        /// <summary>
        /// Trạng thái
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public enum Inactive
        {
            [Description("Ngưng sử dụng")]
            Inactive = 1,
            [Description("Sử dụng")]
            Active = 0,
        }
        /// <summary>
        /// Cấp khen thưởng
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public enum CommendationLevel
        {
            [Description("Cấp nhà nước")]
            CountryLevel = 0,
            [Description("Cấp tỉnh")]
            ProvinceLevel = 1,
            [Description("Cấp Huyện")]
            DistrictLevel = 2,
            [Description("Cấp Xã")]
            CommuneLevel = 3,
        }
    }
}
