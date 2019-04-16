using Services;
using System;

namespace ConsoleApp
{
    class Program
    {
        private static IdleWorker _idleWorker;

        static void Main(string[] args)
        {
            _idleWorker = new IdleWorker();

            Console.WriteLine($"Started: {DateTime.Now}");

            Console.WriteLine("Press any key to quit");

            Console.ReadKey();
        }
    }
}
