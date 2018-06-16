using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Sykoo.Handlers;
using Sykoo.Models;
using System;

namespace Sykoo.Addons
{
    public class IContext : ICommandContext
    {
        public IUser User { get; }
        public IGuild Guild { get; }
        public GuildModel Server { get; }
        public ConfigModel Config { get; }
        public IDiscordClient Client { get; }
        public IUserMessage Message { get; }
        public IMessageChannel Channel { get; }
        public ConfigHandler ConfigHandler { get; }
        public GuildHandler GuildHandler { get; }
        IDocumentSession Session { get; }

        public IContext(IDiscordClient client, IUserMessage message, IServiceProvider serviceProvider)
        {
            Client = client;
            Message = message;
            User = message.Author;
            Channel = message.Channel;
            Guild = (message.Channel as IGuildChannel).Guild;
            Config = serviceProvider.GetRequiredService<ConfigHandler>().Config;
            ConfigHandler = serviceProvider.GetRequiredService<ConfigHandler>();
            GuildHandler = serviceProvider.GetRequiredService<GuildHandler>();
            Server = serviceProvider.GetRequiredService<GuildHandler>().GetGuild(Guild.Id);
            Session = serviceProvider.GetRequiredService<IDocumentStore>().OpenSession();
        }
    }
}
