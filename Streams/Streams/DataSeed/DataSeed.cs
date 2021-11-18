using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Utils;

namespace DataSeedNet3
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

            // Seed Article data with big string
            await SeedOneArticle(Constants.FileNames.With_8900_Elements_5MB);
            await SeedOneArticle(Constants.FileNames.With_89000_Elements_50MB);

            //Seed a lot of Posts data with small strings
            await SeedPosts(Constants.FileNames.With_1_Element_1KB);
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

        public static async Task SeedOneArticle(string fileName)
        {
            await using SqlConnection conn = new SqlConnection(Constants.ConnectionString);
            await conn.OpenAsync();

            await using SqlCommand cmd = new SqlCommand($"INSERT INTO [Articles] (BigString, Name) VALUES (@textdata, '{fileName}')", conn);
            using StreamReader file = File.OpenText($"./DataSeed/{fileName}.txt");
            cmd.Parameters.Add("@textdata", SqlDbType.NVarChar, -1).Value = file;

            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task SeedPosts(string filename)
        {
            await using SqlConnection conn = new SqlConnection(Constants.ConnectionString);
            await conn.OpenAsync();

            await using SqlCommand cmd = new SqlCommand($"INSERT INTO [Posts] (PostString, Name) VALUES (@textdata, '{filename}')", conn);
            using StreamReader file = File.OpenText($"./DataSeed/{filename}.txt");

            var str = await file.ReadToEndAsync();
            cmd.Parameters.Add("@textdata", SqlDbType.NVarChar).Value = str;
            
            for (var i = 0; i < 8900; i++)
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
