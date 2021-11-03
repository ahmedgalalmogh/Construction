using Construction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repositories
{
    public  interface IProject
    {

        Task<IEnumerable<PoneNumber>> Get();
        Task<PoneNumber> Get(int id);
        Task<PoneNumber> Create(PoneNumber project);
        Task Update(PoneNumber project);
        Task Delete(int id);
    }
}
