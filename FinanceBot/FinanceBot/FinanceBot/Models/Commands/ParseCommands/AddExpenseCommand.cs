using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FinanceBot.Models.CommandsException;
using FinanceBot.Models.CommandsExceptions;
using FinanceBot.Models.EntityModels;
using FinanceBot.Models.Repository;
using FinanceBot.Views.Update;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceBot.Models.Commands.ParseCommands
{
    public class AddExpenseCommand : IParseCommand
    {
        private readonly string _rgxString = @"(\d+|\D+)";
        private IUserAccountRepository _userAccountRepository;
        private ICategoryRepository _categoryRepository;
        private IExpenseRepository _expenseRepository;

        private long _chatId;
        private string _msg;
        private int _usrId;
        private TelegramBotClient _client;


        public AddExpenseCommand(
            IUserAccountRepository userAccountRepository,
            ICategoryRepository categoryRepository,
            IExpenseRepository expenseRepository)
        {
            _userAccountRepository = userAccountRepository;
            _categoryRepository = categoryRepository;
            _expenseRepository = expenseRepository;
        }

        public async Task<Message> Execute(Message message,
            TelegramBotClient client)
        {
            _chatId = message.Chat.Id;
            _msg = message.Text;
            _usrId = message.From.Id;
            _client = client;

            if (!_userAccountRepository.GetUser(_usrId,
                 out UserAccount userAccount))
            {
                throw new UserNotFoundException(_msg, _usrId);
            }

            Regex regex = new Regex(_rgxString);
            Match match = regex.Match(_msg);

            if (match.Success && decimal.TryParse(match.Value, out decimal summ))
            {
                var cmdTxtContent = (match.NextMatch().Value).Split(" ",
                    StringSplitOptions.RemoveEmptyEntries);

                var categoryName = cmdTxtContent[0];
                var description = (_msg.Split(categoryName)[1]).Trim();

                var category = _categoryRepository
                    .GetCategory(userAccount, categoryName);

                if (category == null)
                {
                    category = _categoryRepository
                        .GetCategory(userAccount, "other");
                }

                _expenseRepository.AddExpense(new Expense
                {
                    ExpenseId = _expenseRepository.Expenses
                        .Where(e => e.UserAccount.UserId == _usrId)
                        .Select(e => e.ExpenseId)
                        .OrderByDescending(e => e)
                        .FirstOrDefault() + 1,
                    Amount = summ,
                    Category = category,
                    Description = description,
                    ExpenseDateTime = DateTime.Now,
                    UserAccount = userAccount,
                });
            }
            else
            {
                throw new BadExpenseExeption(_msg);
            }

            return await client.SendTextMessageAsync(_chatId,
                string.Format(SimpleTxtResponse.AddExpense,"","", _msg));
        }
    }
}
