using Newtonsoft.Json;

namespace Thread_Racing_Game.Core.Models
{
    public class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("buff")]
        public Buff Buff { get; set; }

    }

}
