using Newtonsoft.Json.Linq;

namespace ArthurDoc.Client.Models
{
    public class NewReqestParameters
    {
        public bool IsJson { get; set; }
        public JObject JsonBody { get; set; }
        public byte[] FileContent { get; set; }
        public string Path { get; set; }

        public string TemplateGuid { get; set; }

        public bool MergeResultFiles { get; set; }
    }
}
