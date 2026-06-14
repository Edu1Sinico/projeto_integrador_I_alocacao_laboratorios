using System.ComponentModel.DataAnnotations;
using SistemaLocLab.Domain.Enum;

namespace SistemaLocLab.Application.DTOs
{
    public class UpdateUsuarioDTO
    {
        [Required(ErrorMessage = "O nome e obrigatorio.")]
        [Length(2, 70, ErrorMessage = "O nome precisa ter entre 2 e 70 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email e obrigatorio.")]
        [EmailAddress(ErrorMessage = "Email invalido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O RE e obrigatorio.")]
        [Length(5, 20, ErrorMessage = "O RE precisa ter entre 5 e 20 caracteres.")]
        public string RE { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo de usuario e obrigatorio.")]
        public TipoUsuario Tipo { get; set; }
    }
}
