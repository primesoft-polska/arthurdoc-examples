using System.Collections.Generic;
using ArthurDoc.Client.Models;
using Newtonsoft.Json;

namespace ArthurDoc.Client.DTO
{
    public class JobResultResponse
    {
        [JsonProperty("files")]
        public List<ArthurFile> Files { get; set; }
    }
}
