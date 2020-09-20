using System;
namespace FinanceBot.Models.CommandsException
{
    public class ParseCommandException : Exception
    {
        public string BadCommand { get; }

        public ParseCommandException(string badCommand)
        {
            BadCommand = badCommand;
        }
    }
}
