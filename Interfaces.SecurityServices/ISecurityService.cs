using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.SecurityServices
{
    public interface ISecurityService
    {
        Task<string> GetShareAccessSignatureForBlobContainer(string containerName, string policyName);
        Task<AuthenticationResult> GetToken(string authority, string resource, string clientId, string redirectUri);
        Task<string> GetSas(string authority, string resource, string clientId, string redirectUri, string containerName, string policyName = null);
    }
}
