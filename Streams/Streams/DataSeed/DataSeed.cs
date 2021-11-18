using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace DataSeed
{
    /// <summary>
    /// Insert json to DB from files.
    /// Important note: when file is inserted into DB it has 2 times bigger size in SQL server
    /// </summary>
    public class DataSeedHelper
    {
        public static async Task Seed()
        {
            await ClearDb();

            await Stream50KBFileToServer();
            await Stream5MBfileToServer();

            await Stream8900PostsToDb();
        }

        public static async Task ClearDb()
        {
            await using SqlConnection conn = new SqlConnection(Constants.ConnectionString);
            await conn.OpenAsync();
            await using SqlCommand cmd = new SqlCommand("delete from Articles", conn);
            await cmd.ExecuteNonQueryAsync();

            await using SqlCommand cmd2 = new SqlCommand("delete from Posts", conn);
            await cmd2.ExecuteNonQueryAsync();
        }

        public static async Task Stream50KBFileToServer()
        {
            await using SqlConnection conn = new SqlConnection(Constants.ConnectionString);
            await conn.OpenAsync();

            await using SqlCommand cmd = new SqlCommand("INSERT INTO [Articles] (BigString, Name) VALUES (@textdata, '89 elements, 50 KB')", conn);

            using StreamReader file = File.OpenText("./DataSeed/89elements.txt");
            // Add a parameter which uses the StreamReader we just opened
            // Size is set to -1 to indicate "MAX"
            cmd.Parameters.Add("@textdata", SqlDbType.NVarChar, -1).Value = file;
            // Send the data to the server asynchronously
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task Stream5MBfileToServer()
        {
            await using SqlConnection conn = new SqlConnection(Constants.ConnectionString);
            await conn.OpenAsync();

            await using SqlCommand cmd = new SqlCommand("INSERT INTO [Articles] (BigString, Name) VALUES (@textdata, '89000 elements, 5 MB')", conn);
            using StreamReader file = File.OpenText("./DataSeed/8900elements.txt");
            // Add a parameter which uses the StreamReader we just opened
            // Size is set to -1 to indicate "MAX"
            cmd.Parameters.Add("@textdata", SqlDbType.NVarChar, -1).Value = file;
            // Send the data to the server asynchronously
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task Stream8900PostsToDb()
        {
            await using SqlConnection conn = new SqlConnection(Constants.ConnectionString);
            await conn.OpenAsync();

            await using SqlCommand cmd = new SqlCommand("INSERT INTO [Posts] (PostString, Name) VALUES (@textdata, '8900 elements')", conn);
            using StreamReader file = File.OpenText("./DataSeed/1element.txt");
            // Add a parameter which uses the StreamReader we just opened
            // Size is set to -1 to indicate "MAX"

            var str = await file.ReadToEndAsync();
            cmd.Parameters.Add("@textdata", SqlDbType.NVarChar).Value = str;
            // Send the data to the server asynchronously
            for (var i = 0; i < 8000; i++)
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
