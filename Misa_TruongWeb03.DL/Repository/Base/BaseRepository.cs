using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Helper;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Entity.Base;
using Misa_TruongWeb03.DL.Repository.UnitOfWorkk;
using MySqlConnector;
using System.Collections.Generic;
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
        protected readonly IUnitOfWork _unitOfWork;
        protected IDbConnection Connection => _unitOfWork.Connection;
        protected IDbTransaction Transaction => _unitOfWork.Transaction;
        #endregion
        #region Constructor
        public BaseRepository(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        #endregion
        #region Method
        /// Base GET 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<(IEnumerable<TEntity>, int)> Get(Dictionary<string, object> dictionary, FilterModel filter, string? sort)
        {

            var tableName = typeof(TEntity).Name;
            var query = $"SELECT * FROM {tableName} WHERE";
            var where = " 1=1";
            var parameters = new DynamicParameters();

            if (dictionary != null && dictionary.Any())
            {
                foreach (var dict in dictionary)
                {
                    var columnName = dict.Key;
                    var dictValue = dict.Value;
                    if (dictValue != null)
                    {
                        where += $" AND {columnName} = @{columnName}";
                        parameters.Add(columnName, dictValue);
                    }
                }
            }
            query += where;
            // Retrieve total count before applying pagination
            var countQuery = $"SELECT COUNT(*) FROM {tableName} WHERE";
            countQuery += where;
            var totalCount = await Connection.ExecuteScalarAsync<int>(countQuery, parameters);

            var offset = (filter.PageIndex - 1) * filter.PageSize;
            query += " LIMIT @Offset, @PageSize";
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", filter.PageSize);

            var entity = await Connection.QueryAsync<TEntity>(query, parameters);
            return (entity, totalCount);



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

            return await Connection.QueryFirstOrDefaultAsync<TEntity>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: Transaction);


        }
        /// <summary>
        /// Base POST 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<Guid> Post(TEntity model)
        {
            var storedProcedureName = GenerateProcName.Generate<TEntity>("Post");
            Guid newGuid = Guid.NewGuid();
            var parameters = DynamicParametersAdd.CreateParameterDynamic(model);
            parameters.Add("Id", newGuid);
            var result = await Connection.QueryFirstOrDefaultAsync<string>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: Transaction);
            return newGuid;
        }
        /// <summary>
        /// Base PUT 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<int> Put(Guid id, TEntity model)
        {

            var storedProcedureName = GenerateProcName.Generate<TEntity>("Put");
            var parameters = DynamicParametersAdd.CreateParameterDynamic(model);
            parameters.AddDynamicParams(new { id });
            return await Connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: Transaction);


        }
        /// <summary>
        /// Base DELETE 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<int> Delete(Guid id)
        {
            var storedProcedureName = GenerateProcName.Generate<TEntity>("Delete");
            DynamicParameters parameters = new DynamicParameters();
            parameters.AddDynamicParams(new { id });
            return await Connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: Transaction);

        }
        /// <summary>
        /// Base Check duplicate 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Base Entity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<bool> CheckDuplicate(TEntity model)
        {
            var parameters = ParameterObjectBuilder.CreateParameterObject(model);
            var storedProcedureName = $"proc_{typeof(TEntity).Name.ToLower()}_checkDuplicate";
            return await Connection.QueryFirstOrDefaultAsync<bool>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: Transaction);


        }
        #endregion
    }
}

