using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace PerfectNumbers
{
    class Numbers
    {
        public static StreamReader reader = new StreamReader(@"liczby.csv");
        static List<int> numbers = new List<int>();
        public static int suma = 0, wiersz = 4, licznik = 0;
        static int rekord;
        static readonly object obj = new object();


        public static void ReadCsvFile()
        {
            try
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    numbers.Add(Convert.ToInt32(line));
                }
                var liczby = numbers.ToArray();

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("The csv file couldn't be read: ");
                Console.WriteLine(e.Message);
            }
        }

        public static void IsPerfectNumber()
        {
            foreach (int indeks in numbers)
            {
                lock (obj)
                {
                    rekord++;
                    int sumDzielnikow = 0;
                    for (int i = 1; i < indeks; i++)
                    {
                        if (indeks % i == 0)
                        {
                            sumDzielnikow = sumDzielnikow + i;
                        }
                    }
                    if (sumDzielnikow == indeks)
                    {
                        wiersz++;
                        Console.SetCursorPosition(1, wiersz);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Liczba {0} jest doskonała (na pozycji nr {1})", indeks, rekord);
                        suma += indeks;
                        licznik++;

                    }
                }
            }
            Console.WriteLine("Jest {0} liczb doskonalych ich suma wynosi {1}", licznik, suma);
        }

        public static void Progress()
        {
            int numbersLen = numbers.Count;
            for (int i = 0; i < numbersLen; i++)
            {
                lock (obj)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(1, 1);
                    Console.WriteLine("{0} progres z {1}, procent {2} % ", rekord, numbersLen, (double)((100 * (double)(rekord) + 1) / numbers.Count));
                }
            }
        }
    }

}

