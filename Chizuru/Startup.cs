using Chizuru.Services;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Chizuru
{   
    class Startup
    {
        public async Task InitAsync()
        {
            Console.Title = "Chizuru";

            var services = new ServiceCollection()
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    MessageCacheSize = 1000
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    CaseSensitiveCommands = false,
                    DefaultRunMode = RunMode.Async
                }))
                .AddSingleton<CommandHandler>()
                .AddSingleton<LogService>()
                .AddSingleton<StartupService>();

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<CommandHandler>();
            provider.GetRequiredService<LogService>();
            await provider.GetRequiredService<StartupService>().StartAsync();
            await Task.Delay(-1);
        }
    }
}
