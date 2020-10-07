using System;
namespace FinanceBot.Models.CommandsException
{
    public class UserAlredyExistException : CommandExeption
    {
        public UserAlredyExistException(int userId)
        {
            base.BadCommand = string
                .Format("Пользователь с id{0} уже существует!", userId);
        }
    }
}
