using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ArthurDoc.Client.Models
{
    public class ClientConfiguration
    {
        [JsonProperty("endpoints")]
        public ArthurDocEndpoints Endpoints { get; set; }

        [JsonProperty("clientConfig")]
        public SendingClientConfig ClientConfig { get; set; }
    }
}
