using System;
using System.Text.RegularExpressions;
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


        //TODO: refactor it:
        //--------------------//
        long _chatId;
        string _msg;
        int _usrId;
        UserAccount _currentUser;
        bool _isNewUser = false;
        TelegramBotClient _client;
        //--------------------//



        public ChangeSalaryDateCommand(
            IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }


        //--------------------//
        public void Execute(Message message, TelegramBotClient client)
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
                SendMsg(salaryDay);
            }
            else
            {
                throw new ParseCommandException(_msg);
            }
        }

        private async void SendMsg(int salaryDay)
        {
            if (_userAccountRepository.GetUser(_usrId,
                    out UserAccount userAccount))
            {
                _currentUser = userAccount;

                _currentUser.SalaryDay = salaryDay;
                _currentUser.InitUserDates();

                
            }
            else
            {
                _currentUser = new UserAccount(_usrId, salaryDay);
                _userAccountRepository.AddAccount(_currentUser);
                _isNewUser = true;
            }

            await _client.SendTextMessageAsync(_chatId,
            string.Format(SimpleTxtResponse.ChangeSalaryDay,
            _currentUser.CountdownDate.Day.ToString(),
            _currentUser.CountdownDate.Month.ToString(),
            _currentUser.ResetDate.Day.ToString(),
            _currentUser.ResetDate.Month.ToString()));

            if (_isNewUser)
            {
                await _client.SendTextMessageAsync(_chatId,
                SimpleTxtResponse.HelloUser);
            }
        }
        //--------------------//

        //public async void Execute(Message message, TelegramBotClient client)
        //{
        //    var chatId = message.Chat.Id;
        //    var msg = message.Text;
        //    var usrId = message.From.Id;

        //    Regex regex = new Regex(_rgxString);
        //    Match match = regex.Match(msg);

        //    UserAccount currentUser;
        //    bool isNewUser = false;

        //    if (int.TryParse(match.Value, out int salaryDay)
        //        && salaryDay <= 31
        //        && salaryDay > 0)
        //    {
        //        if (_userAccountRepository.GetUser(usrId,
        //            out UserAccount userAccount))
        //        {
        //            userAccount.SalaryDay = salaryDay;
        //            userAccount.InitUserDates();

        //            currentUser = userAccount;
        //        }
        //        else
        //        {
        //            currentUser = new UserAccount(usrId, salaryDay);
        //            _userAccountRepository.AddAccount(currentUser);
        //            isNewUser = true;
        //        }
        //    }
        //    else
        //    {
        //         throw new ParseCommandException(msg);
        //    }

        //    //var messageId = message.MessageId;

        //    await client.SendTextMessageAsync(chatId,
        //        string.Format(SimpleTxtResponse.ChangeSalaryDay,
        //        currentUser.CountdownDate.Day.ToString(),
        //        currentUser.CountdownDate.Month.ToString(),
        //        currentUser.ResetDate.Day.ToString(),
        //        currentUser.ResetDate.Month.ToString()));

        //    if (isNewUser)
        //    {
        //        await client.SendTextMessageAsync(chatId,
        //        SimpleTxtResponse.HelloUser);
        //    }

    }
}
