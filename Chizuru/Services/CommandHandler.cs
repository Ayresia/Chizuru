using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Chizuru.Services
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _provider;

        public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider provider)
        {
            _client = client;
            _commands = commands;
            _provider = provider;

            _client.MessageReceived += HandleCommands;
        }

        public async Task HandleCommands(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, msg);
            int argPos = 0;

            if (msg.HasCharPrefix('!', ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _provider);
            }
        }
    }
}
