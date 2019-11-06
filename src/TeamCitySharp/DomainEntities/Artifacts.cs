using System.Collections.Generic;
using Newtonsoft.Json;

namespace TeamCitySharp.DomainEntities
{
  public class Artifacts
  {
    public override string ToString()
    {
      return "artifacts";
    }

    [JsonProperty("href")]
    public string Href { get; set; }

    [JsonProperty("file")]
    public List<Artifact> Files { get; set; }
  }
}