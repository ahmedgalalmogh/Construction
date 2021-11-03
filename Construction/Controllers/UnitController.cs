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
    public class UnitController : ControllerBase
    {
        private readonly IUnit _unitRepository;

        public UnitController(IUnit unit)
        {
            _unitRepository = unit;
        }
        [HttpGet]
        public async Task<IEnumerable<Unit>> GetUnits()
        {
            return await _unitRepository.Get();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnits(int id)
        {
            return await _unitRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> CreateUnit([FromBody] Unit unit)
        {
            var newUnit = await _unitRepository.Create(unit);
            return CreatedAtAction((nameof(GetUnits)), new { id = newUnit.Id }, newUnit);
        }
        [HttpPut]

        public async Task<ActionResult> PutUnit(int id, [FromBody] Unit unit)
        {
            if (id != unit.Id)
            {
                return BadRequest("Id doesn't match");
            }
            await _unitRepository.Update(unit);
            return NoContent();
        }
        [HttpDelete]

        public async Task<ActionResult> DeleteUnit(int id)
        {
            var unit = await _unitRepository.Get(id);
            if (unit == null)
            {
                return BadRequest("unit Not Found in Our DB");
            }
            await _unitRepository.Delete(id);
            return NoContent();
        }
    }
}
