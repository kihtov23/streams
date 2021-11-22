using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Utils;
using WebAPI.Posts;


namespace WebApiNet3.Controllers
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
        [Route("/")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello world!");
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
            return await _dbContext.Articles.FirstAsync(a => a.Name == Constants.FileNames.With_8900_Elements_5MB);
        }

        #endregion

        #region UseCase - Get numbers IAsyncEnumerable

        [HttpGet]
        [Route("/numbers")]
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
            // MaxIAsyncEnumerableBufferLimit should be updated. (Defaults to 8192) 
            var posts = GetPostsQueryable();
            return posts;
        }

        private IAsyncEnumerable<Post> GetPostsQueryable()
        {
            return _dbContext.Posts.AsNoTracking().Where(a => a.Name == Constants.FileNames.With_1_Element_1KB).AsAsyncEnumerable();
        }

        #endregion

        #region UseCase - Stream as a file

        [HttpGet]
        [Route("/fileStream")]
        public IActionResult GetFileAsStream()
        {
            StreamReader file = System.IO.File.OpenText($"./{Constants.FileNames.With_8900_Elements_5MB}.txt");
            return File(file.BaseStream, "application/octet-stream");
        }

        #endregion

        #region UseCase - GetPosts as as part of single model

        [HttpGet]
        [Route("/GetPostsAsSingleModel")]
        public IActionResult GetPostsAsSingleModel()
        {
            // MaxIAsyncEnumerableBufferLimit should be updated. (Defaults to 8192) 
            var posts = GetPostsQueryable();
            return Ok(posts);
        }
      

        #endregion
    }
}
