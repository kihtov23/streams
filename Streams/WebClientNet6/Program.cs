using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebClientNet6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start WebClient .Net Core 3.1");

            Stopwatch sw = Stopwatch.StartNew();
            //GetPosts();
            var numbers = Enumerable.Range(1, 10);
            Parallel.ForEach(numbers, (number) =>
            {
                GetPosts();
            });

            sw.Stop();


            Console.WriteLine($"Finish. Time per request: {sw.Elapsed / 10}");
            Console.ReadLine();
        }

        static void GetPosts()
        {
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://localhost:5001/posts").GetAwaiter().GetResult();
            var result = response.Content.ReadAsStringAsync();
        }
    }
}
