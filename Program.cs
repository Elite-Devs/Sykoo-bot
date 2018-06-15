using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Sykoo.Handlers;
using Sykoo.Managers;
using System;
using System.Threading.Tasks;

namespace Sykoo
{
    class Program
    {
        static void Main(string[] args)
            => new Program().InitializeAsync().GetAwaiter().GetResult();

        async Task InitializeAsync()
        {
            var services = new ServiceCollection()
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    MessageCacheSize = 20,
                    AlwaysDownloadUsers = true,
                    LogLevel = LogSeverity.Error
                }))
                .AddSingleton(new CommandService())
                .AddSingleton<ConfigHandler>()
                .AddSingleton<DatabaseManager>()
                .AddSingleton<MainHandler>()
                .AddSingleton<EventsHandler>();

            var provider = services.BuildServiceProvider();
            provider.GetRequiredService<ConfigHandler>().CheckConfig();
            await provider.GetRequiredService<MainHandler>().StartAsync();
            await provider.GetRequiredService<EventsHandler>().InitializeAsync(provider);

            await Task.Delay(-1);
        }
    }
}
