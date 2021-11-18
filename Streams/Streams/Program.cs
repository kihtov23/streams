using System;

namespace DataSeed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Data Seed....");

            DataSeedHelper.Seed().GetAwaiter().GetResult();

            Console.WriteLine("Finished.");
            Console.ReadLine();
        }
    }
}
