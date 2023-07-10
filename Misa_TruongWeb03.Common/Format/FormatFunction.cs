using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Misa_TruongWeb03.Common.Enum.EmisStudy.EmisStudyEnum;
using static Misa_TruongWeb03.Common.Enum.EmulationTitleEnum;

namespace Misa_TruongWeb03.Common.Format
{
    /// <summary>
    /// Hàm format cho excel config
    /// </summary>
    public class FormatFunction
    {
        /// <summary>
        /// Format đối tượng khen thưởng
        /// </summary>
        /// <param name="value"></param>
        /// <returns>ApplyObject?</returns>
        /// Created By: NQTruong (05/06/2023)
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
        /// <summary>
        /// Format loại phong trào
        /// </summary>
        /// <param name="value"></param>
        /// <returns>MovementType?</returns>
        /// Created By: NQTruong (05/06/2023)
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
        /// <summary>
        /// Format trạng thái
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Inactive?</returns>
        /// Created By: NQTruong (05/06/2023)
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
        /// <summary>
        /// Format cấp khen thưởng
        /// </summary>
        /// <param name="value"></param>
        /// <returns>CommendationLevel?</returns>
        /// Created By: NQTruong (05/06/2023)
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
        /// <summary>
        /// Convert đối tượng khen thưởng
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string?</returns>
        /// Created By: NQTruong (05/06/2023)
        public static string? convertApplyObject(ApplyObject value)
        {
            switch (value)
            {
                case ApplyObject.Personal:
                    return "Cá nhân";
                case ApplyObject.Group:
                    return "Tập thể";
                case ApplyObject.PersonalAndGroup:
                    return "Cá nhân và tập thể";
                default:
                    return null;
            }
        }
        /// <summary>
        /// Convert loại phong trào
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string?</returns>
        /// Created By: NQTruong (05/06/2023)
        public static string? convertMovementType(MovementType value)
        {
            switch (value)
            {
                case MovementType.Regular:
                    return "Thường xuyên";
                case MovementType.Period:
                    return "Theo đợt";
                case MovementType.RegularAndPeriod:
                    return "Thường xuyên;Theo đợt";
                default:
                    return null;
            }
        }
        /// <summary>
        /// Convert trạng thái
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string?</returns>
        /// Created By: NQTruong (05/06/2023)
        public static string? convertInactive(Inactive value)
        {
            switch (value)
            {
                case Inactive.Inactive:
                    return "Ngưng sử dụng";
                case Inactive.Active:
                    return "Đang sử dụng";
                default:
                    return null;
            }
        }
        /// <summary>
        /// Conver cấp khen thưởng
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string?</returns>
        /// Created By: NQTruong (05/06/2023)
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

        /// <summary>
        /// Format loại câu hỏi
        /// </summary>
        /// <param name="value"></param>
        /// <returns>ApplyObject?</returns>
        /// Created By: NQTruong (05/07/2023)
        public static QuestionType? FormatQuestionType(string value)
        {
            switch (value)
            {
                case "Chọn đáp án":
                    return QuestionType.Choosing;
                case "Đúng sai":
                    return QuestionType.TrueOrFalse;
                case "Điền vào chỗ trống":
                    return QuestionType.Fill;
                case "Tự luận":
                    return QuestionType.Write;
                default:
                    return null;
            }
        }
    }
}
