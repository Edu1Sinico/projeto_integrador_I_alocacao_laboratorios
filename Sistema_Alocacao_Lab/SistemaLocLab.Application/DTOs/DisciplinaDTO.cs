using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaLocLab.Application.DTOs
{
    public class DisciplinaDTO
    {
        public Guid IdDisciplina {get; set;}
        public string NomeDisciplina {get;set;} = string.Empty;
        public int QtdeAlunos {get;set;}
        public DateTime DataCriacao {get;set;}
        public DateTime? DataAtualizacao {get;set;}
    }
}