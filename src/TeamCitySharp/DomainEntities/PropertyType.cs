using Newtonsoft.Json;

namespace TeamCitySharp.DomainEntities
{
  public class PropertyType
  {
    public PropertyType()
    {
    }

    public PropertyType(string rawValue)
    {
      RawValue = rawValue;
    }

    public override string ToString()
    {
      return RawValue;
    }

    [JsonProperty("rawValue")]
    public string RawValue { get; set; }
  }
}