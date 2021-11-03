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
    public class PhoneController : ControllerBase
    {
        private readonly IPhoneNumber _phoneRepository;

        public PhoneController(IPhoneNumber Iphone)
        {
            _phoneRepository = Iphone;
        }
        [HttpGet]
        public async Task<IEnumerable<PhoneNumber>> GetPhones()
        {
            return await _phoneRepository.Get();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneNumber>> GetPhones(int id)
        {
            return await _phoneRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<PhoneNumber>> CreatePhone([FromBody] PhoneNumber phone)
        {
            var newPhone = await _phoneRepository.Create(phone);
            return CreatedAtAction((nameof(GetPhones)), new { id = newPhone.Id }, newPhone);
        }
        [HttpPut]

        public async Task<ActionResult> PutPhone(int id, [FromBody] PhoneNumber phone)
        {
            if (id != phone.Id)
            {
                return BadRequest("Id doesn't match");
            }
            await _phoneRepository.Update(phone);
            return NoContent();
        }
        [HttpDelete]

        public async Task<ActionResult> DeletePhone(int id)
        {
            var phone = await _phoneRepository.Get(id);
            if (phone == null)
            {
                return BadRequest("Phone Not Found in Our DB");
            }
            await _phoneRepository.Delete(id);
            return NoContent();
        }
    }
}
