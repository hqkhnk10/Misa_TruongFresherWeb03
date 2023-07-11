using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Helper;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Entity.Base;
using MySqlConnector;
using System.Data;
using System.Data.Common;
using static Dapper.SqlMapper;

namespace Misa_TruongWeb03.DL.Repository.Base
{
    /// <summary>
    /// Base Repo kết nối tới database, gọi store procedure
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity model</typeparam>
    /// CreatedBy: NQTruong (24/05/2023)
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
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
        /// CreatedBy: NQTruong (24/05/2023)
        public DbConnection GetConnection()
        {
            return new MySqlConnection(_configuration.GetSection("ConnectionString").Value);
        }
        /// <summary>
        /// Base GET 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<BaseGet<IEnumerable<TEntity>>> Get(TEntity model, FilterModel getModel)
        {
            using var connection = GetConnection();
            try
            {
                connection.Open();
                var storedProcedureName = GenerateProcName.Generate<TEntity>("Get");

                var parameters = DynamicParametersAdd.CreateParameterDynamic(model);
                foreach (var property in typeof(FilterModel).GetProperties())
                {
                    var value = property.GetValue(getModel);
                    parameters.Add(property.Name, value);
                }

                var result = await connection.QueryMultipleAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                var listModel = await result.ReadAsync<TEntity>();
                var totalCount = await result.ReadSingleAsync<int>();

                var returnModel = new BaseGet<IEnumerable<TEntity>>
                {
                    Data = listModel,
                    Pagination = new Pagination
                    {
                        Count = totalCount,
                        PageIndex = getModel.PageIndex,
                        PageSize = getModel.PageSize,
                    }
                };
                return returnModel;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { connection.Close(); }
        }
        /// <summary>
        /// Base GET by Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<TEntity?> GetById(Guid id)
        {
            var storedProcedureName = GenerateProcName.Generate<TEntity>("GetById");

            var parameters = new DynamicParameters();
            parameters.AddDynamicParams(new { id });

            using var connection = GetConnection();
            try
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<TEntity>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { connection.Close(); }
        }
        /// <summary>
        /// Base POST 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<Guid> Post(TEntity model)
        {
            using var connection = GetConnection();
            try
            {
                connection.Open();

                var storedProcedureName = GenerateProcName.Generate<TEntity>("Post");

                var parameters = DynamicParametersAdd.CreateParameterDynamic(model);

                return await connection.QueryFirstOrDefaultAsync<Guid>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { connection.Close(); }
        }
        /// <summary>
        /// Base PUT 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<int> Put(Guid id, TEntity model)
        {
            using var connection = GetConnection();
            try
            {
                connection.Open();

                var storedProcedureName = GenerateProcName.Generate<TEntity>("Put");

                var parameters = DynamicParametersAdd.CreateParameterDynamic(model);
                parameters.AddDynamicParams(new { id });

                return await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { connection.Close(); }
        }
        /// <summary>
        /// Base DELETE 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<int> Delete(Guid id)
        {

            using var connection = GetConnection();
            try
            {
                connection.Open();

                var storedProcedureName = GenerateProcName.Generate<TEntity>("Delete");

                DynamicParameters parameters = new DynamicParameters();
                parameters.AddDynamicParams(new { id });
                
                return await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { connection.Close(); }
        }
        /// <summary>
        /// Base Check duplicate 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<bool> CheckDuplicate(TEntity model)
        {
            using var connection = GetConnection();
            try
            {
                connection.Open();

                var parameters = ParameterObjectBuilder.CreateParameterObject(model);
                var storedProcedureName = $"proc_{typeof(TEntity).Name.ToLower()}_checkDuplicate";

                return  await connection.QueryFirstOrDefaultAsync<bool>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { connection.Close(); }
        } 
        #endregion
    }
}
