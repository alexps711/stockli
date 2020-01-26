using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace stockmarket
{
    public static class DataLoader
    {
        static readonly HttpClient client = new HttpClient();
        public static List<Stock> Stocks { get; set; }


        public static async Task<string> GetGlobalQuote(string symbol)
        {
            try
            {
                string responseBody = await client.GetStringAsync($"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol.ToUpper()}&apikey=" + Constants.API_KEY);
                //Get rid of the parent token of the JSON.
                JObject json = (JObject)JObject.Parse(responseBody)["Global Quote"];

                GlobalQuote ts = json.ToObject<GlobalQuote>();

                return ts.ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message: {0} ", e.Message);
                Console.WriteLine(e.StackTrace);
                return "0";
            }
        }

        public static async Task<string> GetDaily(string symbol)
        {
            try
            {
                string responseBody = await client.GetStringAsync($"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol.ToUpper()}&apikey=" + Constants.API_KEY);
                //Get rid of the parent token of the JSON.
                JObject json = (JObject)JObject.Parse(responseBody)["Time Series (Daily)"];
                //Retrieve the stock queried
                int index = Stocks.FindIndex(s => s.Symbol.Equals(symbol.ToUpper()));
                Stock currentStock = Stocks[index];
                //Store the timestamps inside the retrieved stock and format them for printing.
                string toReturn = "";
                foreach (JProperty child in json.Properties())
                {
                    TimeStamp ts = child.First.ToObject<TimeStamp>();
                    Stocks[index].Stamps.Add(child.Name, ts);
                    toReturn += child.Name + "\n" + ts.ToString() + "\n";
                }
                return toReturn;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return e.Message;
            }
        }

        public static void ReadCSV()
        {
            Stocks = new List<Stock>();
            try
            {
                using var reader = new StreamReader("/Users/alex/projects/stockmarket/stockmarket/Assets/companylist.csv");
                string headerLine = reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    //Create a stock with the symbol and name read in the CSV, stripping the double quotes.
                    Stock stock = new Stock(values[1].Replace("\"", ""), values[0].Replace("\"", ""));
                    Stocks.Add(stock);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
