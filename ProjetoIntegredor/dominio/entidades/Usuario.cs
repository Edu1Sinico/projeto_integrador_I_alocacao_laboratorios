using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using ProjetoIntegredor.dominio.enums;

namespace ProjetoIntegredor.model
{
    public class Usuario
    {
        private static int contador = 1;
        public int IdUsuario { get; private set; }
        private string re;
        private string nome;
        private string senhaHash;
        private string email;
        public TipoUsuario Tipo { get; set; }

        // Validação de RE
        public string RE
        {
            get => re;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("RE inválido!");

                re = value;
            }
        }

        // Validação de nome
        public String Nome
        {
            get => nome;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Nome de usuário inválido!");
                nome = value;
            }
        }

        // Validação do e-mail
        public string Email
        {
            get => email; // similar ao get {return email;}
            set
            {
                // Valida se o valor não é nulo ou vázio string.IsNullOrEmpty(value)
                if (string.IsNullOrEmpty(value) || !value.Contains("@"))
                    throw new ArgumentException("E-mail inválido!");
                email = value;
            }
        }

        // Validação de senha

        // Senha só pode ser definida via método
        public void DefinirSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha))
                throw new ArgumentException("Senha inválida!");

            senhaHash = GerarHash(senha);
        }

        public bool ValidarSenha(string senha)
        {
            return senhaHash == GerarHash(senha);
        }

        public string GerarHash(string senha)
        {
            // Função nativa para criptografar a senha
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(senha)); // Converte em bites e cripotografa em 64 "partes"
                return Convert.ToBase64String(bytes); // Converte para String, exemplo senha: 123 -> @$rd#weq24fw
            }
        }

        // Construtor
        public Usuario(string re, string nome, string email, TipoUsuario tipo)
        {
            IdUsuario = contador++;
            RE = re;
            Nome = nome;
            Email = email;
            Tipo = tipo;
        }
    }
}