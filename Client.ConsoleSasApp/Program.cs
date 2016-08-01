using App.Services.Security.Implementation;
using Interfaces.DataServices;
using Interfaces.SecurityServices;
using Microsoft.Azure;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ConsoleSasApp
{
    class Program
    {
        static void Main(string[] args)
        {

            GetImage();
            Console.ReadLine();
        }

        static async Task<string> GetToken()
        {
            string authority = "https://login.windows.net/demolimoges.onmicrosoft.com";
            string resource = "https://demolimoges.onmicrosoft.com/SASWebAPI";
            string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
            Uri redirectUri = new Uri(ConfigurationManager.AppSettings["ida:RedirectUri"]);

            ISecurityService securityService = new SecurityService();
            //var authResult = await securityService.GetToken(authority, resource, clientId, ConfigurationManager.AppSettings["ida:RedirectUri"]);

            var sas = await securityService.GetSas(authority, resource, clientId, ConfigurationManager.AppSettings["ida:RedirectUri"], "medias");

            return sas;
        }

        static async void GetImage()
        {
            var sas = await GetToken();
            var creds = new StorageCredentials(sas);
            var client = new CloudBlobClient(new Uri("https://dmostorage.blob.core.windows.net"), creds);

            CloudBlobContainer container = client.GetContainerReference("medias");

            var connnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("ResourceManager.png");
            var stream = new MemoryStream();

            await blockBlob.DownloadToStreamAsync(stream);

            //IDataServices dataService = new DataServices();

            //var image = await dataService.GetImage(connnectionString, "ResourceManager.png");
        }

    }
}
