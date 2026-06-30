using SistemaLocLab.Domain.Enum;

namespace SistemaLocLab.Application.DTOs
{
    public class AlocacaoDTO
    {
        public Guid IdAlocacao { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public StatusAlocacao Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid LaboratorioId { get; set; }
        public int NumeroLaboratorio { get; set; }
        public string BlocoLaboratorio { get; set; } = string.Empty;
        public Guid DisciplinaId { get; set; }
        public string NomeDisciplina { get; set; } = string.Empty;
        public Guid UsuarioId { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
    }
}
