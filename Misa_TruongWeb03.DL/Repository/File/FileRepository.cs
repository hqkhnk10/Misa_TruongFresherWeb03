using Dapper;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Common.Entity.FileEntity;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data.Common;

namespace Misa_TruongWeb03.DL.Repository.FileRepository
{
    public class FileRepository : IFileRepository
    {
        #region Property
        private readonly IConfiguration _configuration;
        #endregion
        #region Constructor
        public FileRepository(IConfiguration configuration)
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
        public async Task<List<ExcelConfigEntity>> MappingConfig(string key)
        {
            using var connection = this.GetConnection();
            try
            {
                // Fetch columnMapping from database
                    connection.Open();
                    var query = $"SELECT * FROM ExcelMapping where TableKey = '{key}'";
                    var res = await connection.QueryAsync<ExcelConfigEntity>(query);
                    return res.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
