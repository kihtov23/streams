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

        [HttpGet]
        [Route("/helloWorld")]
        public async Task<IActionResult> Get()
        {
            return Ok("Hello world!");
        }

        [HttpGet]
        [Route("/GetBigArticle")]
        public async Task<IActionResult> GetBigArticle()
        {
            var article = await GetArticle();
            return Ok(article);
        }

        private async Task<Article> GetArticle()
        {
            return await _dbContext.Articles.FirstAsync(a => a.Name == "89000 elements, 5 MB");
        }

        [HttpGet]
        [Route("/GetPosts")]
        public IAsyncEnumerable<Post> GetPostsFromDb()
        {
            var posts = GetPostsQueryable();
            return posts;
        }

        private IAsyncEnumerable<Post> GetPostsQueryable()
        {
            return _dbContext.Posts.Where(a => a.Name == "8900 elements").AsAsyncEnumerable();
        }

        [HttpGet]
        [Route("/GetPostsFromCode")]
        public IAsyncEnumerable<int> GetPostsFromCode()
        {
            return GetPosts();
        }

        private async IAsyncEnumerable<int> GetPosts()
        {
            for (int i = 1; i <= 5; i++)
            {
                await Task.Delay(500);
                yield return i;
            }
        }
    }
}
