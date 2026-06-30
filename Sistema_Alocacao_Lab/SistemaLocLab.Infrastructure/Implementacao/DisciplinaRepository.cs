using Microsoft.EntityFrameworkCore;
using SistemaLocLab.Domain.Entities;
using SistemaLocLab.Infrastructure.Context;
using SistemaLocLab.Application.Interfaces;

namespace SistemaLocLab.Infrastructure.Repositories.Implementations
{
    public class DisciplinaRepository : IDisciplinaRepository
    {
        private readonly ApplicationDbContext _context;

        public DisciplinaRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(
            Disciplina disciplina)
        {
            await _context.Disciplinas
                .AddAsync(disciplina);

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(
            Disciplina disciplina)
        {
            _context.Disciplinas
                .Update(disciplina);

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var disciplina =
                await ObterPorIdAsync(id);

            if (disciplina == null)
                throw new Exception(
                    "Disciplina não encontrada.");

            _context.Disciplinas
                .Remove(disciplina);

            await _context.SaveChangesAsync();
        }

        public async Task<Disciplina?> ObterPorIdAsync(Guid id)
        {
            return await _context.Disciplinas.FirstOrDefaultAsync(x => x.IdDisciplina == id);
        }

        public async Task<IEnumerable<Disciplina>> ObterTodosAsync()
        {
            return await _context.Disciplinas
                .ToListAsync();
        }

        public async Task<IEnumerable<Disciplina>> BuscarPorNomeAsync(string nome)
        {
            return await _context.Disciplinas.Where(x => x.NomeDisciplina.ToLower().Contains(nome.ToLower().Trim())).ToListAsync();
        }
    }
}