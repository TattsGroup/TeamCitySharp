using Newtonsoft.Json;

namespace TeamCitySharp.DomainEntities
{
  public class HrefWrapper
  {
    [JsonProperty("href")]
    public string Href { get; set; }
  }
}
