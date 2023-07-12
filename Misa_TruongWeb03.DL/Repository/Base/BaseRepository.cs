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
        protected readonly DbConnection con;
        #endregion
        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new MySqlConnection(_configuration.GetSection("ConnectionString").Value);
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
            await connection.OpenAsync();
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
            await connection.CloseAsync();
            return returnModel;
        }
        /// <summary>
        /// Base GET by Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<TEntity?> GetById(Guid id)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            var storedProcedureName = GenerateProcName.Generate<TEntity>("GetById");
            var parameters = new DynamicParameters();
            parameters.AddDynamicParams(new { id });
            await connection.CloseAsync();

            return await connection.QueryFirstOrDefaultAsync<TEntity>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);

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
            await connection.OpenAsync();
            var storedProcedureName = GenerateProcName.Generate<TEntity>("Post");
            Guid newGuid = Guid.NewGuid();
            var parameters = DynamicParametersAdd.CreateParameterDynamic(model);
            parameters.Add("Id", newGuid);
            var result = await connection.QueryFirstOrDefaultAsync<string>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return newGuid;
        }

        public DbConnection OpenConnection()
        {
            this.con.Open();
            return this.con;
        }
        public void CloseConnection()
        {
            this.con.Close();
            this.con.Dispose();
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
            await connection.OpenAsync();
            var storedProcedureName = GenerateProcName.Generate<TEntity>("Put");
            var parameters = DynamicParametersAdd.CreateParameterDynamic(model);
            parameters.AddDynamicParams(new { id });
            await connection.CloseAsync();
            return await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);

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
            await connection.OpenAsync();
            var storedProcedureName = GenerateProcName.Generate<TEntity>("Delete");
            DynamicParameters parameters = new DynamicParameters();
            parameters.AddDynamicParams(new { id });
            await connection.CloseAsync();
            return await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
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
            await connection.OpenAsync();

            var parameters = ParameterObjectBuilder.CreateParameterObject(model);
            var storedProcedureName = $"proc_{typeof(TEntity).Name.ToLower()}_checkDuplicate";

            await connection.CloseAsync();
            return await connection.QueryFirstOrDefaultAsync<bool>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);

        }
        #endregion
    }
}

