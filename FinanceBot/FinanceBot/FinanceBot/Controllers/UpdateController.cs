using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types;
using FinanceBot.Models;
using FinanceBot.Models.Repository;
using FinanceBot.Models.Commands.Utils;
using System;
using FinanceBot.Models.CommandsException;
using FinanceBot.Models.Commands.ParseCommands;

namespace FinanceBot.Controllers
{
    [ApiController]
    [Route(Settings.WebHookRoutePart)]
    public class UpdateController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices] IConfiguration configuration,
            [FromBody] Update update,
            [FromServices] Bot bot,
            [FromServices] IExpenseRepository expenseRepository,
            [FromServices] IUserAccountRepository userAccountRepository,
            [FromServices] ICategoryRepository categoryRepository)
        {
            if (update == null) return Ok();

            var commands = bot.Commands;
            var message = update.Message;

            if (message == null) return Ok();

            var config = configuration.Get<Settings>();
            var client = await bot.Get(config);

            foreach (var command in commands)
            {
                if (command.Contains(config, message))
                {
                    command.Execute(message, client, expenseRepository,
                        userAccountRepository, categoryRepository);

                    return Ok();
                }
            }

            var parseCommandDict = MessageParser
                .ComposeParseCommandDict(message, userAccountRepository);

            //TODO: Exeption handlers
            try
            {
                var parsedCommand = MessageParser
                    .ReadCommand(parseCommandDict, message);
                parsedCommand.Execute(message, client);
            }
            catch (ParseCommandException e)
            {
                var badCommand = new BadCommand(e);
                badCommand.Execute(message, client);
            }
            
            return Ok();
        }
    }
}