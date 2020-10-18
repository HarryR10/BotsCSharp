using System;
using FinanceBot.Models.CommandsException;
using FinanceBot.Views.Update;

namespace FinanceBot.Models.CommandsExceptions
{
    public class CategoryNotFoundException : CommandExeption
    {
        public CategoryNotFoundException(string categoryName)
        {
            base.BadCommand = string
                .Format(SimpleTxtResponse.CategoryNotFound, categoryName);
        }
    }
}
