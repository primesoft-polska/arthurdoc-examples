using Newtonsoft.Json;

namespace ArthurDoc.Client.Models
{
    public class FileInfo
    {
        [JsonProperty("file")]
        public FileDetails File { get; set; }
    }
}
