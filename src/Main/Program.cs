using System;

namespace Main
{
    class Program
    {
        private static IdleWorker _idleWorker;

        static void Main(string[] args)
        {
            _idleWorker = new IdleWorker();

            Console.WriteLine("Press any key to quit");

            Console.ReadKey();
        }
    }
}
