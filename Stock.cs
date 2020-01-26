using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace stockmarket
{
    public class Stock
    {
        public Dictionary<string, TimeStamp> Stamps { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public Stock(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
            Stamps = new Dictionary<string, TimeStamp>();
        }
    }

    public struct GlobalQuote
    {
        [JsonProperty("01. symbol")]
        public string symbol;
        [JsonProperty("02. open")]
        public string open;
        [JsonProperty("03. high")]
        public string high;
        [JsonProperty("04. low")]
        public string low;
        [JsonProperty("06. volume")]
        public string volume;
        [JsonProperty("06. close")]
        public string close;
        [JsonProperty("0.5 price")]
        public string price;
        [JsonProperty("08. previous close")]
        public string prev_close;
        [JsonProperty("09. change")]
        public string change;
        [JsonProperty("07. latest trading day")]
        public string latest_trading_day;
        [JsonProperty("10. change percent")]
        public string change_percent;



        override public string ToString()
        {
            string toReturn = "";
            foreach (var property in GetType().GetFields())
            {
                var current = this;
                if (property.GetValue(this) != null)
                {
                    toReturn += $"{property.Name}: {property.GetValue(this)} \n";
                }
            }
            return toReturn;
        }
    }

    public struct TimeStamp
    {
        [JsonProperty("1. open")]
        public string open;
        [JsonProperty("2. high")]
        public string high;
        [JsonProperty("3. low")]
        public string low;
        [JsonProperty("4. close")]
        public string close;
        [JsonProperty("5. volume")]
        public string volume;

        override public string ToString()
        {
            string toReturn = "";
            foreach (var property in GetType().GetFields())
            {
                var current = this;
                toReturn += $"{property.Name}: {property.GetValue(this)} \n";
            }
            return toReturn;
        }
    }
}
