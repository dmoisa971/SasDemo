using App.Services.Security.Implementation;
using Interfaces.DataServices;
using Interfaces.SecurityServices;
using Microsoft.Azure;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ConsoleSasApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GetToken();
            Console.ReadLine();
        }

        static async void GetToken()
        {
            string authority = "https://login.windows.net/demolimoges.onmicrosoft.com";
            string resource = "https://demolimoges.onmicrosoft.com/SASWebAPI";
            string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
            Uri redirectUri = new Uri(ConfigurationManager.AppSettings["ida:RedirectUri"]);

            ISecurityService securityService = new SecurityService();
            //var authResult = await securityService.GetToken(authority, resource, clientId, ConfigurationManager.AppSettings["ida:RedirectUri"]);

            var sas = await securityService.GetSas(authority, resource, clientId, ConfigurationManager.AppSettings["ida:RedirectUri"], "medias");

            //AuthenticationContext authContext = new AuthenticationContext(authority);
            //AuthenticationResult authResult = await authContext.AcquireTokenAsync(resource, clientId, redirectUri, new PlatformParameters(PromptBehavior.Auto));
        }

        static async void GetImage()
        {
            var connnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            //IDataServices dataService = new DataServices();

            //var image = await dataService.GetImage(connnectionString, "ResourceManager.png");
        }

    }
}
