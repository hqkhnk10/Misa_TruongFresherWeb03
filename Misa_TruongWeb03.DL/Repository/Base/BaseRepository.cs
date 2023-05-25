using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Common.Helper;
using Misa_TruongWeb03.Common.Resource;
using MySqlConnector;
using System.Data;
using System.Data.Common;
using static Dapper.SqlMapper;

namespace Misa_TruongWeb03.DL.Repository.Base
{
    /// <summary>
    /// Base Repo kết nối tới database, gọi store procedure
    /// </summary>
    /// <typeparam name="T">Generic Entity model</typeparam>
    /// <typeparam name="TGetDTO">Generic Get DTO model</typeparam>
    /// <typeparam name="TPostDTO">Generic Post DTO model</typeparam>
    /// <typeparam name="TPutDTO">Generic Put DTO model</typeparam>
    /// CreatedBy: QTNgo (24/05/2023)
    public abstract class BaseRepository<T, TGetDTO, TPostDTO, TPutDTO> : IBaseRepository<T, TGetDTO, TPostDTO, TPutDTO>
    {
        #region Property
        protected readonly IConfiguration _configuration;
        #endregion
        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
        #region Method
        /// <summary>
        /// Tạo connection đến MySQL
        /// </summary>
        /// <returns>Base Entity</returns>
        /// CreatedBy: QTNgo (24/05/2023)
        public DbConnection GetConnection()
        {
            return new MySqlConnection(_configuration.GetSection("ConnectionString").Value);
        }
        /// <summary>
        /// Base GET 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: QTNgo (24/05/2023)
        public async Task<BaseEntity> Get(TGetDTO model)
        {
            using var connection = GetConnection();
            try
            {
                var parameters = ParameterObjectBuilder.CreateParameterObject(model);
                var storedProcedureName = $"proc_{typeof(T).Name.ToLower()}_get";
                connection.Open();
                var result = await connection.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                var listModel = result.AsList();
                var newResult = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status200OK,
                    Data = listModel,
                    Pagination = new Pagination() { PageIndex = PropertyAccess.GetPropertyValue(model, "pageIndex"), PageSize = PropertyAccess.GetPropertyValue(model, "pageSize"), Count = listModel.Count > 0 ? PropertyAccess.GetPropertyValue(listModel[0], "Count") : 0 }
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    DevMsg = ex.Message,
                    UserMsg = VN.Error500
                };
                return exception;
            }
            finally { connection.Close(); }
        }
        /// <summary>
        /// Base GET by Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: QTNgo (24/05/2023)
        public async Task<BaseEntity> GetById(int id)
        {
            var storedProcedureName = $"proc_{typeof(T).Name.ToLower()}_getdetail";
            DynamicParameters parameters = new DynamicParameters();
            parameters.AddDynamicParams(new { id });
            using var connection = GetConnection();
            try
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                var newResult = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status200OK,
                    Data = result,
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    DevMsg = ex.Message,
                    UserMsg = VN.Error500
                };
                return exception;
            }
            finally { connection.Close(); }
        }
        /// <summary>
        /// Base POST 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: QTNgo (24/05/2023)
        public async Task<BaseEntity> Post(TPostDTO model)
        {
            using var connection = GetConnection();
            try
            {
                var parameters = ParameterObjectBuilder.CreateParameterObject(model);
                var storedProcedureName = $"proc_{typeof(T).Name.ToLower()}_insert";
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<int>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                var newResult = new BaseEntity
                {
                    Data = result,
                    ErrorCode = StatusCodes.Status200OK,
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    DevMsg = ex.Message,
                    UserMsg = VN.Error500
                };
                return exception;
            }
            finally { connection.Close(); }
        }
        /// <summary>
        /// Base PUT 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: QTNgo (24/05/2023)
        public async Task<BaseEntity> Put(TPutDTO model)
        {
            using var connection = GetConnection();
            try
            {
                var parameters = ParameterObjectBuilder.CreateParameterObject(model);
                var storedProcedureName = $"proc_{typeof(T).Name.ToLower()}_update";
                connection.Open();
                var result = await connection.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                var listModel = result.AsList();
                var newResult = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status200OK,
                    Data = listModel,
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    DevMsg = ex.Message,
                    UserMsg = VN.Error500
                };
                return exception;
            }
            finally { connection.Close(); }
        }
        /// <summary>
        /// Base DELETE 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: QTNgo (24/05/2023)
        public async Task<BaseEntity> Delete(int id)
        {
            var storedProcedureName = $"proc_{typeof(T).Name.ToLower()}_delete";
            DynamicParameters parameters = new DynamicParameters();
            parameters.AddDynamicParams(new { id });
            using var connection = GetConnection();
            try
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                var newResult = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status200OK,
                    Data = result,
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
            finally { connection.Close(); }
        }
        /// <summary>
        /// Base Check duplicate 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: QTNgo (24/05/2023)
        public async Task<BaseEntity> CheckDuplicate(T model)
        {
            using var connection = GetConnection();
            try
            {
                var parameters = ParameterObjectBuilder.CreateParameterObject(model);
                var storedProcedureName = $"proc_{typeof(T).Name.ToLower()}_checkDuplicate";
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<int>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                if (result > 0)
                {
                    var found = new BaseEntity
                    {
                        ErrorCode = StatusCodes.Status302Found,
                        Data = true,
                        DevMsg = VN.DuplicateError,
                        UserMsg = VN.DuplicateError
                    };
                    return found;
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
                    DevMsg = ex.Message,
                    UserMsg = VN.Error500
                };
                return exception;
            }
            finally { connection.Close(); }
        } 
        #endregion
    }
}
