using Newtonsoft.Json;

namespace CosmosTodoApi.Models
{
    public class QAItem
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("question")]
        public string? Question { get; set; }

        [JsonProperty("answer")]
        public string? Answer { get; set; }
    }
}