using System.ComponentModel.DataAnnotations;

namespace SistemaLocLab.Application.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O email e obrigatorio.")]
        [EmailAddress(ErrorMessage = "Email invalido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha e obrigatoria.")]
        public string Senha { get; set; } = string.Empty;
    }
}
