using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ArthurDoc.Client.Models
{
    public class SendingClientConfig
    {
        [JsonProperty("authorizationHeader")]
        public string AuthHeader { get; set; }
        [JsonProperty("authorizationToken")]
        public string AuthToken { get; set; }

        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }
    }
}
