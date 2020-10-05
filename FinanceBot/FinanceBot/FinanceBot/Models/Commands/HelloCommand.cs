using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using FinanceBot.Models.EntityModels;
using FinanceBot.Models.Repository;
using FinanceBot.Views.Update;
using System.Threading.Tasks;

namespace FinanceBot.Models.Commands
{
    public class HelloCommand : ICommand
    {
        public string CommandName => "hello";

        public async Task<Message> Execute(Message message,
            TelegramBotClient client,
            IExpenseRepository expenseRepository,
            IUserAccountRepository userAccountRepository,
            ICategoryRepository categoryRepository)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            expenseRepository.AddExpense(new Expense
            {
                UserAccount = userAccountRepository.Accounts.FirstOrDefault(),
                ExpenseId = 1,
                ExpenseDateTime = DateTime.Now,
                Category = categoryRepository.Categories.FirstOrDefault(),
                Amount = 13m,
                Description = "just two Expense"
            });

            var tempStr = string.Format(SimpleTxtResponse.ExpenseTemplate,
                expenseRepository.Expenses.FirstOrDefault().Amount,
                expenseRepository.Expenses.FirstOrDefault().Category.CategoryName,
                expenseRepository.Expenses.FirstOrDefault().Description,
                "/del1");

            var outStr = string.Format(SimpleTxtResponse.LastExpenses, tempStr);

            //TODO: Telegram.Bot.Exceptions.ApiRequestException
            return await client.SendTextMessageAsync(chatId, outStr);
        }
    }
}
