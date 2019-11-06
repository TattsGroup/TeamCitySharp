using Newtonsoft.Json;

namespace TeamCitySharp.DomainEntities
{
    public class BuildFeature
    {
        public BuildFeature()
        {
            Properties = new Properties();
        }

        public override string ToString()
        {
            return Type;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }
}