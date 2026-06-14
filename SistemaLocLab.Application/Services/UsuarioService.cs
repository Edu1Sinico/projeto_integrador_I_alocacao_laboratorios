using SistemaLocLab.Application.DTOs;
using SistemaLocLab.Application.Interfaces;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioDTO>> ObterUsuarios()
        {
            var usuarios = await _usuarioRepository.ObterTodosAsync();

            return usuarios.Select(MapearParaDTO).ToList();
        }

        public async Task<UsuarioDTO?> ObterUsuarioId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);

            if (usuario == null)
                return null;

            return MapearParaDTO(usuario);
        }

        public async Task<UsuarioDTO?> ObterUsuarioEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Informe um email valido.");

            var usuario = await _usuarioRepository.ObterPorEmailAsync(email.Trim().ToLower());

            if (usuario == null)
                return null;

            return MapearParaDTO(usuario);
        }

        public async Task<UsuarioDTO?> Login(LoginDTO dto)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email.Trim().ToLower());

            if (usuario == null || usuario.SenhaHash != dto.Senha)
                return null;

            return MapearParaDTO(usuario);
        }

        public async Task<UsuarioDTO?> CriarUsuario(CreateUsuarioDTO dto)
        {
            var emailExiste = await _usuarioRepository.ExisteEmailAsync(dto.Email.Trim().ToLower());

            if (emailExiste)
                return null;

            var usuario = new Usuarios(
                dto.Nome,
                dto.Email,
                dto.RE,
                dto.SenhaHash,
                dto.Tipo);

            await _usuarioRepository.AdicionarAsync(usuario);

            return MapearParaDTO(usuario);
        }

        public async Task<UsuarioDTO?> AtualizarUsuario(Guid id, UpdateUsuarioDTO dto)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);

            if (usuario == null)
                return null;

            var usuarioComEmail = await _usuarioRepository.ObterPorEmailAsync(dto.Email.Trim().ToLower());

            if (usuarioComEmail != null && usuarioComEmail.ID != id)
                throw new InvalidOperationException("Email ja cadastrado para outro usuario.");

            usuario.Atualizar(dto.Nome, dto.Email, dto.RE, dto.Tipo);

            await _usuarioRepository.AtualizarAsync(usuario);

            return MapearParaDTO(usuario);
        }

        public async Task<bool> RemoverUsuario(Guid id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);

            if (usuario == null)
                return false;

            await _usuarioRepository.RemoverAsync(id);

            return true;
        }

        private UsuarioDTO MapearParaDTO(Usuarios usuario)
        {
            return new UsuarioDTO
            {
                ID = usuario.ID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                RE = usuario.RE,
                Tipo = usuario.Tipo,
                DataCriacao = usuario.DataCriacao
            };
        }
    }
}
