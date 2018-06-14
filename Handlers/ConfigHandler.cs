using Sykoo.Managers;
using Sykoo.Models;
using System;
using System.IO;

namespace Sykoo.Handlers
{
    public class ConfigHandler
    {
        public ConfigModel Config
        {
            get => DatabaseManager.GetConfig();
        }

        public void CheckConfig()
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "database", "config.json"))) return;
            Console.Write("Enter token: ");
            string token = Console.ReadLine();
            DatabaseManager.SaveConfig(new ConfigModel
            {
                Token = token,
                Prefix = "s!"
            });
        }

        public void Save(ConfigModel config)
            => DatabaseManager.SaveConfig(config);
    }
}
