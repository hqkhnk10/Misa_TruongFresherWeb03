//using Dapper;
//using FresherWeb03.Entity;
//using Microsoft.AspNetCore.Mvc;
//using Misa_TruongWeb03.DTO;
//using Misa_TruongWeb03.Entity;
//using MySqlConnector;
//using System.Data;

//namespace FresherWeb03.Controller
//{
//    [Route("api/v1/[controller]")]
//    [ApiController]
//    public class EmulationCommendationController : ControllerBase
//    {
//        private readonly string _connectionString;
//        public EmulationCommendationController(IConfiguration configuration)
//        {
//            _connectionString = configuration.GetSection("ConnectionString").Value;
//        }

//        // GET 
//        /// <summary>
//        /// Lấy danh sách cấp phong trào
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public async Task<BaseEntity> Get()
//        {
//            using var mySqlConnection = new MySqlConnection(_connectionString);
//            try
//            {
//                mySqlConnection.Open();
//                var query = "select id as Value, Name as Label from emulationcommendation";

//                var result = await mySqlConnection.QueryAsync<EmulationCommendation>(query);

//                var listModel = result.AsList();
//                var newResult = new BaseEntity
//                {
//                    Data = listModel,
//                    Count = listModel.Count,
//                };
//                return newResult;
//            }
//            catch (Exception ex)
//            {
//                var exception = new BaseEntity
//                {
//                    StatusCode = StatusCodes.Status500InternalServerError,
//                    Data = null,
//                    Message = ex.Message
//                };
//                return exception;
//            }
//            finally { mySqlConnection.Close(); }
//        }

//    }
//}
