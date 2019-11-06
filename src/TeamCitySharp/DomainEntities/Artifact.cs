using Newtonsoft.Json;
using System;

namespace TeamCitySharp.DomainEntities
{
  public class Artifact
  {
    [JsonProperty("size")]
    public ulong Size { get; set; }

    [JsonProperty("modificationTime")]
    public DateTime ModificationTime { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("href")]
    public string Href { get; set; }
    
    [JsonProperty("content")]
    public HrefWrapper Content { get; set; }
    
    [JsonProperty("children")]
    public HrefWrapper Children { get; set; }

    public override string ToString()
    {
      return Name;
    }
  }
}
