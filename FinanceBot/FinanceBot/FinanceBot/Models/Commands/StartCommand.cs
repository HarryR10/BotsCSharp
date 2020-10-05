using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using FinanceBot.Models.Repository;
using FinanceBot.Views.Update;
using System.Threading.Tasks;

namespace FinanceBot.Models.Commands
{
    public class StartCommand : ICommand
    {
        public string CommandName => "start";

        public async Task<Message> Execute(Message message,
            TelegramBotClient client,
            IExpenseRepository expenseRepository,
            IUserAccountRepository userAccountRepository,
            ICategoryRepository categoryRepository)
        {
            var userId = message.From.Id;
            var chatId = message.Chat.Id;

            return await client.SendTextMessageAsync(chatId,
                SimpleTxtResponse.HelloUser);
        }
    }
}
