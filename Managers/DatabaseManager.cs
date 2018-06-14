using Newtonsoft.Json;
using Sykoo.Models;
using System.IO;

namespace Sykoo.Managers
{
    public class DatabaseManager
    {
        static readonly object fileLock = new object();

        private static string GetDatabasePath(string guildId)
        {
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "database", guildId)))
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "database", guildId));
            return Path.Combine(Directory.GetCurrentDirectory(), "database", guildId);
        }

        public static GuildSettings GetGuildSettings(string guildId)
            => JsonConvert.DeserializeObject<GuildSettings>(File.ReadAllText(Path.Combine(GetDatabasePath(guildId), "settings.json")));

        public static void SaveGuildSettings(string guildId, GuildSettings settings)
            => WriteFile(Path.Combine(GetDatabasePath(guildId), "settings.json"), JsonConvert.SerializeObject(settings));

        public static ConfigModel GetConfig()
            => JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "database", "config.json")));

        public static void SaveConfig(ConfigModel config)
            => WriteFile(Path.Combine(Directory.GetCurrentDirectory(), "database", "config.json"), JsonConvert.SerializeObject(config));

        static void WriteFile(string path, string data)
        {
            lock (fileLock)
            {
                File.WriteAllText(path, data);
            }
        }
    }
}
