using Newtonsoft.Json;

namespace Sykoo.Models
{
    public class GuildSettings
    {
        [JsonProperty("single_message_channel")]
        public ulong SingleMessageChannel { get; set; }
    }
}
