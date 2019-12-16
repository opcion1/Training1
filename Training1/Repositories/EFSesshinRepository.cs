

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;

namespace Training1.Repositories
{
    public class EFSesshinRepository : ISesshinRepository
    {
        private readonly ProductContext _context;
        public EFSesshinRepository(ProductContext context)
        {
            _context = context;
        }

        public IQueryable<Sesshin> Sesshins => _context.Sesshin;

        public async Task<List<Sesshin>> ListAsync()
        {
            List<Sesshin> sesshins = await Sesshins.ToListAsync();

            foreach(Sesshin sesshin in sesshins)
            {

            }

            return sesshins;
        }

        public async Task<Sesshin> GetByIdAsync(int id)
        {
            return await Sesshins.FirstOrDefaultAsync(p => p.SesshinId == id);
        }

        public async Task AddAsync(Sesshin sesshin)
        {
            _context.Add(sesshin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sesshin sesshin)
        {
            _context.Update(sesshin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sesshin = await GetByIdAsync(id);
            _context.Sesshin.Remove(sesshin);
            await _context.SaveChangesAsync();
        }

        public bool SesshinExists(int id)
        {
            return Sesshins.Any(e => e.SesshinId == id);
        }
    }
}
