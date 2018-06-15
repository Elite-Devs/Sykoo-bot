using Sykoo.Managers;
using Sykoo.Models;
using System;
using System.IO;

namespace Sykoo.Handlers
{
    public class ConfigHandler
    {
        DatabaseManager DBMan { get; }

        public ConfigHandler(DatabaseManager manager) => DBMan = manager;

        public ConfigModel Config
        {
            get => DBMan.GetConfig();
        }

        public void CheckConfig()
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "database", "config.json"))) return;
            Console.Write("Enter token: ");
            string token = Console.ReadLine();
            DBMan.SaveConfig(new ConfigModel
            {
                Token = token,
                Prefix = "s!"
            });
        }

        public void Save(ConfigModel config)
            => DBMan.SaveConfig(config);
    }
}
