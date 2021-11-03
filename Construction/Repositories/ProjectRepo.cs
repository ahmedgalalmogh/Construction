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
        public async Task<Project> Create(Project project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task Delete(int id)
        {
            Project projectToDelete =(Project) await _context.projects.FindAsync(id);
             _context.Remove(projectToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project>> Get()
        {
            return await _context.projects.Include(b => b.Buildings).AsNoTracking().ToListAsync();
        }

        public async Task<Project> Get(int id)
        {
            return await _context.projects.Include(b => b.Buildings).AsNoTracking().FirstOrDefaultAsync(b=>b.Id==id);
        }

        public async Task Update(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
