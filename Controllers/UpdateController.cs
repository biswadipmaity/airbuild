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
        
        public async Task<IActionResult> Index()
        {
            var clientVersion = Request.Headers[versionHeaderKey];
            var macID = Request.Headers[macHeaderKey];
            if(string.IsNullOrWhiteSpace(clientVersion))
            {
                return StatusCode(404);
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=airbuildppe;AccountKey=HXl2xZ1C05Q2gOSiL66JJKwj7VPWQ5DkeGRbk5a1HAPPredw5zsLzNr3hc/Gl7HSqB3PjYbdWDWmu1FOcRYFVQ==;EndpointSuffix=core.windows.net");

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
                return StatusCode(304);
            }

            var device = new Device(macID,Database.getNickName(macID), Database.isPPDevice(macID), version.latestVersion);
            Database.Devices.Add(device);

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
