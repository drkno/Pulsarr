using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pulsarr.Model.Updates;

namespace Pulsarr.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class UpdateController : Controller
    {
        [HttpGet("{branch}/changes")]
        public async Task<IEnumerable<UpdatePackage>> GetChangeList(string branch)
        {
            return new List<UpdatePackage>();
        }

        [HttpGet("{branch}")]
        public async Task<UpdatePackageAvailable> CheckUpdate(string branch)
        {
            return null;
        }
    }
}