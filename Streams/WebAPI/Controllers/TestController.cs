using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ArticleDbContext _dbContext;

        public TestController(ArticleDbContext context)
        {
            _dbContext = context;
        }

        #region UseCase - BigArticleAsSingleModel

        [HttpGet]
        [Route("/bigArticleAsSingleModel")]
        public async Task<IActionResult> GetBigArticleAsSingleModel()
        {
            var article = await GetArticle();
            return Ok(article);
        }

        private async Task<Article> GetArticle()
        {
            return await _dbContext.Articles.FirstAsync(a => a.Name == "89000 elements, 5 MB");
        }

        #endregion

        #region UseCase - Get numbers IAsyncEnumerable

        [HttpGet]
        [Route("/Numbers")]
        public IAsyncEnumerable<int> GetNumbers()
        {
            return GetNumbersAsync();
        }

        private async IAsyncEnumerable<int> GetNumbersAsync()
        {
            for (int i = 1; i <= 5; i++)
            {
                await Task.Delay(500);
                yield return i;
            }
        }

        #endregion

        #region UseCase - GetPosts as IAsyncEnumerable 

        [HttpGet]
        [Route("/GetPosts")]
        public IAsyncEnumerable<Post> GetPostsFromDb()
        {
            var posts = GetPostsQueryable();
            return posts;
        }

        private IAsyncEnumerable<Post> GetPostsQueryable()
        {
            return _dbContext.Posts.AsNoTracking().Where(a => a.Name == "8900 elements").AsAsyncEnumerable();
        }

        #endregion

        #region UseCase - Stream as a file

        [HttpGet]
        [Route("/fileStream")]
        public async Task<IActionResult> GetFileAsStream()
        {
            StreamReader file = System.IO.File.OpenText("./89000elements.txt");
            return File(file.BaseStream, "application/octet-stream");
        }

        #endregion

        #region UseCase - Stream string from DB as file (ADO.NET)

        [HttpGet]
        [Route("/streamStringFromDbAsFile")]
        public async Task<IActionResult> GetFileAsStreamFromDb()
        {
            var bigString = await GetStreamFromDb();
            return File(bigString, "application/octet-stream");

        }

        private async Task<Stream> GetStreamFromDb()
        {
            await using SqlConnection conn = new SqlConnection(Constants.ConnectionString);
            await conn.OpenAsync();
            await using SqlCommand cmd = new SqlCommand("SELECT BigString  FROM [Test].[dbo].[Articles] where Name = '89000 elements, 5 MB'", conn);
            await using SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess);
            await reader.ReadAsync();

            using TextReader bigString = reader.GetTextReader("BigString");

            bigString.ReadAsync()


            return ((StreamReader)bigString).BaseStream;
        }

        #endregion
    }
}
