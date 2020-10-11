using Chizuru.Models;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Chizuru.Services
{
    class StartupService
    {
        private IServiceProvider _provider;
        private DiscordSocketClient _client;
        private CommandService _commands;

        public StartupService(IServiceProvider provider, DiscordSocketClient client, CommandService commands)
        {
            _provider = provider;
            _client = client;
            _commands = commands;
        }

        public async Task StartAsync()
        {
            String configFile = $@"{Directory.GetCurrentDirectory()}/config.json";
            Configuration config;

            if (!File.Exists(configFile))
            {
                using (StreamWriter file = File.CreateText(configFile))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    config = new Configuration
                    {
                        Token = "put-your-token-here"
                    };

                    serializer.Serialize(file, config);
                }

                Console.WriteLine("[Warn] Please put your token inside the config.json file.");
                Environment.Exit(0);
            }

            config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(configFile));

            await _client.LoginAsync(TokenType.Bot, config.Token);
            await _client.StartAsync();
            await _client.SetGameAsync("!help", type: ActivityType.Listening);

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }
    }
}
