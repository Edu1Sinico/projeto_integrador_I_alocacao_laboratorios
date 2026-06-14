using System.ComponentModel.DataAnnotations;

namespace SistemaLocLab.Application.DTOs
{
    public class UpdateLaboratorioDTO
    {
        [Required(ErrorMessage = "O numero do laboratorio e obrigatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "O numero do laboratorio precisa ser maior que zero.")]
        public int NumeroLaboratorio { get; set; }

        [Required(ErrorMessage = "O bloco do laboratorio e obrigatorio.")]
        [Length(1, 20, ErrorMessage = "O bloco precisa ter entre 1 e 20 caracteres.")]
        public string Bloco { get; set; } = string.Empty;

        [Required(ErrorMessage = "A quantidade de computadores e obrigatoria.")]
        [Range(1, 200, ErrorMessage = "A quantidade de computadores precisa estar entre 1 e 200.")]
        public int QtdeComputador { get; set; }
    }
}
