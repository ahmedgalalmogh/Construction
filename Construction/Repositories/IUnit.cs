using Construction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repositories
{
    public interface IUnit
    {

        Task<IEnumerable<Unit>> Get();
        Task<Unit> Get(int id);
        Task<Unit> Create(Unit unit);
        Task Update(Unit unit);
        Task Delete(int id);
    }
}
