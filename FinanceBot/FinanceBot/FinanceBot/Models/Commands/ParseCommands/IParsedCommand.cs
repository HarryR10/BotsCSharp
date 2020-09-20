using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using FinanceBot.Models.Repository;

namespace FinanceBot.Models.Commands.ParseCommands
{
    public interface IParseCommand
    {
        public void Execute(Message message,
            TelegramBotClient client);

        //public void Execute(Message message,
        //    TelegramBotClient client,
        //    IExpenseRepository expenseRepository,
        //    IUserAccountRepository userAccountRepository,
        //    ICategoryRepository categoryRepository);
    }
}
