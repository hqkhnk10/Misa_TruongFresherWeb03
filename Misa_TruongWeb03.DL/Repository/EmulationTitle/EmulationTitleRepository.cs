using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Repository.Base;
using Newtonsoft.Json.Linq;
using System.Data;
using static Dapper.SqlMapper;

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
        public async Task<BaseEntity> InsertMultiple(IEnumerable<PostEmulationTitle> models)
        {
            using var connection = this.GetConnection();
            try
            {
                connection.Open();
                // Generate the SQL query for the multiple insert
                var query = "INSERT INTO emulationtitle (EmulationTitleName, EmulationTitleCode,EmulationTitleNote, ApplyObject, CommendationLevelId, MovementType, Inactive, CreatedAt, CreatedBy, ModifiedAt, ModifiedBy)" +
                    " VALUES (@EmulationTitleName, @EmulationTitleCode, @EmulationTitleNote,@ApplyObject, @CommendationLevel, @MovementType, 0, NOW(), @CreatedBy, NOW(), @CreatedBy);";
                // Execute the insert for each entity in the collection
                foreach (var model in models)
                {
                    var parameters = new DynamicParameters(model);

                    // Execute the query with the dynamic parameters
                    await connection.ExecuteAsync(query, parameters);
                }
                return new BaseEntity
                {
                    ErrorCode = 200
                };
            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }
        }
        public async Task<BaseEntity> CheckDuplicateMultiple(IEnumerable<string> models)
        {
            using var connection = this.GetConnection();
            try
            {
                connection.Open();
                // Generate the SQL query to check for duplicates
                var query = "SELECT EmulationTitleCode FROM emulationtitle WHERE EmulationTitleCode IN @Values GROUP BY EmulationTitleCode HAVING COUNT(*) > 0";

                // Execute the query with the list of values as a parameter
                var duplicates = await connection.QueryAsync<string>(query, new { Values = models });
                // If the count is greater than 0, duplicates exist
                var errorCode = StatusCodes.Status200OK;
                if(duplicates.Count() > 0)
                {
                    errorCode = StatusCodes.Status302Found;
                }
                return new BaseEntity
                {
                    ErrorCode = errorCode,
                    Data = duplicates
                };
            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }
        }
        public async Task<bool> CheckDuplicateCode(string code)
        {
            using var connection = this.GetConnection();
            try
            {
                connection.Open();
                // Generate the SQL query to check for duplicates
                var query = "SELECT 1 FROM emulationtitle WHERE EmulationTitleCode = @Code";

                // Execute the query with the list of values as a parameter
                var duplicate = await connection.QueryFirstOrDefaultAsync<int?>(query, new { Code = code });
                // If the count is greater than 0, duplicates exist
                if(duplicate == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
            finally { connection.Close(); }
        }

        #endregion

    }
}
