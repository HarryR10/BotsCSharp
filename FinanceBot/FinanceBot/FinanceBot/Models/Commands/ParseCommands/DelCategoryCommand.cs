using System;
using System.Threading.Tasks;
using FinanceBot.Models.Repository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceBot.Models.Commands.ParseCommands
{
    public class DelCategoryCommand : IParseCommand
    {
        public async Task<Message> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            return await client.SendTextMessageAsync(chatId,
                "It's DelCategoryCommand!", replyToMessageId: messageId);
        }

        //public void Execute(Message message, TelegramBotClient client, IExpenseRepository expenseRepository, IUserAccountRepository userAccountRepository, ICategoryRepository categoryRepository)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
