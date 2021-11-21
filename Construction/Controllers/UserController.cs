using Construction.Models;
using Construction.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Construction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepository;

        public UserController(IUser user)
        {
            _userRepository = user;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.Get();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsers(int id)
        {
            return await _userRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromForm] User user)
        {
            var newUnit = await _userRepository.GetUser(user);
            return CreatedAtAction((nameof(GetUsers)), new { id = newUnit.Id }, newUnit);
        }
       // [HttpPost]
        //public async Task<ActionResult<User>> GetUser([FromBody] string email,string password)
        //{
        //    //    var user = new User();
        //    //    user.Email = email;
        //    //    user.Password = password;
        //    //    var newUnit = await _userRepository.GetUser(user);
        //    //    return newUnit;
        //    throw Exception;
        //}
        [HttpPut]

        public async Task<ActionResult> PutUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest("Id doesn't match");
            }
            await _userRepository.Update(user);
            return NoContent();
        }
        [HttpDelete]

        public async Task<ActionResult> DeleteUser(int id)
        {
            var unit = await _userRepository.Get(id);
            if (unit == null)
            {
                return BadRequest("unit Not Found in Our DB");
            }
            await _userRepository.Delete(id);
            return NoContent();
        }
    }
}
