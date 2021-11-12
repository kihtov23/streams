using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        string connectionString =
            "Data Source=(local);Initial Catalog=Test;Integrated Security=true";

        private string queryString = "SELECT BigString from dbo.BigStrings";


        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello world!");
        }

        private async Task GetBigStrings()
        {
            await using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(queryString, connection);

            connection.Open();
            await using SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess);
            while (await reader.ReadAsync())
            {
                using TextReader data = reader.GetTextReader("BigString");
            }
        }
    }
}
