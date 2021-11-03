using Construction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repositories
{
    public  interface IBuilding
    {
        Task<IEnumerable<Building>> Get();
        Task<Building> Get(int id);
        Task<Building> Create(Building building);
        Task Update(Building building);
        Task Delete(int id);

    }
}
