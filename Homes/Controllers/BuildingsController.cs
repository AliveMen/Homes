using System.Threading.Tasks;
using Homes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homes.Controllers
{
    using Homes.Interfaces;

    [ApiController]
    [Route("buildings")]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingsController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<Building[]>> Search(SearchCriteria criteria)
        {
            var result = await _buildingService.SearchAsync(criteria);
            return Ok(result);
        }

        /// <summary>
        /// Return building by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Building>> GetByIds(string id)
        {
            var result = await _buildingService.GetByIdsAsync(new [] {id});
            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(string id)
        {
            await _buildingService.DeleteAsync(new []{id});
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Building), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Building>> Create([FromBody] Building building)
        {
            var result = await this._buildingService.SaveAsync(new[] {building});

            return Ok(building);
        }
    }
}