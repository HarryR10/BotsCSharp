using System;
using Telegram.Bot.Types;
using FinanceBot.Models.EntityModels;
using Microsoft.AspNetCore.Mvc;
using FinanceBot.Models.Repository;
using System.Linq;
using System.Collections.Generic;
using FinanceBot.Models.Commands.ParseCommands;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FinanceBot.Models.CommandsException;

namespace FinanceBot.Models.Commands.Utils
{
    public static class MessageParser
    {
        /// <summary>
        /// Используется для наполнения словаря распознаваемых комманд
        /// (в зависимости от того, использовал ли пользователь бота ранее)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userAccountRepository"></param>
        /// <returns></returns>
        public static Dictionary<string, IParseCommand> ComposeParseCommandDict(
            Message message,
            IUserAccountRepository userAccountRepository)
        {
            var result = new Dictionary<string, IParseCommand>();

            if (!userAccountRepository.Accounts
                .Where(u => u.UserId == message.From.Id)
                .Any())
            {
                //дата начисления зарплаты в формате 1-2х значного числа
                result.Add(@"^\d{1,2}$", new ChangeSalaryDateCommand());
                return result;
            }

            //100 проезд на такси
            //100 проезд
            result.Add(@"^[^\/]\d+\s+.+", new AddExpenseCommand());

            //обед бизнес-ланч, кафе
            //обед
            //связь телефон!
            //связь!
            result.Add(@"^[^\/]\D+(\s+|\s+.*)\!?$", new AddCategoryCommand());

            //-обед
            result.Add(@"^-{1}\D+", new DelCategoryCommand());

            //зп12
            result.Add(@"^зп\d{1,2}$", new ChangeSalaryDateCommand());

            return result;
        }

        public static IParseCommand ReadCommand(
            Dictionary<string, IParseCommand> commandDict,
            Message message)
        {
            var msg = message.Text;

            var currentCommands = commandDict
                .Where(el =>
                {
                    Regex regex = new Regex(el.Key);
                    Match match = regex.Match(msg);

                    if (match.Success)
                    {
                        return true;
                    }
                    return false;
                })
                .Select(el => el.Value);

            if (currentCommands.Count() != 0)
            {
                return currentCommands.FirstOrDefault();
            }

            throw new ParseCommandException(msg);
        }
    }
}
