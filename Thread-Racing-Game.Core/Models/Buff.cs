using Newtonsoft.Json;

namespace Thread_Racing_Game.Core.Models
{
    public class Buff
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("multiplier")]
        public double Multiplier { get; set; }
    }
}
