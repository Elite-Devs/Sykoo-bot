using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Sykoo.Handlers
{
    public class MainHandler
    {
        ConfigHandler ConfigHandler { get; }
        DiscordSocketClient Client { get; }
        EventsHandler EventsHandler { get; }

        public MainHandler(ConfigHandler configHandler, DiscordSocketClient client, EventsHandler eventsHandler)
        {
            ConfigHandler = configHandler;
            Client = client;
            EventsHandler = eventsHandler;
        }

        public async Task StartAsync()
        {
            // Events to come
            Client.Ready += EventsHandler.Ready;
            Client.Connected += EventsHandler.Connected;
            Client.Log += EventsHandler.Log;
            Client.MessageReceived += EventsHandler.MessageRecievedAsync;

            await Client.LoginAsync(TokenType.Bot, ConfigHandler.Config.Token).ConfigureAwait(false);
            await Client.StartAsync().ConfigureAwait(false);
        }
    }
}
