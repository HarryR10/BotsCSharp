using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using FinanceBot.Models.Repository;
using System.Threading.Tasks;

namespace FinanceBot.Models.Commands.ParseCommands
{
    public interface IParseCommand
    {
        public Task<Message> Execute(Message message,
            TelegramBotClient client);

        //public void Execute(Message message,
        //    TelegramBotClient client,
        //    IExpenseRepository expenseRepository,
        //    IUserAccountRepository userAccountRepository,
        //    ICategoryRepository categoryRepository);
    }
}
