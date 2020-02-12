using Newtonsoft.Json;

namespace ArthurDoc.Client.DTO
{
    public class NewJobRequestJson : NewJobRequestBase
    {
        [JsonProperty("file")]
        public new object File { get; set; }
    }
}
