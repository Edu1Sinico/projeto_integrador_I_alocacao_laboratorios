using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaLocLab.Application.Interfaces;
using SistemaLocLab.Infrastructure.Repositories.Interfaces;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Application.Services
{
    public class SoftwareService : ISoftwareService
    {

        private readonly ISoftwareRepository _softwareRepository;

        public SoftwareService(ISoftwareService softwareService)
        {
            _softwareRepository = softwareService;
        }

        public List<SoftwareDTO> ObterSoftwares()
        {
            var softwares = _softwareRepository.ObterTodosAsync();
        }

        public SoftwareDTO ObterSoftwareId(int id)
        {
            var software = _softwareRepository.ObterPorIdAsync(id); // Verificar questão de buscar por ID e Guid
        }

        // Criar uma função no softwareRepository para buscar pelo nome
        public List<SoftwareDTO> BuscarSoftwaresNome(string nome)
        {
            throw new NotImplementedException();
        }

        public SoftwareDTO CriarSoftware(CreateSoftwareDTO dto)
        {
            // Atribui o que foi recebido do DTO para a entidade verdadeira
            var software = new Software
            {
                NomeSoftware = dto.NomeSoftware.Trim(),
                Versao = dto.Versao.Trim(),
                DateCriacao = dto.DateCriacao,
                DataAtualizacao = dto.DataAtualizacao
            };

            _softwareRepository.AdicionarAsync(software);

            return MapearParaDTO(software);
        }

        public SoftwareDTO AtualizarSoftware(int id, UpdateSoftwareDTO dto)
        {
            var software = _softwareRepository.ObterPorIdAsync(id);

            // Se não exisitr, retorna null.
            if (software == null)
                return null;

            // Se existir, atualiza os dados e retorna o produto atualizado.
            software.NomeSoftware = dto.NomeSoftware;
            software.Versao = dto.Versao;
            software.DataAtualizacao = dto.DataAtualizacao;

            _softwareRepository.AtualizarAsync(software);

            return MapearParaDTO(software);
        }


        public bool RemoverSoftware(int id)
        {
            var software = _softwareRepository.ObterPorIdAsync(id);

            if (software == null)
                return false;

            return _softwareRepository.Remover(id);
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