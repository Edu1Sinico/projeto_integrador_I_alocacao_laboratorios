namespace ProjetoIntegredor
{
    public class Usuario
    {
        public int usuario { get; set; }
        public int RE { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }

        public Usuario(int usuario, int re, string nome, string senha, string tipo)
        {
            this.Usuario = usuario;
            this.RE = re;
            this.Nome = nome;
            this.Senha = senha;
            this.Tipo = tipo;

        }
    }
}