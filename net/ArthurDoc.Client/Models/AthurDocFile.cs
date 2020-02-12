using Newtonsoft.Json;

namespace ArthurDoc.Client.Models
{
    public class ArthurFile
    {
        [JsonProperty("file")]
        public FileDetails File { get; set; }
    }

    public class FileDetails
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

}
