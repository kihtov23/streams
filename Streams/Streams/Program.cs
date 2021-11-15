using System;

namespace Streams
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSeed.Seed().GetAwaiter().GetResult();

            Console.WriteLine("Hello World!");
        }
    }
}
