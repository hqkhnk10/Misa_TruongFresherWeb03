﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using MySqlConnector;
using System.Data;

namespace Misa_TruongWeb03.DL.Repository.EmulationTitle
{
    public class EmulationTitleRepository : IEmulationTitleRepository
    {
        private readonly IConfiguration _configuration;
        public EmulationTitleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_configuration.GetSection("ConnectionString").Value);
        }
        /// <summary>
        /// Lấy danh sách danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseEntity> Get(GetEmulationTitle model)
        {
            using var conn = this.GetConnection();
            try
            {
                conn.Open();
                var query = "proc_emulationtitle_get";
                //var parameters = new DynamicParameters();
                //parameters.Add("@pageSize", model.pageSize, System.Data.DbType.Int64);
                //parameters.Add("@pageIndex", model.pageSize, System.Data.DbType.Int64);
                //parameters.Add("@keyword", model.pageSize, System.Data.DbType.String);
                //parameters.Add("@ApplyObject", model.pageSize, System.Data.DbType.Int64);
                //parameters.Add("@CommendationLevel", model.pageSize, System.Data.DbType.Int64);
                //parameters.Add("@MovementType", model.pageSize, System.Data.DbType.Int64);
                //parameters.Add("@Inactive", model.pageSize, System.Data.DbType.Int64);

                var result = await conn.QueryAsync<EmulationTitleModel>(query, new
                {
                    model.pageSize,
                    model.pageIndex,
                    model.keyword,
                    model.ApplyObject,
                    model.CommendationLevel,
                    model.MovementType,
                    model.Inactive,
                    model.ApplyObjectSort,
                    model.CommendationLevelSort,
                    model.MovementTypeSort,
                    model.InactiveSort
                }, commandType: CommandType.StoredProcedure);
                var listModel = result.AsList();
                var newResult = new BaseEntity
                {
                    Data = listModel,
                    Count = listModel.Count > 0 ? listModel[0].Count : 0,
                    Pagination = new Pagination() { PageIndex = model.pageIndex, PageSize = model.pageSize, Count = listModel.Count > 0 ? listModel[0].Count : 0 }
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    Message = ex.Message
                };
                return exception;
            }
            finally { conn.Close(); }
        }
        /// <summary>
        /// Lấy chi tiết danh hiệu thi đua
        /// </summary>
        /// <param name="id">Id của danh hiệu thi đua</param>
        /// <returns></returns>
        public async Task<BaseEntity> GetDetail(int id)
        {
            using var conn = this.GetConnection();
            try
            {
                conn.Open();
                var query = "proc_emulationtitle_getdetail";

                var result = await conn.QueryFirstOrDefaultAsync<EmulationTitleModel>(query, new { id }, commandType: CommandType.StoredProcedure);
                if (result == null)
                {
                    var notFound = new BaseEntity
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Data = null,
                        Message = "NOTFOUND"
                    };
                    return notFound;
                }
                var newResult = new BaseEntity
                {
                    Data = result
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    Message = ex.Message
                };
                return exception;
            }
            finally { conn.Close(); }
        }
        /// <summary>
        /// Thêm danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseEntity> Post(PostEmulationTitle model)
        {
            using var conn = this.GetConnection();
            try
            {
                conn.Open();
                var query = "proc_emulationtitle_insert";

                var result = await conn.ExecuteAsync(query, new
                {
                    model.ApplyObject,
                    model.CommendationLevel,
                    model.EmulationTitleName,
                    model.EmulationTitleCode,
                    model.MovementType,
                    model.Inactive,
                    model.CreatedBy
                }, commandType: CommandType.StoredProcedure);
                if (result == 0)
                {
                    var notFound = new BaseEntity
                    {
                        Count = result,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Data = null,
                        Message = "Error"
                    };
                    return notFound;
                }
                var newResult = new BaseEntity
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    Message = ex.Message
                };
                return exception;
            }
            finally { conn.Close(); }
        }
        /// <summary>
        /// Sửa danh hiệu thi đua
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseEntity> Put(int id, PostEmulationTitle model)
        {
            using var conn = this.GetConnection();
            try
            {
                conn.Open();
                var query = "proc_emulationtitle_update";

                var result = await conn.ExecuteAsync(query, new
                {
                    Id = id,
                    model.ApplyObject,
                    model.CommendationLevel,
                    model.EmulationTitleName,
                    model.EmulationTitleCode,
                    model.MovementType,
                    model.Inactive,
                    updatedBy = model.CreatedBy
                }, commandType: CommandType.StoredProcedure);
                if (result == 0)
                {
                    var notFound = new BaseEntity
                    {
                        Count = result,
                        StatusCode = 400,
                        Data = null,
                        Message = "Error"
                    };
                    return notFound;
                }
                var newResult = new BaseEntity
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = result
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    Message = ex.Message
                };
                return exception;
            }
            finally { conn.Close(); }
        }
        /// <summary>
        /// Xóa danh hiệu thi đua
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BaseEntity> Delete(int id)
        {
            using var conn = this.GetConnection();
            try
            {
                conn.Open();
                var query = "proc_emulationtitle_delete";

                var result = await conn.ExecuteAsync(query, new
                {
                    Id = id,
                }, commandType: CommandType.StoredProcedure);
                if (result == 0)
                {
                    var notFound = new BaseEntity
                    {
                        Count = result,
                        StatusCode = StatusCodes.Status404NotFound,
                        Data = null,
                        Message = "Error"
                    };
                    return notFound;
                }
                var newResult = new BaseEntity
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = result
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    Message = ex.Message
                };
                return exception;
            }
            finally { conn.Close(); }
        }
        /// <summary>
        /// Xóa nhiều danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                        Count = result,
                        StatusCode = 400,
                        Data = null,
                        Message = "Error"
                    };
                    return notFound;
                }
                var newResult = new BaseEntity
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = result
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    Message = ex.Message
                };
                return exception;
            }
            finally { conn.Close(); }
        }
        /// <summary>
        /// Kiểm tra trùng mã danh hiệu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseEntity> CheckDuplicate(EmulationTitleModel model)
        {
            using var conn = this.GetConnection();
            try
            {
                conn.Open();
                var query = "proc_emulationtitle_checkDuplicate";

                var result = await conn.QueryFirstOrDefaultAsync<int>(query, new
                {
                    Id = model.EmulationTitleID,
                    Code = model.EmulationTitleCode,
                }, commandType: CommandType.StoredProcedure);
                if (result > 0)
                {
                    var found = new BaseEntity
                    {
                        StatusCode = StatusCodes.Status302Found,
                        Data = null,
                        Message = "Trùng mã danh hiệu"
                    };
                    return found;
                }
                var newResult = new BaseEntity
                {
                    StatusCode = StatusCodes.Status200OK,
                    Data = result
                };
                return newResult;
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    Message = ex.Message
                };
                return exception;
            }
            finally { conn.Close(); }
        }

    }
}
