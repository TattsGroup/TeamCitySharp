using Newtonsoft.Json;
using System.Collections.Generic;

namespace TeamCitySharp.DomainEntities
{
    public class BuildFeatures
    {
        public override string ToString()
        {
            return "features";
        }

        [JsonProperty("feature")]
        public List<BuildFeature> Feature { get; set; }
    }
}