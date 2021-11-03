using Construction.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repositories
{
    public class BuildingRepo : IBuilding
    {
        private readonly ConstructionContext _context;
        public BuildingRepo(ConstructionContext context)
        {
            _context = context;
        }
        public async Task<Building> Create(Building building)
        {
            _context.Add(building);
            await  _context.SaveChangesAsync();
            return building;
        }

        public async Task Delete(int id)
        {
            var BuilingToDelete = _context.buildings.FindAsync(id);
            _context.Remove(BuilingToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Building>> Get()
        {
            return await _context.buildings.Include(b=>b.project).AsNoTracking().ToListAsync();
        }

        public async Task<Building> Get(int id)
        {
            return await _context.buildings.Include(b=>b.project).AsNoTracking().FirstOrDefaultAsync(b=>b.Id==id);
        }

        public async Task Update(Building building)
        {
            _context.Entry(building).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
