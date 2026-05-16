using Microsoft.EntityFrameworkCore;
using SistemaLocLab.Domain.Entities;
using SistemaLocLab.Infrastructure.Context;
using SistemaLocLab.Infrastructure.Repositories.Interfaces;

namespace SistemaLocLab.Infrastructure.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(
            Usuarios usuario)
        {
            await _context.Usuarios.AddAsync(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(
            Usuarios usuario)
        {
            _context.Usuarios.Update(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var usuario =
                await ObterPorIdAsync(id);

            if (usuario == null)
                throw new Exception(
                    "Usuário não encontrado.");

            _context.Usuarios.Remove(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task<Usuarios?> ObterPorIdAsync(Guid id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Usuarios?> ObterPorEmailAsync(
            string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(x =>
                    x.Email == email.ToLower());
        }
}
}