using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ArthurDoc.Client.DTO;
using ArthurDoc.Client.Models;
using log4net;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace ArthurDoc.Client
{
    public class Core
    {

        private static Core _instance;
        public static Core Instance => _instance ?? (_instance = new Core());

        private readonly ClientConfiguration _configuration;
        private static readonly ILog _logger = log4net.LogManager.GetLogger("ArthurDocClientConnector");

        public Core()
        {
            DirectoryInfo dI = new DirectoryInfo(Directory.GetCurrentDirectory());
            var configJson = File.ReadAllText($@"{dI.FullName}\connectorConfig.json");
            _configuration = JsonConvert.DeserializeObject<ClientConfiguration>(configJson);
        }

        public async Task<string> SendNewJobRequest(Callbacks.ArthurDocJobCommunicationStatus callback, NewReqestParameters requestParameters, string requestName, bool mergeFile)
        {
            try
            {
                callback?.Invoke("Creating request", "Orange");
                var client = GetHttpClient();

                NewJobRequestBase data;

                if (requestParameters.IsXml)
                {
                    data = new NewJobRequestJson
                    {
                        Name = requestName,
                        TemplateId = requestParameters.TemplateGuid,
                        File = requestParameters.XmlBody,
                        MergeFiles = mergeFile
                    };
                }
                else if (requestParameters.IsJson)
                {
                    data = new NewJobRequestJson
                    {
                        Name = requestName,
                        TemplateId = requestParameters.TemplateGuid,
                        File = requestParameters.JsonBody,
                        MergeFiles = mergeFile
                    };
                }
                else
                {
                    data = new NewJobRequestBase64
                    {
                        Name = requestName,
                        TemplateId = requestParameters.TemplateGuid,
                        File = requestParameters.FileContent,
                        MergeFiles = mergeFile
                    };
                }

                JsonSerializerSettings serializerSettings = new JsonSerializerSettings
                {
                    StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
                    Culture = CultureInfo.CurrentUICulture,
                    Formatting = Formatting.Indented
                };
                var jsonToSend = JsonConvert.SerializeObject(data, serializerSettings);

                var content = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                callback?.Invoke("Sending request", "Orange");
                var response = await client.PostAsync(_configuration.Endpoints.CreateJobPath, content);
                var deserializedObject = JsonConvert.DeserializeObject<NewJobResponse>(response.Content.ReadAsStringAsync().Result);
                callback?.Invoke("A request has been successfully submitted.", "Green");
                return deserializedObject.JobId;

            }
            catch (Exception e)
            {
                callback?.Invoke("A error occurrs in sending proces to ArthurDoc.", "Red");
                _logger.Error($"Error in SendNewJobRequest: {e.Message}");
                return "err0000000000000000000";
            }

        }

        public async Task<bool> GetJobStatus(Callbacks.ArthurDocJobCommunicationStatus callback, string jobId)
        {
            callback?.Invoke("Checking job status...", "Orange");
            var client = GetHttpClient();
            var url = new StringBuilder(_configuration.Endpoints.JobStatusPath).Append(jobId);

            var result = await client.GetStringAsync(url.ToString());
            var res = JsonConvert.DeserializeObject<JobStatusResponse>(result);
            if (res.Status.Equals("ready"))
                callback?.Invoke("Job finished.", "Green");
            else
                callback?.Invoke("Job is current in process. Checking again after 2 seconds.", "Orange");
            return res.Status.Equals("ready");
        }

        public async Task<List<DownloadedFile>> GetJobResult(Callbacks.ArthurDocJobCommunicationStatus callback, string jobId)
        {
            var files = new List<DownloadedFile>();
            try
            {
                callback?.Invoke("Getting a job result.", "Orange");
                var client = GetHttpClient();
                var url = new StringBuilder(_configuration.Endpoints.JobResultPath).Append(jobId);
                var result = await client.GetStringAsync(url.ToString());
                var res = JsonConvert.DeserializeObject<JobResultResponse>(result);
                callback?.Invoke("Successfully download information about generated files.", "Orange");
                var i = 1;
                foreach (var re in res.Files)
                {
                    var file = new DownloadedFile
                    {
                        Name = re.File.Name
                    };
                    callback?.Invoke($"Downloading file {i++} from {res.Files.Count}.", "Orange");
                    var fileContent = await client.GetByteArrayAsync(re.File.Url);
                    file.FileContent = fileContent;
                    files.Add(file);
                }
                return files;
            }
            catch (Exception e)
            {
                _logger.Error($"Error in SendNewJobRequest: {e.Message}");
                callback?.Invoke("A error occurrs in getJobResult proces.", "Red");
                return null;
            }


        }


        public HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(_configuration.ClientConfig.AuthHeader, _configuration.ClientConfig.AuthToken);
            client.BaseAddress = new Uri(_configuration.ClientConfig.BaseUrl);
            return client;
        }
    }
}
