using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.VisualBasic;
using System;
using System.Threading.Tasks;

namespace Chizuru.Services
{
    class LogService
    {
        private DiscordSocketClient _client;
        private CommandService _commands;

        public LogService(DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;

            _client.Log += LogEvent;
            _commands.CommandExecuted += CommandExecutedCommand;
        }

        public Task LogEvent(LogMessage msg)
        {
            Console.WriteLine($"[{DateTime.Now}] - {msg.Message}");
            return Task.CompletedTask;
        }

        public async Task CommandExecutedCommand(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!string.IsNullOrEmpty(result.ErrorReason))
            {
                Console.WriteLine($"[{DateTime.Now}] - !{command.Value.Name} [{result.Error} - {result.ErrorReason}]");
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
