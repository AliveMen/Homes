using System.Threading.Tasks;
using Homes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homes.Controllers
{
    using Interfaces;

    [ApiController]
    [Route("tenants")]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<Tenant[]>> SearchTenants(TenantSearchCriteria criteria)
        {
            var result = await _tenantService.SearchAsync(criteria);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Tenant[]>> GetById(string id)
        {
            var result = await _tenantService.GetByIdsAsync(new []{id});
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            await _tenantService.DeleteAsync(new []{id});
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Save([FromBody] Tenant tenant)
        {
            var result = await _tenantService.SaveAsync(new Tenant[] { tenant });
            return Ok(result);
        }
    }
}