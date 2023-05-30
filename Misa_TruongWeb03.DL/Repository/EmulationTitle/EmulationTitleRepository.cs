﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Repository.Base;
using System.Data;

namespace Misa_TruongWeb03.DL.Repository.EmulationTitleRepository
{
    /// <summary>
    /// Repository của danh hiệu thi đua
    /// Kế thừa CRUD từ base
    /// kết nối với database
    /// </summary>
    /// CreatedBy: NQTruong (24/05/2023)
    public class EmulationTitleRepository : BaseRepository<EmulationTitle, GetEmulationTitle, PostEmulationTitle, UpdateEmulationTitle>, IEmulationTitleRepository
    {
        #region Constructor
        public EmulationTitleRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Method
        /// <summary>
        /// Xóa nhiều danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<BaseEntity> DeleteMultiple(DeleteEmulationTitle model)
        {
            using var conn = this.GetConnection();
            try
            {
                conn.Open();
                var query = "proc_emulationtitle_delete_multiple";
                var stringInt = String.Join(",", model.Id);
                var result = await conn.QueryFirstAsync<int>(query, new
                {
                    int_array = stringInt,
                }, commandType: CommandType.StoredProcedure);
                if (result == 0)
                {
                    var notFound = new BaseEntity
                    {
                        Rows = result,
                        ErrorCode = 400,
                        Data = null,
                        DevMsg = VN.DatabaseError,
                        UserMsg = VN.ErrorDelete
                    };
                    return notFound;
                }
                var newResult = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status200OK,
                    Data = result
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    DevMsg = ex.Message
                };
                return exception;
            }
            finally { conn.Close(); }
        }

        /// <summary>
        /// Thay đổi trạng thái của 1 danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<BaseEntity> UpdateStatus(UpdateEmulationTitleStatusDto model)
        {
            using var conn = this.GetConnection();
            try
            {
                conn.Open();
                var query = "proc_emulationtitle_update_status";
                var result = await conn.QueryFirstAsync<int>(query, new
                {
                    model.EmulationTitleID,
                    model.Inactive
                }, commandType: CommandType.StoredProcedure);
                var newResult = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status200OK,
                    Data = result
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    DevMsg = ex.Message
                };
                return exception;
            }
            finally { conn.Close(); }
        }
        /// <summary>
        ///  Thay đổi trạng thái nhiều danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<BaseEntity> UpdateMultipleStatus(UpdateMultipleEmulationTitleStatusDto model)
        {
            using var conn = this.GetConnection();
            try
            {
                conn.Open();
                var query = "proc_emulationtitle_update_multiple";
                var stringInt = String.Join(",", model.Id);
                var result = await conn.QueryFirstAsync<int>(query, new
                {
                    int_array = stringInt,
                    Inactive = model.Inactive
                }, commandType: CommandType.StoredProcedure);
                var newResult = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status200OK,
                    Data = result
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    DevMsg = ex.Message
                };
                return exception;
            }
            finally { conn.Close(); }
        }
        #endregion

    }
}
