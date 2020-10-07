using System;
namespace FinanceBot.Models.CommandsException
{
    public class CommandExeption : Exception
    {
        public string BadCommand { get; set; }
    }
}
