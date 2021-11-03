using Construction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repositories
{
    public  interface IProject
    {

        Task<IEnumerable<Project>> Get();
        Task<Project> Get(int id);
        Task<Project> Create(Project project);
        Task Update(Project project);
        Task Delete(int id);
    }
}
