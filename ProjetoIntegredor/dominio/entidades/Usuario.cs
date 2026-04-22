using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;

namespace ProjetoIntegredor.model
{
    public class Usuario
    {
        private static int contador = 1;
        public int Id_usuario { get; private set; }
        private int re;
        public string Nome { get; set; }
        public string Senha {get;set;}
        private string email;
        public TipoUsuario Tipo { get; set; }
    
        public int RE
        {
            get { return re; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("RE não pode ser negativo!");
                re = value;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (value == null || !value.Contains("@"))
                    throw new ArgumentException("E-mail inválido!");
                email = value;
            }
        }

        // Construtor
        public Usuario(int re, string nome, string senha, string email, TipoUsuario tipo)
        {
            Id_usuario = contador++;
            RE = re;
            Nome = nome;
            Email = email;
            Senha = senha;
            Tipo = tipo;
        }
    }
}