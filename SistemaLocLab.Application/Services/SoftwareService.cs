using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaLocLab.Application.Interfaces;
using SistemaLocLab.Infrastructure.Repositories.Interfaces;
using SistemaLocLab.Domain.Entities;
using SistemaLocLab.Application.DTOs;

namespace SistemaLocLab.Application.Services
{
    public class SoftwareService : ISoftwareService
    {

        private readonly ISoftwareRepository _softwareRepository;

        public SoftwareService(ISoftwareRepository softwareRepository)
        {
            _softwareRepository = softwareRepository;
        }

        public async Task<List<SoftwareDTO>> ObterSoftwares()
        {
            var softwares = await _softwareRepository.ObterTodosAsync();

            return softwares
                .Select(MapearParaDTO)
                .ToList();
        }

        public async Task<SoftwareDTO?> ObterSoftwareId(Guid id)
        {
            var software = await _softwareRepository.ObterPorIdAsync(id);

            if (software == null)
                return null;

            return MapearParaDTO(software);
        }

        // Criar uma função no softwareRepository para buscar pelo nome
        public async Task<List<SoftwareDTO>> BuscarSoftwaresNome(string nome)
        {
            throw new NotImplementedException();
        }

        public async Task<SoftwareDTO> CriarSoftware(CreateSoftwareDTO dto)
        {
            // Atribui o que foi recebido do DTO para a entidade verdadeira
            var software = new Software(dto.NomeSoftware, dto.Versao);

            await _softwareRepository.AdicionarAsync(software);

            return MapearParaDTO(software);
        }

        public async Task<SoftwareDTO?> AtualizarSoftware(Guid id, UpdateSoftwareDTO dto)
        {
            var software = await _softwareRepository.ObterPorIdAsync(id);

            // Se não exisitr, retorna null.
            if (software == null)
                return null;

            // Se existir, atualiza os dados e retorna o produto atualizado.
            software.Atualizar(dto.NomeSoftware, dto.Versao);

            await _softwareRepository.AtualizarAsync(software);

            return MapearParaDTO(software);
        }


        public async Task<bool> RemoverSoftware(Guid id)
        {
            var software = await _softwareRepository.ObterPorIdAsync(id);

            if (software == null)
                return false;

            await _softwareRepository.RemoverAsync(id);

            return true;
        }


        // Método para mapeamento dos dados da entidade para DTO
        private SoftwareDTO MapearParaDTO(Software software)
        {
            return new SoftwareDTO
            {
                IdSoftware = software.IdSoftware,
                NomeSoftware = software.NomeSoftware,
                Versao = software.Versao,
                DateCriacao = software.DataCriacao,
                DataAtualizacao = software.DataAtualizacao
            };
        }
    }
}