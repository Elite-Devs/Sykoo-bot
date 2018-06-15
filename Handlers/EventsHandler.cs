using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Sykoo.Addons;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Sykoo.Handlers
{
    public class EventsHandler
    {
        DiscordSocketClient Client { get; }
        IServiceProvider ServiceProvider { get; set; }
        CommandService CommandService { get; }

        public EventsHandler(DiscordSocketClient client)
        {
            Client = client;
        }

        public async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            // Null reference exception? 何で？
            //await CommandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
        }

        internal Task Ready()
        {
            Console.WriteLine("Bot is ready");
            Client.SetActivityAsync(new Game("over this server", ActivityType.Watching));
            return Task.CompletedTask;
        }

        internal Task Connected()
        {
            Console.WriteLine("Connected to Discord");
            return Task.CompletedTask;
        }

        internal Task Log(LogMessage log)
        {
            Console.WriteLine($"Log: {log.Message ?? log.Exception.Message}");
            return Task.CompletedTask;
        }

        internal async Task MessageRecievedAsync(SocketMessage message)
        {
            if (!(message is SocketUserMessage userMessage) || userMessage.Source != MessageSource.User || userMessage.Author.IsBot)
                return;
            // Check for our "one message only" channel
            Console.WriteLine("We got a message lol");

            int argPos = 0;
            var context = new IContext(Client, userMessage, ServiceProvider);
            if (!userMessage.HasStringPrefix(context.Config.Prefix, ref argPos)) return;
            // another one え？
            //var result = await CommandService.ExecuteAsync(context, argPos, ServiceProvider, MultiMatchHandling.Best);
            /*switch (result.Error)
            {
                case CommandError.Exception:
                    Console.WriteLine($"Exception: {result.ErrorReason}");
                    break;
            }*/
            Console.WriteLine("Got through the command checks");
        }
    }
}
