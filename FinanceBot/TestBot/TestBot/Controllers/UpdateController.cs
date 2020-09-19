using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types;
using TestBot.Models;

namespace TestBot.Controllers
{
    [ApiController]
    [Route(Settings.WebHookRoutePart)]
    public class UpdateController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices] IConfiguration configuration,
            [FromBody] Update update,
            [FromServices] Bot bot)
        {
            if (update == null) return Ok();

            var commands = bot.Commands;
            var message = update.Message;

            var config = configuration.Get<Settings>();
            var client = await bot.Get(config);

            foreach (var command in commands)
            {
                if (command.Contains(config, message))
                {
                    command.Execute(message, client);
                    break;
                }
                //TODO: error handler

            }

            return Ok();
        }
    }
}