using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Sykoo.Handlers
{
    public class MainHandler
    {
        ConfigHandler ConfigHandler { get; }
        DiscordSocketClient Client { get; }

        public MainHandler(ConfigHandler configHandler, DiscordSocketClient client)
        {
            ConfigHandler = configHandler;
            Client = client;
        }

        public async Task StartAsync()
        {
            // Events to come

            await Client.LoginAsync(TokenType.Bot, ConfigHandler.Config.Token).ConfigureAwait(false);
            await Client.StartAsync().ConfigureAwait(false);
        }
    }
}
