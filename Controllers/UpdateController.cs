using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Server.Helper;
using Server.TypeDefinition;

namespace Server.Controllers
{
    public struct VersionSchema{
        public string latestVersion;

        public string path;
    };
    public class UpdateController : Controller
    {
        private const string versionHeaderKey = "x-ESP8266-version";
        private const string macHeaderKey = "x-ESP8266-STA-MAC";

        private const string StagingBlob = "DefaultEndpointsProtocol=https;AccountName=airbuildppe;AccountKey=HXl2xZ1C05Q2gOSiL66JJKwj7VPWQ5DkeGRbk5a1HAPPredw5zsLzNr3hc/Gl7HSqB3PjYbdWDWmu1FOcRYFVQ==;EndpointSuffix=core.windows.net";
        
        private const string ProdBlob = "DefaultEndpointsProtocol=https;AccountName=airbuildprod;AccountKey=+Fb9XlmUDwjDYNQeG1zHqZrqreEdZ5bqbYY0PATDvqGYx4OQo9ec5+AXlhuNolR7tAqhu0j5Zv5ybhNT8MYONQ==;EndpointSuffix=core.windows.net";

        public async Task<IActionResult> Index()
        {
            var clientVersion = Request.Headers[versionHeaderKey];
            var macID = Request.Headers[macHeaderKey];
            if(string.IsNullOrWhiteSpace(clientVersion))
            {
                return StatusCode(404);
            }

            if(!Database.hasMacId(macID))
            {
                var device = new Device(macID, "-", false, clientVersion);
                Database.Devices.Add(device);
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Database.isPPDevice(macID) ? ProdBlob : StagingBlob);

            //  create a blob client.
            var blobClient = storageAccount.CreateCloudBlobClient();

            //  create a container 
            var container = blobClient.GetContainerReference("hackathonproject");

            //  create a block blob
            var versionBlob = container.GetBlobReference("version.json");

            VersionSchema version;
            using (var jsonStream = new MemoryStream())
            {
                await versionBlob.DownloadToStreamAsync(jsonStream);
                jsonStream.Seek(0, SeekOrigin.Begin);
                version = CreateFromJsonStream<VersionSchema>(jsonStream);
            }

            if(clientVersion.ToString().Equals(version.latestVersion))
            {
                if(Database.getStatus(macID) == Device.DeviceStatus.UpdateStarted)    
                {
                    Database.setStatus(macID,  Device.DeviceStatus.UpdateFinished);   
                    Database.updateVersion(macID,clientVersion);
                }
                else if(Database.getStatus(macID) == Device.DeviceStatus.UpdateStarted)    
                {
                    Database.setStatus(macID,  Device.DeviceStatus.Idle);
                }
                
                return StatusCode(304);
            }

            Database.setStatus(macID,  Device.DeviceStatus.UpdateStarted);
            var firmwareBlob = container.GetBlobReference(version.path);
            var memoryStream = new MemoryStream();
            await firmwareBlob.DownloadToStreamAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
        
            HttpContext.Response.Headers.Add("Content-Length", memoryStream.Length.ToString());
            return new FileStreamResult(memoryStream, "application/octet-stream");
        }

        public T CreateFromJsonStream<T>(Stream stream)
        {
            JsonSerializer serializer = new JsonSerializer();
            T data;
            using (StreamReader streamReader = new StreamReader(stream))
            {
                data = (T)serializer.Deserialize(streamReader, typeof(T));
            }
            return data;
        }
    }
}
