using Microsoft.EntityFrameworkCore;
using SistemaLocLab.Domain.Entities;
using SistemaLocLab.Infrastructure.Context;
using SistemaLocLab.Infrastructure.Repositories.Interfaces;
using SistemaLocLab.Infrastructure.Context;

namespace SistemaLocLab.Infrastructure.Repositories.Implementations
{
    public class SoftwareRepository :
        ISoftwareRepository
    {
        private readonly ApplicationDbContext _context;

        public SoftwareRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(
            Software software)
        {
            await _context.Softwares.AddAsync(software);

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(
            Software software)
        {
            _context.Softwares.Update(software);

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var software =
                await ObterPorIdAsync(id);

            if (software == null)
                return;

            _context.Softwares.Remove(software);

            await _context.SaveChangesAsync();
        }

        public async Task<Software?>
            ObterPorIdAsync(Guid id)
        {
            return await _context.Softwares
                .FirstOrDefaultAsync(
                    x => x.IdSoftware == id);
        }

        public async Task<IEnumerable<Software>>
            ObterTodosAsync()
        {
            return await _context.Softwares
                .ToListAsync();
        }
    }
}