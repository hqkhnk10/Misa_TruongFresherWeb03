using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FresherWeb03.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var connectionString = "Server=localhost;Port=3306;Database=misa_fresherweb03;Uid=root;Pwd=Conecho123;\r\n";
            var mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("id", id);
            mySqlConnection.Query("SELECT * FROM emulationtitle");
            return "value";
        }

        //// POST api/<EmployeeController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<EmployeeController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EmployeeController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
