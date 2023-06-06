using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Misa_TruongWeb03.Common.Enum.EmulationTitleEnum;

namespace Misa_TruongWeb03.Common.Format
{
    public class FormatFunction
    {
        public static ApplyObject? formatApplyObject(string value)
        {
            switch (value)
            {
                case "Cá nhân":
                    return ApplyObject.Personal;
                case "Tập thể":
                    return ApplyObject.Group;
                case "Cá nhân và tập thể":
                    return ApplyObject.PersonalAndGroup;
                default:
                    return null;
            }
        }
        public static MovementType? formatMovementType(string value)
        {
            switch (value)
            {
                case "Thường xuyên":
                    return MovementType.Regular;
                case "Theo đợt":
                    return MovementType.Period;
                case "Thường xuyên;Theo đợt":
                    return MovementType.RegularAndPeriod;
                default:
                    return null;
            }
        }
        public static Inactive? formatInactive(string value)
        {
            switch (value)
            {
                case "Ngưng sử dụng":
                    return Inactive.Inactive;
                case "Đang sử dụng":
                    return Inactive.Active;
                default:
                    return null;
            }
        }
        public static CommendationLevel? formatCommendationLevel(string value)
        {
            switch (value)
            {
                case "Cấp Nhà nước":
                    return CommendationLevel.CountryLevel;
                case "Cấp Tỉnh/tương đương":
                    return CommendationLevel.ProvinceLevel;
                case "Cấp Huyện/tương đương":
                    return CommendationLevel.DistrictLevel;
                case "Cấp Xã/tương đương":
                    return CommendationLevel.CommuneLevel;
                default:
                    return null;
            }
        }
        public static string? convertCommendationLevel(CommendationLevel value)
        {
            switch (value)
            {
                case CommendationLevel.CountryLevel:
                    return "Cấp Nhà nước";
                case CommendationLevel.ProvinceLevel:
                    return "Cấp Tỉnh/tương đương";
                case CommendationLevel.DistrictLevel:
                    return "Cấp Huyện/tương đương";
                case CommendationLevel.CommuneLevel:
                    return "Cấp Xã/tương đương";
                default:
                    return null;
            }
        }

    }
}
