using Telegram.Bot;
using Telegram.Bot.Types;

namespace TestBot.Models.Commands
{
    public class HelloCommand : ICommand
    {
        public string CommandName => "hello";

        public async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            await  client.SendTextMessageAsync(chatId, "Hello!", replyToMessageId: messageId);
        }
    }
}
