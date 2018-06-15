using Newtonsoft.Json;
using Sykoo.Models;
using System.IO;

namespace Sykoo.Managers
{
    public class DatabaseManager
    {
        static readonly object fileLock = new object();

        private string GetDatabasePath(string guildId)
        {
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "database", guildId)))
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "database", guildId));
            return Path.Combine(Directory.GetCurrentDirectory(), "database", guildId);
        }

        public  GuildSettings GetGuildSettings(string guildId)
            => JsonConvert.DeserializeObject<GuildSettings>(File.ReadAllText(Path.Combine(GetDatabasePath(guildId), "settings.json")));

        public  void SaveGuildSettings(string guildId, GuildSettings settings)
            => WriteFile(Path.Combine(GetDatabasePath(guildId), "settings.json"), JsonConvert.SerializeObject(settings));

        public  ConfigModel GetConfig()
            => JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "database", "config.json")));

        public  void SaveConfig(ConfigModel config)
            => WriteFile(Path.Combine(Directory.GetCurrentDirectory(), "database", "config.json"), JsonConvert.SerializeObject(config));

        private void WriteFile(string path, string data)
        {
            lock (fileLock)
            {
                File.WriteAllText(path, data);
            }
        }
    }
}
