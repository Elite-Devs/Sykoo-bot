using Discord;
using Discord.WebSocket;
using Raven.Client.Documents;
using System.Threading.Tasks;

namespace Sykoo.Handlers
{
    public class MainHandler
    {
        ConfigHandler ConfigHandler { get; }
        IDocumentStore Store { get; }
        DiscordSocketClient Client { get; }
        EventsHandler EventsHandler { get; }

        public MainHandler(ConfigHandler configHandler, EventsHandler eventsHandler, DiscordSocketClient client, IDocumentStore store)
        {
            ConfigHandler = configHandler;
            Store = store;
            Client = client;
            EventsHandler = eventsHandler;
        }

        public async Task StartAsync()
        {
            Client.Ready += EventsHandler.Ready;
            Client.Connected += EventsHandler.Connected;
            Client.Log += EventsHandler.Log;
            Client.MessageReceived += EventsHandler.MessageRecievedAsync;
            Client.GuildAvailable += EventsHandler.GuildAvailable;
            Client.LeftGuild += EventsHandler.LeftGuild;
            Client.JoinedGuild += EventsHandler.JoinedGuild;

            await Client.LoginAsync(TokenType.Bot, ConfigHandler.Config.Token).ConfigureAwait(false);
            await Client.StartAsync().ConfigureAwait(false);
        }
    }
}
