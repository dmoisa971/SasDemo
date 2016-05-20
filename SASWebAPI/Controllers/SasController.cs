using App.Services.Security.Implementation;
using Interfaces.SecurityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SASWebAPI.Controllers
{
    public class SasController : ApiController
    {
        // GET: api/Sas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [Authorize]
        [HttpGet]
        [Route("~/api/sas/blobcontainer/{containerName}")]
        public async Task<IHttpActionResult> GetSasforBlobContainer([FromUri]string containerName, string policyName = null)
        {
            ISecurityService securityService = new SecurityService();
            string sas;
            try
            {
                sas = await securityService.GetShareAccessSignatureForBlobContainer(containerName, policyName);
            }
            catch (Exception ex)
            {
                return
                    BadRequest(
                        string.Format(
                            "Erreur lors de la récupération de la Shared Access Signature pour le container: {0} erreur: {1}.",
                            containerName, ex.Message));
            }

            return Ok(sas);
        }

    }
}
