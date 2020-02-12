using Newtonsoft.Json;

namespace ArthurDoc.Client.DTO
{
    public abstract class NewJobRequestBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("templateguid")]
        public string TemplateId { get; set; }
        [JsonProperty("file")]
        public string File { get; set; }
        [JsonProperty("merge_pdf")]
        public bool MergeFiles { get; set; }
    }
}
