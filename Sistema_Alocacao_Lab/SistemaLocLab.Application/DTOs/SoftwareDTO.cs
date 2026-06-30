using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaLocLab.Application.DTOs
{
    public class SoftwareDTO
    {
        public Guid IdSoftware { get; set; }
        public string NomeSoftware { get; set; } = string.Empty;
        public string Versao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}