using Construction.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repositories
{
    public class PhoneNumberRepo:IPhoneNumber
    {
        private readonly ConstructionContext _context;
        public PhoneNumberRepo(ConstructionContext context)
        {
            _context = context;
        }
        public async Task<PhoneNumber> Create(PhoneNumber phoneNumber)
        {
            _context.Add(phoneNumber);
            await _context.SaveChangesAsync();
            return phoneNumber;
        }

        public async Task Delete(int id)
        {
            PhoneNumber PhoneNumberToDelete = (PhoneNumber)await _context.phoneNumbers.FindAsync(id);
            _context.Remove(PhoneNumberToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PhoneNumber>> Get()
        {
            return await _context.phoneNumbers.ToListAsync();
        }

        public async Task<PhoneNumber> Get(int id)
        {
            return await _context.phoneNumbers.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task Update(PhoneNumber phone)
        {
            _context.Entry(phone).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
