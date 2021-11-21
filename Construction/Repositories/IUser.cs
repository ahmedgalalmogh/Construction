using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Construction.Models;


namespace Construction.Repositories
{
    public interface IUser
    {
        Task<IEnumerable<User>> Get();
        Task<User> Get(int id);
        Task<User> Create(User user);
        Task<User> GetUser(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
