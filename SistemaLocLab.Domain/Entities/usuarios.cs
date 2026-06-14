using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SistemaLocLab.Domain.Enum;

namespace SistemaLocLab.Domain.Entities
{
    public class Usuarios
    {
        public Guid ID { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string RE { get; private set; }
        public string SenhaHash { get; private set; }
        public TipoUsuario Tipo { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public List<Alocacao> Alocacoes { get; private set; } = new();

        protected Usuarios()
        {
            Nome = string.Empty;
            Email = string.Empty;
            RE = string.Empty;
            SenhaHash = string.Empty;
        }

        public Usuarios(string nome, string email, string re, string senhaHash, TipoUsuario tipo)
        {
            Validacao(nome, email, re, senhaHash);

            ID = Guid.NewGuid();
            Nome = nome.Trim();
            Email = email.Trim().ToLower();
            RE = re.Trim();
            SenhaHash = senhaHash;
            Tipo = tipo;
            DataCriacao = DateTime.UtcNow;
        }

        public void Atualizar(string nome, string email, string re, TipoUsuario tipo)
        {
            ValidacaoNome(nome);
            ValidacaoEmail(email);
            ValidacaoRE(re);

            Nome = nome.Trim();
            Email = email.Trim().ToLower();
            RE = re.Trim();
            Tipo = tipo;
        }

        public void UpdateName(string nome)
        {
            ValidacaoNome(nome);
            Nome = nome.Trim();
        }

        public void UpdateSenha(string newSenhaHash)
        {
            ValidacaoSenha(newSenhaHash);
            SenhaHash = newSenhaHash;
        }

        public void Validacao(string nome, string email, string re, string senhaHash)
        {
            ValidacaoNome(nome);
            ValidacaoEmail(email);
            ValidacaoRE(re);
            ValidacaoSenha(senhaHash);
        }

        private void ValidacaoNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome obrigatorio.");

            if (nome.Trim().Length < 2)
                throw new ArgumentException("O nome deve possuir ao menos 2 letras.");

            if (nome.Trim().Length > 70)
                throw new ArgumentException("Nome muito grande.");
        }

        private void ValidacaoEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email obrigatorio.");

            const string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, emailRegex))
                throw new ArgumentException("Email invalido.");
        }

        private void ValidacaoRE(string re)
        {
            if (string.IsNullOrWhiteSpace(re))
                throw new ArgumentException("RE obrigatorio.");

            if (re.Length < 5 || re.Length > 20)
                throw new ArgumentException("RE invalido.");

            if (!re.All(char.IsDigit))
                throw new ArgumentException("RE deve conter apenas numeros.");
        }

        private void ValidacaoSenha(string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(senhaHash))
                throw new ArgumentException("Senha obrigatoria.");
        }
    }
}
