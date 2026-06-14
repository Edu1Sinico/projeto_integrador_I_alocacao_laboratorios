using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaLocLab.Application.DTOs
{
    public class CreateDisciplinaDTO
    {
        [Required(ErrorMessage = "O nome da disciplina é obrigatório.")]
        [Length(0, 50, ErrorMessage = "A quantidade de caracteres precisa estar entre 0 e 50.")]
        public string NomeDisciplina { get; set; } = string.Empty;

        [Required(ErrorMessage = "A quantidade de alunos é obrigatório.")]
        [Range(0, 99999, ErrorMessage = "A quantidade de alunos não pode ser negativa.")]
        public int QtdeAlunos { get; set; }
    }
}