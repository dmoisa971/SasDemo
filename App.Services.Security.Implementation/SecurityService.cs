using Interfaces.SecurityServices;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Net.Http.Headers;

namespace App.Services.Security.Implementation
{
    public class SecurityService : ISecurityService
    {
        public async Task<string> GetSas(string authority, string resource, string clientId, string redirectUri, string containerName, string policyName)
        {
            var authResult = await this.GetToken(authority, resource, clientId, redirectUri);
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    authResult.AccessTokenType, authResult.AccessToken);

            Uri requestURI = new Uri(string.Format("https://localhost:44300/api/sas/blobcontainer/{0}", containerName));
            HttpResponseMessage httpResponse = await httpClient.GetAsync(requestURI);

            return httpResponse.Content.ToString();
        }

        /// <summary>
        /// Méthode permettant de récupérer une Shared Access Signature pour se connecter à une ressource de type blob
        /// </summary>
        /// <param name="containerName">Nom du container contenant le blob</param>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public string GetShareAccessSignatureForBlobContainer(string containerName, string policyName, string storageConnectionString)
        {
            
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

            CloudBlobClient client = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(containerName);

            string sas = container.GetSharedAccessSignature(
                new SharedAccessBlobPolicy()
                {
                    SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
                },
                policyName);

            return sas;
        }

        public async Task<AuthenticationResult> GetToken(string authority, string resource, string clientId, string redirectUri)
        {
            Uri uri = new Uri(redirectUri);
            AuthenticationContext authContext = new AuthenticationContext(authority);
            AuthenticationResult authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, new PlatformParameters(PromptBehavior.Auto));

            return authResult;

        }
    }
}
