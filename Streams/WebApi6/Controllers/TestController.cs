using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI6;

namespace WebApi6.Controllers
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
            //TODO chose one of 2
            //var posts = GetPostsQueryable();
            var posts = GetPostsStreamForeach();
            return posts;
        }

        private IAsyncEnumerable<Post> GetPostsQueryable()
        {
            return _dbContext.Posts.Where(a => a.Name == "8900 elements").AsAsyncEnumerable();
        }

        private async IAsyncEnumerable<Post> GetPostsStreamForeach()
        {

            foreach (var post in _dbContext.Posts.AsNoTracking().Where(a => a.Name == "8900 elements"))
            {
                await Task.Delay(500);
                yield return post;
            }
        }

        #endregion
    }
}
