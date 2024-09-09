using System;
using Newtonsoft.Json;

namespace crud_practice.Models;

public class Document
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [JsonProperty("available")]
    public bool Available { get; set; }
    [JsonProperty("topic")]
    public string Topic { get; set; }
}
