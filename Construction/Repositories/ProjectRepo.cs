using Construction.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repositories
{
    public class ProjectRepo:IProject
    {
        private readonly ConstructionContext _context;
        public ProjectRepo(ConstructionContext context)
        {
            _context = context;
        }
        public async Task<PoneNumber> Create(PoneNumber project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task Delete(int id)
        {
            PoneNumber projectToDelete =(PoneNumber) await _context.projects.FindAsync(id);
             _context.Remove(projectToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PoneNumber>> Get()
        {
            return await _context.projects.Include(b => b.Buildings).AsNoTracking().ToListAsync();
        }

        public async Task<PoneNumber> Get(int id)
        {
            return await _context.projects.Include(b => b.Buildings).AsNoTracking().FirstOrDefaultAsync(b=>b.Id==id);
        }

        public async Task Update(PoneNumber project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
