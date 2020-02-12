using Newtonsoft.Json;

namespace ArthurDoc.Client.DTO
{
    public class JobStatusRequest
    {
        [JsonProperty("guid")]
        public string Id { get; set; }
    }
}
