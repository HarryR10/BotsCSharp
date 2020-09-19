using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using FinanceBot.Models.Repository;

namespace FinanceBot.Models.Commands
{
    public interface ICommand
    {
        public string CommandName { get; }

        public void Execute(Message message,
            TelegramBotClient client,
            IExpenseRepository expenseRepository,
            IUserAccountRepository userAccountRepository,
            ICategoryRepository categoryRepository);

        public bool Contains(Settings settings, Message message)
        {
            var botName = settings.Name;
            var command = message.Text;
            bool isCompleteCommand = command.Contains(botName);

            if (message.Chat.Type == ChatType.Private)
            {
                isCompleteCommand = true;
            }

            return !string.IsNullOrEmpty(command)
                && command.Contains(CommandName)
                && isCompleteCommand;
        }
    }
}
