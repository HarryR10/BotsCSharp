using System;
using System.Threading.Tasks;
using FinanceBot.Models.CommandsException;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceBot.Models.Commands.ParseCommands
{
    //TODO: new Interface?
    public class BadCommand : IParseCommand
    {
        private string _errorMsg;

        public BadCommand(ParseCommandException exception)
        {
            _errorMsg = exception.BadCommand;
        }

        public async Task<Message> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            return await client.SendTextMessageAsync(chatId,
                string.Format(@"{0} - it's BadCommand", _errorMsg), replyToMessageId: messageId);
        }
    }
}
