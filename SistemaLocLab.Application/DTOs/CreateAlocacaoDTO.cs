using System.ComponentModel.DataAnnotations;

namespace SistemaLocLab.Application.DTOs
{
    public class CreateAlocacaoDTO
    {
        [Required(ErrorMessage = "A data e obrigatoria.")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "A hora inicial e obrigatoria.")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "A hora final e obrigatoria.")]
        public TimeSpan HoraFim { get; set; }

        [Required(ErrorMessage = "O laboratorio e obrigatorio.")]
        public Guid LaboratorioId { get; set; }

        [Required(ErrorMessage = "A disciplina e obrigatoria.")]
        public Guid DisciplinaId { get; set; }

        [Required(ErrorMessage = "O usuario e obrigatorio.")]
        public Guid UsuarioId { get; set; }
    }
}
