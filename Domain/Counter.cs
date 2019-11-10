using Newtonsoft.Json;

namespace Domain
{
  public class Counter
  {
    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("value")]
    public long Value { get; set; }

    [JsonProperty("skip")]
    public long Skip { get; set; }
  }
}
