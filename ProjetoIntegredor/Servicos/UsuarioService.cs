using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;

namespace ProjetoIntegredor.Servicos
{
    public class UsuarioService
    {

        private List<Usuario> usuarios = new();

        // Métodos CRUD dos Usuários

        // Cadastrar
        public Usuario CadastrarUsuario(Usuario usuarioLogado, string re, string nome, string senha, string email, TipoUsuario tipo)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarDiretor(usuarioLogado);

            // Verifica se o RE já foi cadastrado.
            if (usuarios.Any(u => u.RE == re))
                throw new ArgumentException("RE já cadastrado!");

            // Realiza o cadastro e define uma senha para ele
            var usuario = new Usuario(re, nome, email.ToLower(), tipo);
            usuario.DefinirSenha(senha);

            usuarios.Add(usuario);
            return usuario;
        }

        // Buscar (por RE ou Nome)
        public Usuario? BuscarUsuarioRE(Usuario usuarioLogado, string re)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarDiretor(usuarioLogado);
            return usuarios.FirstOrDefault(u => u.RE == re);
        }

        // Pode haver mais de um usuário com o mesmo nome
        public List<Usuario> BuscarUsuarioNome(Usuario usuarioLogado, string nome)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarDiretor(usuarioLogado);
            return usuarios
                .Where(u => u.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Todos os usuários
        public List<Usuario> BuscarUsuarios(Usuario usuarioLogado)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarDiretor(usuarioLogado);
            return usuarios.ToList();
        }

        // Atualizar
        public Usuario? AtualizarUsuario(Usuario usuarioLogado, int id, string nome, string email, string? senha = null)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarDiretor(usuarioLogado);
            var usuario = usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (usuario == null)
                return null;

            usuario.Nome = nome; // atualizar nome
            usuario.Email = email;  // atualizar e-mail


            if (!string.IsNullOrWhiteSpace(senha))
                usuario.DefinirSenha(senha); // atualizar senha de forma opcional

            return usuario;
        }

        // Excluir
        public Usuario? ExcluirUsuario(Usuario usuarioLogado, int id)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarDiretor(usuarioLogado);
            var usuario = usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (usuario != null)
                usuarios.Remove(usuario);

            return usuario;
        }

        // Método principais de "Usuário"

        // Realizar login
        public Usuario? Login(TipoUsuario tipo, string re, string senha)
        {
            var usuario = usuarios.FirstOrDefault(u => u.RE == re && u.Tipo == tipo); // Valida o RE e o tipo de usuário

            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado!");

            if (usuario.Tipo != tipo)
                throw new ArgumentException("Tipo de usuário inválido!");

            if (!usuario.ValidarSenha(senha))
                throw new ArgumentException("Senha incorreta!");

            return usuario;
        }

        // Recuperar Senha 
        public bool RecuperarSenha(string re, string novaSenha)
        {
            var usuario = usuarios.FirstOrDefault(u => u.RE == re);

            if (usuario == null)
                return false;

            usuario.DefinirSenha(novaSenha);
            return true;
        }

    }
}