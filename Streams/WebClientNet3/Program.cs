using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebClientNet3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start WebClient .Net Core 3.1");

            Stopwatch sw = Stopwatch.StartNew();

            var numbers = Enumerable.Range(1, 10);
            Parallel.ForEach(numbers, (number) =>
            {
                //GetBigArticleAsSingleModel();
                //GetPosts();
                GetPostsAsSingleModel();
            });

            sw.Stop();


            Console.WriteLine($"Finish. Time per request: {sw.Elapsed/ 10}");
            Console.ReadLine();
        }


        static void GetBigArticleAsSingleModel()
        {
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://localhost:5001/bigArticleAsSingleModel").GetAwaiter().GetResult();
            var result = response.Content.ReadAsStringAsync();
        }

        static void GetPosts()
        {
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://localhost:5001/GetPosts").GetAwaiter().GetResult();
            var result = response.Content.ReadAsStringAsync();
        }

        static void GetPostsAsSingleModel()
        {
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://localhost:5001/GetPostsAsSingleModel").GetAwaiter().GetResult();
            var result = response.Content.ReadAsStringAsync();
        }

        static void GetFileAsStream()
        {
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://localhost:5001/fileStream").GetAwaiter().GetResult();
            var result = response.Content.ReadAsStringAsync();
        }

        // Get File stream

        public static void Post()
        {
            var client = new HttpClient();
            HttpResponseMessage response = client.PostAsJsonAsync("https://localhost:5001/", "test").GetAwaiter().GetResult();
            var result = response.Content.ReadAsStringAsync();
        }
    }
}
