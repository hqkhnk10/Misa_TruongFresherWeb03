using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Common.Helper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Dapper.SqlMapper;

namespace Misa_TruongWeb03.DL.Repository.Base
{
    public abstract class BaseRepository<T, TGetDTO, TPostDTO, TPutDTO> : IBaseRepository<T, TGetDTO, TPostDTO, TPutDTO>
    {
        protected readonly IConfiguration _configuration;
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbConnection GetConnection()
        {
            return new MySqlConnection(_configuration.GetSection("ConnectionString").Value);
        }
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
                    UserMsg = "Lỗi hệ thống"
                };
                return exception;
            }
            finally { connection.Close(); }
        }
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
                    UserMsg = "Lỗi hệ thống"
                };
                return exception;
            }
            finally { connection.Close(); }
        }
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
                    UserMsg = "Lỗi hệ thống"
                };
                return exception;
            }
            finally { connection.Close(); }
        }
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
                    UserMsg = "Lỗi hệ thống"
                };
                return exception;
            }
            finally { connection.Close(); }
        }
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
                        Data = null,
                        DevMsg = "Trùng mã danh hiệu",
                        UserMsg = "Trùng mã danh hiệu"
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
                    UserMsg = "Lỗi hệ thống"
                };
                return exception;
            }
            finally { connection.Close(); }
        }
    }
}
