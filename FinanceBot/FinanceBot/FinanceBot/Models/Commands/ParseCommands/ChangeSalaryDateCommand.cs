using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FinanceBot.Models.CommandsException;
using FinanceBot.Models.EntityModels;
using FinanceBot.Models.Repository;
using FinanceBot.Views.Update;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceBot.Models.Commands.ParseCommands
{
    public class ChangeSalaryDateCommand : IParseCommand
    {
        private readonly string _rgxString = @"\d{1,2}$";
        private IUserAccountRepository _userAccountRepository;
        private long _chatId;
        private string _msg;
        private int _usrId;
        private UserAccount _currentUser;
        private bool _isNewUser = false;
        private TelegramBotClient _client;


        public ChangeSalaryDateCommand(
            IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async Task<Message> Execute(Message message, TelegramBotClient client)
        {
            _chatId = message.Chat.Id;
            _msg = message.Text;
            _usrId = message.From.Id;
            _client = client;

            Regex regex = new Regex(_rgxString);
            Match match = regex.Match(_msg);

            if (int.TryParse(match.Value, out int salaryDay)
                && salaryDay <= 31
                && salaryDay > 0)
            {
                if (_userAccountRepository.GetUser(_usrId,
                    out UserAccount userAccount))
                {
                    _currentUser = userAccount;
                    _currentUser.InitUserDates(salaryDay);
                }
                else
                {
                    _currentUser = new UserAccount(_usrId, salaryDay);
                    _userAccountRepository.AddAccount(_currentUser);
                    _isNewUser = true;
                }

                var msgToUser = string.Format(SimpleTxtResponse.ChangeSalaryDay,
                        _currentUser.CountdownDate.Day.ToString(),
                        _currentUser.CountdownDate.Month.ToString(),
                        _currentUser.ResetDate.Day.ToString(),
                        _currentUser.ResetDate.Month.ToString());

                if (_isNewUser)
                {
                    msgToUser += "\n\n" + SimpleTxtResponse.HelloUser;
                }

                return await _client.SendTextMessageAsync(_chatId, msgToUser);
            }
            else
            {
                throw new ParseCommandException(_msg);
            }
        }
    }
}
