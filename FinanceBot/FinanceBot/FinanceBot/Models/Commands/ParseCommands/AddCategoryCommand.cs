﻿using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using FinanceBot.Models.EntityModels;
using FinanceBot.Models.Repository;
using Telegram.Bot;
using Telegram.Bot.Types;
using FinanceBot.Models.CommandsException;
using FinanceBot.Views.Update;

namespace FinanceBot.Models.Commands.ParseCommands
{
    public class AddCategoryCommand : IParseCommand
    {
        private readonly string _rgxString = @"!$";
        private IUserAccountRepository _userAccountRepository;
        private ICategoryRepository _categoryRepository;
        private long _chatId;
        private string _msg;
        private int _usrId;
        private TelegramBotClient _client;

        public AddCategoryCommand(IUserAccountRepository userAccountRepository,
            ICategoryRepository categoryRepository)
        {
            _userAccountRepository = userAccountRepository;
            _categoryRepository = categoryRepository;
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

            bool isMounthly = false;
            string cleanCmd;

            Regex regex = new Regex(_rgxString);
            Match match = regex.Match(_msg);

            if (match.Success)
            {
                isMounthly = true;
                cleanCmd = _msg.Substring(0, _msg.Length - 1);
            }
            else
            {
                cleanCmd = _msg;
            }


            string categoryName = cleanCmd.Split(" ")[0];

            _categoryRepository.AddCategory(
                new Category
                {
                    CategoryName = categoryName,
                    CategoryId =
                        _categoryRepository.Categories
                        .Where(c => c.Author.UserId == _usrId)
                        .Select(c => c.CategoryId)
                        .OrderByDescending(catId => catId)
                        .FirstOrDefault() + 1,
                    Description = cleanCmd,
                    IsBasic = false,
                    Author =
                        _userAccountRepository.Accounts
                        .Where(u => u.UserId == _usrId)
                        //TODO: FirstOrDefault / First?
                        .FirstOrDefault(),  
                    IsMounthly = isMounthly
                }, userAccount);

            return await _client.SendTextMessageAsync(_chatId,
                string.Format(SimpleTxtResponse.AddCategory, categoryName));
        }
    }
}
