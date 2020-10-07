using System;
namespace FinanceBot.Models.CommandsException
{
    public class UserNotFoundException : CommandExeption
    {
        public UserNotFoundException(string message, int userId)
        {
            base.BadCommand = string
                .Format("{0}: пользователь с id{1} не зарегистрирован!",
                message, userId);
        }
    }
}
