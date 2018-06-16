using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Sykoo.Handlers;
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
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    ThrowOnError = true,
                    IgnoreExtraArgs = false,
                    CaseSensitiveCommands = false,
                    DefaultRunMode = RunMode.Async
                }))
                .AddSingleton(new DocumentStore
                {
                    Database = "Sykoo",
                    Urls = new[] { "http://localhost:8080" }
                }.Initialize())
                .AddSingleton<MainHandler>()
                .AddSingleton<GuildHandler>()
                .AddSingleton<ConfigHandler>()
                .AddSingleton<EventsHandler>();

            var provider = services.BuildServiceProvider();
            provider.GetRequiredService<ConfigHandler>().CheckConfig();
            await provider.GetRequiredService<MainHandler>().StartAsync();
            await provider.GetRequiredService<EventsHandler>().InitializeAsync(provider);

            await Task.Delay(-1);
        }
    }
}
