using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace FunctionApp1
{
    public class Function1
    {
        [FunctionName("funblobrawtigger")]
        [return: ServiceBus("hl7-queue", Connection = "AzureWebJobsServiceBus")]
        public static string Run(
            [BlobTrigger("raw/{name}", Connection = "teststorageaccountadf_STORAGE")] Stream inputBlob,
            [Blob("hl7/{name}", FileAccess.Write)] Stream outputBlob,
            string name,
            ILogger log)
        {
            try
            {
                var len = inputBlob.Length;

                //send file from jurisdiction/ga to hl7/jurisdiction/ga
                log.LogInformation("Send file to hl7");
                FileInfo fileInfo = new FileInfo { FileName = name, FilePath = "raw", FileSize = inputBlob.Length };
                //inputBlob.CopyTo(outputBlob);
                log.LogInformation("Send file to hl7 completed");

                log.LogInformation("Send fileinfo to service bus queue");
                string jsonString = JsonSerializer.Serialize(fileInfo);
                string body = string.Empty;
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                using (var reader = new StreamReader(stream))
                {
                    body = reader.ReadToEnd();
                    log.LogInformation($"Message body : {body}");
                }
                log.LogInformation($"SendMessage processed.");
                return body;
            }
            catch (Exception ex)
            {
                return ex.Message;
            } 

        }
    }

    public class FileInfo
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }

    }
}
