using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SistemaLocLab.Domain.Enum;

namespace SistemaLocLab.Domain.Entities
{
    public class Usuarios
    {
        //Guid garante que um ID seja exclusivo em qualquer computador ou rede
        public Guid ID { get; private set; }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public string RE { get; private set; }

        public string SenhaHash { get; private set; }

        public TipoUsuario Tipo { get; private set; }

        public DateTime DataCriacao { get; private set; }

        public List<Alocacao> Alocacoes { get; private set; } = new();

        //construtor protegido, somente a classe e as subclasses podem acessa-lo
        protected Usuarios() { }


        //Construtor com parametros
        public Usuarios(string nome, string email, string re, string senhaHash, TipoUsuario tipo)
        {
            Validacao(nome, email, re, senhaHash);
            ID = Guid.NewGuid();

            //.Trim() serve para remover os espaços em branco no começo e no final da string
            Nome = nome.Trim();

            //.ToLower deixa os caracteres em letra minuscula
            Email = email.Trim().ToLower();

            RE = re.Trim();

            SenhaHash = senhaHash;

            Tipo = tipo;

            DataCriacao = DateTime.Now;

        }

        //metodo para atualizar Nome
        public void UpdateName(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome Inválido.");

            if (nome.Length < 2) throw new ArgumentException("O Nome deve possuir ao menos 2 letras");

            Nome = nome.Trim();
        }

        //metodo para atualizar senha com validação
        public void UpdateSenha(string newSenhaHash)
        {
            //impede senhas nula, vazia, com espaços
            if (string.IsNullOrWhiteSpace(newSenhaHash)) throw new ArgumentException("Senha invalida");
            SenhaHash = newSenhaHash;
        }

        //metodo que centraliza as validaçoes
        public void Validacao(string nome, string email, string re, string SenhaHash)
        {
            ValidacaoNome(nome);
            ValidacaoEmail(email);
            ValidacaoRE(re);
            ValidacaoSenha(SenhaHash);
        }

        private void ValidacaoNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome obrigatório");

            if (nome.Length < 2) throw new ArgumentException("O Nome deve possuir ao menos 2 letras");

            if (nome.Length > 70) throw new ArgumentException("Nome muito grande");
        }

        private void ValidacaoEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email obrigatório.");

            var emailRegex =
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, emailRegex))
                throw new ArgumentException("Email inválido.");
        }

        private void ValidacaoRE(string re)
        {
            if (string.IsNullOrWhiteSpace(re))
                throw new ArgumentException("RE obrigatório.");

            if (re.Length < 5 || re.Length > 20)
                throw new ArgumentException("RE inválido.");

            if (!re.All(char.IsDigit))
                throw new ArgumentException("RE deve conter apenas números.");
        }

        private void ValidacaoSenha(string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(senhaHash))
                throw new ArgumentException("Senha obrigatória.");
        }
    }
}