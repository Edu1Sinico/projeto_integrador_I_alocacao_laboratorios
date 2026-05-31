using System.ComponentModel.DataAnnotations;

namespace SistemaLocLab.Application.DTOs
{
    public class UpdateAlocacaoDTO
    {
        [Required(ErrorMessage = "A hora inicial e obrigatoria.")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "A hora final e obrigatoria.")]
        public TimeSpan HoraFim { get; set; }
    }
}
