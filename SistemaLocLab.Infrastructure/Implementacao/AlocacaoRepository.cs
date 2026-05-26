using Microsoft.EntityFrameworkCore;
using SistemaLocLab.Domain.Entities;
using SistemaLocLab.Infrastructure.Context;
using SistemaLocLab.Infrastructure.Repositories.Interfaces;
namespace SistemaLocLab.Infrastructure.Repositories.Implementations
{
    public class AlocacaoRepository : IAlocacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public AlocacaoRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(
            Alocacao alocacao)
        {
            await _context.Alocacoes
                .AddAsync(alocacao);

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(
            Alocacao alocacao)
        {
            _context.Alocacoes.Update(alocacao);

            await _context.SaveChangesAsync();
        }

        public async Task<Alocacao?> ObterPorIdAsync(Guid id)
        {
            return await _context.Alocacoes
                .Include(x => x.Usuario)
                .Include(x => x.Disciplina)
                .Include(x => x.Laboratorio)
                .FirstOrDefaultAsync(x =>
                    x.IdAlocacao == id);
        }

        public async Task<IEnumerable<Alocacao>> ObterTodosAsync()
        {
            return await _context.Alocacoes
                .Include(x => x.Usuario)
                .Include(x => x.Disciplina)
                .Include(x => x.Laboratorio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alocacao>> ObterPorLaboratorioAsync(Guid laboratorioId)
        {
            return await _context.Alocacoes
                .Where(x =>
                    x.LaboratorioId == laboratorioId)
                .ToListAsync();
        }
    }
}