using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Utils;
using WebAPI.Posts;

namespace WebApiNet6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ArticleDbContext _dbContext;
        public TestController(ArticleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello world!");
        }

        #region UseCase - Get Numbers as IAsyncEnumerable

        [HttpGet]
        [Route("/numbers")]
        public IAsyncEnumerable<int> GetNumbers()
        {
            return GetSomeNumbers();
        }

        private async IAsyncEnumerable<int> GetSomeNumbers()
        {
            for (int i = 1; i <= 5; i++)
            {
                await Task.Delay(500);
                yield return i;
            }
        }

        #endregion

        #region UseCase - Get Posts as IAsyncEnumerable from DB

        [HttpGet]
        [Route("/posts")]
        public IAsyncEnumerable<Post> GetPosts()
        {
            //Chose one of 2 options
            var posts = GetPostsQueryable();
            //var posts = GetPostsStreamForeach();
            return posts;
        }

        private IAsyncEnumerable<Post> GetPostsQueryable()
        {
            return _dbContext.Posts.AsNoTracking().Where(a => a.Name == Constants.FileNames.With_1_Element_1KB).AsAsyncEnumerable();
        }

        private async IAsyncEnumerable<Post> GetPostsStreamForeach()
        {

            foreach (var post in _dbContext.Posts.AsNoTracking().Where(a => a.Name == Constants.FileNames.With_1_Element_1KB))
            {
                //await Task.Delay(500);
                yield return post;
            }
        }

        #endregion
    }
}
