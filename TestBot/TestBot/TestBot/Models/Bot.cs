﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using TestBot.Models.Commands;

namespace TestBot.Models
{
    public class Bot
    {
        private TelegramBotClient _client;

        public IReadOnlyList<ICommand> Commands { get; private set; }

        public async Task<TelegramBotClient> Get(Settings settings)
        {
            string token = settings.ApiToken;
            string url = settings.Url;

            if (_client != null)
            {
                return _client;
            }

            Commands = new List<ICommand>()
            {
                new HelloCommand()
            };

            _client = new TelegramBotClient(token);
            var hook = string.Format(url, Settings.WebHookRoutePart);
            await _client.SetWebhookAsync(hook);

            return _client;
        }
    }
}
