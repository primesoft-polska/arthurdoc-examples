using Newtonsoft.Json;

namespace ArthurDoc.Client.DTO
{
    public class NewJobResponse
    {
        [JsonProperty("jobguid")]
        public string JobId { get; set; }
    }
}
