using Newtonsoft.Json;

namespace ArthurDoc.Client.DTO
{
    public class JobResultRequest
    {
        [JsonProperty("guid")]
        public string JobId { get; set; }
    }
}
