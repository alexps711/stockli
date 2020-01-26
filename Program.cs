using System;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Timers;

namespace stockmarket
{
    class Program
    {
        static void Main(string[] args)
        {
            DataLoader.ReadCSV();
            Console.WriteLine("Welcome!");
            int check = 1;
            while (check != 0)
            {
                Console.Write("> ");
                string symbol = Console.ReadLine();
                try
                {
                    Parser parser = new Parser(symbol);
                    check = 0;
                    Console.WriteLine(parser.ProcessCommand());
                }
                catch (Exception)
                {
                    Console.WriteLine("Mistake!");
                }
            }
            
        }
    }
}
