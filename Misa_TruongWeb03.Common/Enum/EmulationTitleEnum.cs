using System.ComponentModel;

namespace Misa_TruongWeb03.Common.Enum
{
    public class EmulationTitleEnum
    {
        public enum ApplyObject
        {
            [Description("Tập thể")]
            Group = 2,
            [Description("Cá nhân")]
            Personal = 3,
        }
        public enum MovementType
        {
            [Description("Thường xuyên")]
            Regular = 0,
            [Description("Theo đợt")]
            Period = 1,
        }
        public enum Inactive
        {
            [Description("Ngưng sử dụng")]
            Inactive = 1,
            [Description("Sử dụng")]
            Active = 0,
        }
        public enum CommendationLevel
        {
            [Description("Cấp nhà nước")]
            CountryLevel = 1,
            [Description("Cấp tỉnh")]
            ProvinceLevel = 2,
            [Description("Cấp Huyện")]
            DistrictLevel = 3,
            [Description("Cấp Xã")]
            CommuneLevel = 4,
        }
    }
}
