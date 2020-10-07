using System;
namespace FinanceBot.Models.CommandsException
{
    public class ParseCommandException : CommandExeption
    {
        public ParseCommandException(string badCommand)
        {
            base.BadCommand = badCommand;
        }
    }
}
