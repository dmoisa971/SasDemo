using Interfaces.DataServices;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Implementations
{
    public class DataServices : IDataServices
    {
        public async Task<Stream> GetImage(string connectionString, string imageName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("medias");
            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
            var stream = new MemoryStream();

            await blockBlob.DownloadToStreamAsync(stream);

            return stream;
        }

    }
}
