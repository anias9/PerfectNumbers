using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PerfectNumbers
{
    public class Program
    {   
        private static readonly object balanceLock = new object();

        public static void Main()
        {
            Numbers.ReadCsvFile();
            Console.Title = "Szukam liczb doskonałych";
            Console.SetCursorPosition(1, 3);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Szukam liczb doskonałych (ESC - Przerwij)");

            Thread t1 = new Thread(new ThreadStart(Numbers.IsPerfectNumber));
            t1.Start();
            t1.IsBackground = true;
            Thread t2 = new Thread(new ThreadStart(Numbers.Progress));
            t2.Start();
            t1.IsBackground = true;

            while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }

            Console.ReadKey();
        }
    }
}