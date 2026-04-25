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
        public Usuario CadastrarUsuario(string re, string nome, string senha, string email, TipoUsuario tipo)
        {
            // Verifica se o RE já foi cadastrado.
            if (usuarios.Any(u => u.RE == re))
                throw new ArgumentException("RE já cadastrado!");

            // Realiza o cadastro e define uma senha para ele
            var usuario = new Usuario(re, nome, email, tipo);
            usuario.DefinirSenha(senha);

            usuarios.Add(usuario);
            return usuario;
        }

        // Buscar (por RE ou Nome)
        public Usuario? BuscarUsuarioRE(string re)
        {
            return usuarios.FirstOrDefault(u => u.RE == re);
        }

        // Pode haver mais de um usuário com o mesmo nome
        public List<Usuario> BuscarUsuarioNome(string nome)
        {
            return usuarios
                .Where(u => u.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Atualizar
        public Usuario? AtualizarUsuario(int id, string nome, string email, string senha)
        {
            var usuario = usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (usuario == null)
                return null;

            usuario.Nome = nome; // atualizar nome
            usuario.Email = email;  // atualizar e-mail
            usuario.DefinirSenha(senha); // atualizar senha

            return usuario;
        }

        // Excluir
        public Usuario? ExcluirUsuario(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (usuario != null)
                usuarios.Remove(usuario);

            return usuario;
        }


    }
}