using System;
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
    public class DelCategoryCommand : IParseCommand
    {
        private readonly string _rgxString = @"^[^-]\D+";
        private IUserAccountRepository _userAccountRepository;
        private ICategoryRepository _categoryRepository;
        private long _chatId;
        private string _msg;
        private int _usrId;
        private TelegramBotClient _client;

        public DelCategoryCommand(IUserAccountRepository userAccountRepository,
            ICategoryRepository categoryRepository)
        {
            _userAccountRepository = userAccountRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Message> Execute(Message message, TelegramBotClient client)
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

            if (match.Success)
            {
                var currentCategory = _categoryRepository
                    .GetCategory(userAccount, match.Value);

                if (currentCategory != null)
                {
                    _categoryRepository.DeleteCategory(currentCategory);
                }
            }
            else
            {
                throw new CategoryNotFoundException(_msg);
            }

            return await client.SendTextMessageAsync(_chatId,
                string.Format(SimpleTxtResponse.DelCategory, match.Value));
        }

    }
}
