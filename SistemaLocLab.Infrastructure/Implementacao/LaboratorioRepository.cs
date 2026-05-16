using Microsoft.EntityFrameworkCore;
using SistemaLocLab.Infrastructure.Context;
using SistemaLocLab.Infrastructure.Repositories.Interfaces;

namespace SistemaLocLab.Infrastructure.Repositories.Implementations
{
    public class LaboratorioRepository : ILaboratorioRepository
    {
        private readonly ApplicationDbContext _context;

        public LaboratorioRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(
            Laboratorios laboratorio)
        {
            await _context.Laboratorios
                .AddAsync(laboratorio);

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(
            Laboratorios laboratorio)
        {
            _context.Laboratorios
                .Update(laboratorio);

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var laboratorio =
                await ObterPorIdAsync(id);

            if (laboratorio == null)
                throw new Exception(
                    "Laboratório não encontrado.");

            _context.Laboratorios
                .Remove(laboratorio);

            await _context.SaveChangesAsync();
        }

        public async Task<Laboratorios?> ObterPorIdAsync(Guid id)
        {
            return await _context.Laboratorios
                .Include(x => x.Softwares)
                .Include(x => x.Alocacoes)
                .FirstOrDefaultAsync(x =>
                    x.IDLaboratorio == id);
        }

        public async Task<Laboratorios?> ObterPorNumeroAsync(
            int numero)
        {
            return await _context.Laboratorios
                .FirstOrDefaultAsync(x =>
                    x.NumeroLaboratorio == numero);
        }

        public async Task<IEnumerable<Laboratorios>> ObterTodosAsync()
        {
            return await _context.Laboratorios
                .Include(x => x.Softwares)
                .ToListAsync();
        }
    }
}