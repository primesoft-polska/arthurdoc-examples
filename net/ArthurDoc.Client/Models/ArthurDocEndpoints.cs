using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ArthurDoc.Client.Models
{
    public class ArthurDocEndpoints
    {
        [JsonProperty("createJobPath")]
        public string CreateJobPath { get; set; }
        [JsonProperty("jobStatusPath")]
        public string JobStatusPath { get; set; }
        [JsonProperty("jobResultPath")]
        public string JobResultPath { get; set; }
    }
}
