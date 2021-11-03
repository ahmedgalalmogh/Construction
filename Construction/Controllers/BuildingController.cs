using Construction.Models;
using Construction.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuilding _BuildingRepository;

        public BuildingController(IBuilding building)
        {
            _BuildingRepository = building;
        }
        [HttpGet]
        public async Task<IEnumerable<Building>> GetBuildings()
        {
            return await _BuildingRepository.Get();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuildings(int id)
        {
            return await _BuildingRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<Building>> CreateBuilding([FromBody] Building building)
        {
            var newBuilding = await _BuildingRepository.Create(building);
            return CreatedAtAction((nameof(GetBuildings)), new { id = newBuilding.Id }, newBuilding);
        }
        [HttpPut]

        public async Task<ActionResult> PutBuilding(int id, [FromBody] Building building)
        {
            if (id != building.Id)
            {
                return BadRequest("Id doesn't match");
            }
            await _BuildingRepository.Update(building);
            return NoContent();
        }
        [HttpDelete]

        public async Task<ActionResult> DeleteBuilding(int id)
        {
            var building = await _BuildingRepository.Get(id);
            if (building == null)
            {
                return BadRequest("building Not Found in Our DB");
            }
            await _BuildingRepository.Delete(id);
            return NoContent();
        }

    }
}
