using System;
using FinanceBot.Models.CommandsException;
using FinanceBot.Views.Update;

namespace FinanceBot.Models.CommandsExceptions
{
    public class BadExpenseExeption : CommandExeption
    {
        public BadExpenseExeption(string expense)
        {
            base.BadCommand = string
                .Format(SimpleTxtResponse.BadExpense, expense);
        }
    }
}
