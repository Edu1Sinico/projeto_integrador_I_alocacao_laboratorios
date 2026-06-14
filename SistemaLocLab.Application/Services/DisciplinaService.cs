using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaLocLab.Application.DTOs;
using SistemaLocLab.Application.Interfaces;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Application.Services
{
    public class DisciplinaService : IDisciplinaService
    {

        private readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaService(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        public async Task<List<DisciplinaDTO>> ObterDisciplinas()
        {
            var disciplinas = await _disciplinaRepository.ObterTodosAsync();

            return disciplinas
                .Select(MapearParaDTO)
                .ToList();
        }

        public async Task<DisciplinaDTO?> ObterDisciplinaID(Guid id)
        {
            var disciplina = await _disciplinaRepository.ObterPorIdAsync(id);

            if (disciplina == null)
                return null;

            return MapearParaDTO(disciplina);
        }

        public async Task<List<DisciplinaDTO>> BuscarDisciplinasNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Informe um nome válido para buscar.");

            var disciplinas = await _disciplinaRepository.BuscarPorNomeAsync(nome.Trim());

            return disciplinas
                .Select(MapearParaDTO)
                .ToList();
        }

        public async Task<DisciplinaDTO> CriarDisciplina(CreateDisciplinaDTO dto)
        {
            var disciplina = new Disciplina(dto.NomeDisciplina, dto.QtdeAlunos);

            await _disciplinaRepository.AdicionarAsync(disciplina);

            return MapearParaDTO(disciplina);
        }

        public async Task<DisciplinaDTO?> AtualizarDisciplina(Guid id, UpdateDisciplinaDTO dto)
        {
            var disciplina = await _disciplinaRepository.ObterPorIdAsync(id);

            if (disciplina == null)
                return null;

            disciplina.Atualizar(dto.NomeDisciplina, dto.QtdeAlunos);

            await _disciplinaRepository.AtualizarAsync(disciplina);

            return MapearParaDTO(disciplina);
        }

        public async Task<bool> RemoverDisciplina(Guid id)
        {
            var disciplina = await _disciplinaRepository.ObterPorIdAsync(id);

            if (disciplina == null)
                return false;

            await _disciplinaRepository.RemoverAsync(id);

            return true;
        }

        private DisciplinaDTO MapearParaDTO(Disciplina disciplina)
        {
            return new DisciplinaDTO
            {
                IdDisciplina = disciplina.IdDisciplina,
                NomeDisciplina = disciplina.NomeDisciplina,
                QtdeAlunos = disciplina.QtdeAlunos,
                DataCriacao = disciplina.DataCriacao,
                DataAtualizacao = disciplina.DataAtualizacao
            };
        }
    }
}
