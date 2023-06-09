﻿using Misa_TruongWeb03.Common.Entity.Base;
using static Misa_TruongWeb03.Common.Enum.EmulationTitleEnum;

namespace Misa_TruongWeb03.Common.Entity.EmulationTitle
{
    /// <summary>
    /// Model danh hiệu thi đua
    /// </summary>
    /// Created By: NQTruong (23/05/2023)
    public class EmulationTitle : BaseModel
    {
        /// <summary>
        /// Id của danh hiệu thi đua
        /// </summary>
        /// Created By: NQTruong (23/05/2023)
        public int EmulationTitleID { get; set; } = 0;
        /// <summary>
        /// Mã danh hiệu thi đua
        /// </summary>
        /// Created By: NQTruong (23/05/2023)
        public string? EmulationTitleCode { get; set; }
        /// <summary>
        /// Tên danh hiệu thi đua
        /// </summary>
        /// Created By: NQTruong (23/05/2023)
        /// 
        public string EmulationTitleName { get; set; } = string.Empty;
        /// <summary>
        /// Đối tượng khen thưởng danh hiệu thi đua
        /// </summary>
        /// Created By: NQTruong (23/05/2023)
        /// 
        public ApplyObject? ApplyObject { get; set; }
        /// <summary>
        /// Loại phong trào danh hiệu thi đua
        /// </summary>
        /// Created By: NQTruong (23/05/2023)
        /// 
        public MovementType? MovementType { get; set; }
        /// <summary>
        /// Trạng thái danh hiệu thi đua
        /// </summary>
        /// Created By: NQTruong (23/05/2023)

        public Inactive? Inactive { get; set; }
        /// <summary>
        /// Cấp khen thưởng danh hiệu thi đua
        /// </summary>
        /// Created By: NQTruong (23/05/2023)

        public CommendationLevel? CommendationLevel { get; set; }

        /// <summary>
        /// Ghi chú danh hiệu thi đua
        /// </summary>
        /// Created By: NQTruong (23/05/2023)
        public string? EmulationTitleNote { get; set; }

    }

}
