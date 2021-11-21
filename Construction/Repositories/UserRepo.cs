using Construction.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repositories
{
    public class UserRepo:IUser
    {
        private readonly ConstructionContext _context;
        public UserRepo(ConstructionContext context)
        {
            _context = context;
        }
        public async Task<User> Create(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(int id)
        {
            var userToDelete = _context.users.FindAsync(id);
            _context.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> Get()
        {
            return  _context.users.ToList();
        }

        public async Task<User> Get(int id)
        {
            return await _context.users.FindAsync( id);
        }

        public async Task<User> GetUser(User user)
        {
            return await  _context.users.FirstOrDefaultAsync((e) => e.Email == user.Email && e.Password == user.Password);
             
        }

        public async Task Update(User user)
        {

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }



    }
}
