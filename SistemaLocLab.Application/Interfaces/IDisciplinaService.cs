using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaLocLab.Application.DTOs;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Application.Interfaces
{
    public interface IDisciplinaService
    {
        Task<IEnumerable<DisciplinaDTO>> ObterDisciplinas();
        Task<IEnumerable<DisciplinaDTO>> ObterDisciplinaID(Guid id);
        Task<IEnumerable<DisciplinaDTO>> BuscarDisciplinasNome(string nome);
        Task<DisciplinaDTO> CriarDisciplina(CreateDisciplinaDTO dto);
        Task<DisciplinaDTO> AtualizarDisciplina(Guid id, UpdateDisciplinaDTO dto);
        Task<bool> RemoverSoftware(Guid id);
    }
}
