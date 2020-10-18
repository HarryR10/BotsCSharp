using System;
using FinanceBot.Views.Update;

namespace FinanceBot.Models.CommandsException
{
    public class CategoryAlredyExistException : CommandExeption
    {
        public CategoryAlredyExistException(string categoryName)
        {
            base.BadCommand = string
                .Format(SimpleTxtResponse.CategoryAlredyExist, categoryName);
        }
    }
}
