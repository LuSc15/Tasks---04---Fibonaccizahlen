using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Tasks___04___Fibonaccizahlen
{
    internal class Program
    {
        
        public static List<Task<(long, int)>> tasks = new List<Task<(long, int)>>();
        #region Farben
        public static ConsoleColor[] farben = { ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Blue,ConsoleColor.Gray};
        static Random rnd = new Random();
        #endregion
        static void Main(string[] args)
        {
            long[] stellen = new long[15];
            Stopwatch sw = new Stopwatch();

            sw.Start();
            int start = 30;
            int anzahlWerte = 15;
            for (int i = start; i < start+anzahlWerte;i++)
            {  
                tasks.Add(Task.Factory.StartNew<(long, int)>(x =>
                { 
                    return (Fib((int)x), (int)x); 
                }, i)); 
            }
            Punkte();
            sw.Stop();
            Console.WriteLine();
            Console.ResetColor();
            foreach(var i in tasks)
            {
                Console.WriteLine(i.Result.Item2+":"+i.Result.Item1);
            }
            Console.WriteLine(Convert.ToDouble(sw.ElapsedMilliseconds/1000.0) + " Sekunden");

        }

        public static long Fib(long x)
        {
            return x <= 2 ? 1 : Fib(x - 1) + Fib(x - 2);
        }

        public static void Punkte()
        {
            while (!Task.WhenAll(tasks).IsCompleted)
            {
                Console.ForegroundColor = farben[rnd.Next(0, farben.Length)];
                Console.Write(".");
                Thread.Sleep(100);
            }
        }
    }
}
