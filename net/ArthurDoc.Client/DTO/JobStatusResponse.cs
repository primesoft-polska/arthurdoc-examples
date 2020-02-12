using Newtonsoft.Json;

namespace ArthurDoc.Client.DTO
{
    public class JobStatusResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
