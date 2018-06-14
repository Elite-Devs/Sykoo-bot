using Newtonsoft.Json;

namespace Sykoo.Models
{
    public class ConfigModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
    }
}
