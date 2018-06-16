using Raven.Client.Documents;
using Sykoo.Models;
using System;
using System.IO;

namespace Sykoo.Handlers
{
    public class ConfigHandler
    {
        IDocumentStore Store { get; }

        public ConfigHandler(IDocumentStore store) => Store = store;

        public ConfigModel Config
        {
            get
            {
                using (var session = Store.OpenSession())
                    return session.Load<ConfigModel>("Config");
            }
        }

        public void CheckConfig()
        {
            using (var session = Store.OpenSession())
            {
                if (session.Advanced.Exists("Config")) return;
                Console.Write("Enter token: ");
                string token = Console.ReadLine();
                session.Store(new ConfigModel
                {
                    Id = "Config",
                    Token = token,
                    Prefix = "s!"
                });
                session.SaveChanges();
            }
        }

        public void Save(ConfigModel config = null)
        {
            config = config ?? Config;
            if (config == null) return;
            using (var session = Store.OpenSession())
            {
                session.Store(config, "Config");
                session.SaveChanges();
            }
        }
    }
}
