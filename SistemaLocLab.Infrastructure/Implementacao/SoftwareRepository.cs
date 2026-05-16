using Microsoft.EntityFrameworkCore;
namespace SistemaLocLab.Infrastructure.Repositories.Implementations
{
    public class SoftwareRepository : ISoftwareRepository
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
            await _context.Softwares
                .AddAsync(software);

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
                throw new Exception(
                    "Software não encontrado.");

            _context.Softwares.Remove(software);

            await _context.SaveChangesAsync();
        }

        public async Task<Software?> ObterPorIdAsync(Guid id)
        {
            return await _context.Softwares
                .Include(x => x.Laboratorios)
                .Include(x => x.Disciplinas)
                .FirstOrDefaultAsync(x =>
                    x.IdSoftware == id);
        }

        public async Task<IEnumerable<Software>> ObterTodosAsync()
        {
            return await _context.Softwares
                .ToListAsync();
        }
    }
}