using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaLocLab.Application.DTOs
{
    public class CreateSoftwareDTO
    {
        [Required(ErrorMessage = "O nome do software é obrigatório.")]
        [Length(0, 50, ErrorMessage = "A quantidade de caracteres precisa estar entre 0 e 50.")]
        public string NomeSoftware { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A versão do software é obrigatório.")]
        [Length(0, 30, ErrorMessage = "A quantidade de caracteres precisa estar entre 0 e 30.")]
        public string Versao { get; set; } = string.Empty;
    }
}