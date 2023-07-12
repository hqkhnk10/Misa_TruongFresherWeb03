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
        public async Task<(IEnumerable<TEntity>, int totalCount)> Get(Dictionary<string, object> dictionary, FilterModel filters)
        {
            using var connection = GetConnection();

            connection.Open();
            var storedProcedureName = GenerateProcName.Generate<TEntity>("Get");

            var parameters = new DynamicParameters();
            var whereClause = "";
            if (dictionary != null && dictionary.Any())
            {
                foreach (var dict in dictionary)
                {
                    var columnName = dict.Key;
                    var filterValue = dict.Value;

                    whereClause += $" AND {columnName} = @{columnName}";
                    parameters.Add(columnName, filterValue);
                }
            }

            var result = await connection.QueryMultipleAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            var listModel = await result.ReadAsync<TEntity>();
            var totalCount = await result.ReadSingleAsync<int>();

            connection.Close();
            return (listModel, totalCount);
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
     
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<TEntity>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            connection.Close(); 
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
                Guid newGuid = Guid.NewGuid();
                var parameters = DynamicParametersAdd.CreateParameterDynamic(model);
                parameters.Add("Id", newGuid);
                var result = await connection.QueryFirstOrDefaultAsync<string>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                return newGuid;
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

                return await connection.QueryFirstOrDefaultAsync<bool>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);

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
