using System;
using FinanceBot.Views.Update;

namespace FinanceBot.Models.CommandsException
{
    public class UserAlredyExistException : CommandExeption
    {
        public UserAlredyExistException(int userId)
        {
            base.BadCommand = string
                .Format(SimpleTxtResponse.UserAlredyExist, userId);
        }
    }
}
