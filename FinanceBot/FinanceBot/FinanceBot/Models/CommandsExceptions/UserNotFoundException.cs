using System;
using FinanceBot.Views.Update;

namespace FinanceBot.Models.CommandsException
{
    public class UserNotFoundException : CommandExeption
    {
        public UserNotFoundException(string message, int userId)
        {
            base.BadCommand = string
                .Format(SimpleTxtResponse.UserNotFound,
                message, userId);
        }
    }
}
