namespace SistemaLocLab.Application.DTOs
{
    public class LaboratorioDTO
    {
        public Guid IDLaboratorio { get; set; }
        public int NumeroLaboratorio { get; set; }
        public string Bloco { get; set; } = string.Empty;
        public int QtdeComputador { get; set; }
        public int CapacidadeMaxAluno { get; set; }
    }
}
