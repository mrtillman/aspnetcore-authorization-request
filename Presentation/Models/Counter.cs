using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace AuthDemo.Models
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