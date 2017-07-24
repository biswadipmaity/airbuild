using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Server.Controllers
{
    public class UpdateController : Controller
    {
        private const string latestVersion = "001";

        private const string versionHeaderKey = "x-ESP8266-version";

        public async Task<IActionResult> Index()
        {
            var clientVersion = Request.Headers[versionHeaderKey];
            if(string.IsNullOrWhiteSpace(clientVersion))
            {
                return StatusCode(404);
            }

            if(latestVersion.Equals(clientVersion))
            {
                return StatusCode(304);
            }


            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=airbuild;AccountKey=EkWjwGOb3AsrVcONZfkLhulHjCUM24wi9oVdoLYfo6QLZUyjPY93ql1Tz3q7ytw84axfi47H8hEg4MSyYd/Y1g==;EndpointSuffix=core.windows.net");

            //  create a blob client.
            var blobClient = storageAccount.CreateCloudBlobClient();

            //  create a container 
            var container = blobClient.GetContainerReference("build");

            //  create a block blob
            var blockBlob = container.GetBlobReference("build_001.bin");


            byte[] res;
            using (var memoryStream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(memoryStream);
                res = memoryStream.ToArray();
            }

            return new FileStreamResult(new MemoryStream(res), "application/octet-stream");
        }
    }
}
