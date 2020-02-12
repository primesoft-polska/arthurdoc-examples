using Newtonsoft.Json;

namespace ArthurDoc.Client.DTO
{
    public class NewJobRequestBase64: NewJobRequestBase
    {
        [JsonProperty("file")]
        public new byte[] File { get; set; }

    }
}
