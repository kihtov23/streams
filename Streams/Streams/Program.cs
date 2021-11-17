using System;

namespace Streams
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Data Seed....");

            DataSeed.Seed().GetAwaiter().GetResult();

            Console.WriteLine("Finished.");
            Console.ReadLine();
        }
    }
}
