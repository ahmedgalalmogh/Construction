using Construction.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repositories
{
    public class UnitRepo:IUnit
    {
        private readonly ConstructionContext _context;
        public UnitRepo(ConstructionContext context)
        {
            _context = context;
        }
        public async Task<Unit> Create(Unit unit)
        {
            _context.Add(unit);
            await _context.SaveChangesAsync();
            return unit;
        }

        public async Task Delete(int id)
        {
            var unitToDelete = _context.units.FindAsync(id);
            _context.Remove(unitToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Unit>> Get()
        {
            return await _context.units.Include(b => b.building).AsNoTracking().ToListAsync();
        }

        public async Task<Unit> Get(int id)
        {
            return await _context.units.Include(b => b.building).AsNoTracking().FirstOrDefaultAsync(b=>b.Id==id);
        }

        public async Task Update(Unit unit)
        {
            _context.Entry(unit).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

