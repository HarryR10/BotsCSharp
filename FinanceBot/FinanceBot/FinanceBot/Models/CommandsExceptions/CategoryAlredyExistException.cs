using System;
namespace FinanceBot.Models.CommandsException
{
    public class CategoryAlredyExistException : CommandExeption
    {
        public CategoryAlredyExistException(int categoryId)
        {
            base.BadCommand = string
                .Format("Категория с id{0} уже существует!", categoryId);
        }
    }
}
