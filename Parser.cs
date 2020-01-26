using System;
using System.Collections.Generic;

namespace stockmarket
{
    public class Parser
    {
        public string StockSymbol { get; set; }
        public string Command { get; set; }

        public Parser(string commands) 
        {
            foreach (string command in commands.Split(" "))
            {               
                command.Trim(); //Get rid of possible double spaces.
                if (IsSymbol(command))
                {
                    StockSymbol = command.ToUpper();
                }
                else if(IsCommand(command))
                {
                    Command = command.ToUpper();
                }
                else
                {
                    throw new Exception("Mistake!");
                }
            }
        }

        //Returns true id the <param>Command</param> is a stock name or a stock symbol.
        public bool IsSymbol(string input)
        {
            foreach (Stock stock in DataLoader.Stocks)
            {
                if (stock.Name.Equals(input.ToUpper()) || stock.Symbol.Equals(input.ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }

        public string ProcessCommand()
        {
            return Command switch
            {
                "DAILY" => DataLoader.GetDaily(StockSymbol).Result,
                "GQ" => DataLoader.GetGlobalQuote(StockSymbol).Result,
                _ => "",
            };
        }

        public bool IsCommand(string input)
        {
            return input.ToUpper() == "DAILY" || input.ToUpper() == "GQ";
        }

        public void ThrowError()
        {
            Console.WriteLine("The command entered is not valid!");
        }

    }
}
