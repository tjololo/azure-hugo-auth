using System.IO;
using System.Net.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace AuthProxy
{
    public static class AuthProxy
    {
        [FunctionName("AuthProxy")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{*path}")] HttpRequest req,
            ILogger log, string path)
        {
            // Authorization - allow only @myorg.com users
            string user = "";
            if (req.Headers.ContainsKey("X-MS-CLIENT-PRINCIPAL-NAME")) {
                user = req.Headers["X-MS-CLIENT-PRINCIPAL-NAME"];
            }
            string[] allowesUsers = System.Environment.GetEnvironmentVariable("ALLOWED_USERS").Trim().Split(",");
            if (allowesUsers.Length > 0 && Array.IndexOf(allowesUsers, user) == -1)
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            var blobStorageURI = System.Environment.GetEnvironmentVariable("BLOB_SERVICE_ENDPOINT", EnvironmentVariableTarget.Process);
            var container = System.Environment.GetEnvironmentVariable("BLOB_CONTAINER", EnvironmentVariableTarget.Process);
            var authKey = System.Environment.GetEnvironmentVariable("BLOB_ACCESS_STRING", EnvironmentVariableTarget.Process);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(blobStorageURI);
                string postfix = "";
                if (path == null || path.EndsWith("/") || path == "")
                {
                    postfix = "index.html";
                }
                var storagePath = "/" + container + "/" + path + postfix + authKey;
                return await client.GetAsync(storagePath);
            }
        }
    }
}
