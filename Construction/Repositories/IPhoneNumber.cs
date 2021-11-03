using Construction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repositories
{
    public interface IPhoneNumber
    {
        Task<IEnumerable<PhoneNumber>> Get();
        Task<PhoneNumber> Get(int id);
        Task<PhoneNumber> Create(PhoneNumber number);
        Task Update(PhoneNumber number);
        Task Delete(int id);
    }
}
