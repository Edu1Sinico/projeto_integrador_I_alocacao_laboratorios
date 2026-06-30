using SistemaLocLab.Domain.Enum;

namespace SistemaLocLab.Application.DTOs
{
    public class UsuarioDTO
    {
        public Guid ID { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RE { get; set; } = string.Empty;
        public TipoUsuario Tipo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
