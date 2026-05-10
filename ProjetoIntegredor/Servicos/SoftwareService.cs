using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.menu;
using ProjetoIntegredor.model;

namespace ProjetoIntegredor.Servicos
{
    public class SoftwareService
    {

        private List<Software> softwares = new();

        // Métodos CRUD dos Softwares

        // Cadastrar
        public Software CadastrarSoftware(Usuario usuarioLogado, string nomeSoftware, string versao)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarResponsavelTI(usuarioLogado);

            if (softwares.Any(s => s.NomeSoftware.Equals(nomeSoftware, StringComparison.OrdinalIgnoreCase))) // Procura o software e igonora os cases
                throw new ArgumentException("Software já cadastrado!");

            var software = new Software(Validacao.NormalizarTexto(nomeSoftware), Validacao.NormalizarTexto(versao).ToLower());
            softwares.Add(software);
            return software;
        }

        // Buscar por ID
        public Software? BuscarSoftwareID(int idSoftware)
        {
            return softwares.FirstOrDefault(s => s.IdSoftware.Equals(idSoftware));
        }

        // Buscar por nome
        public List<Software>? BuscarSoftwareNome(string nomeSoftware)
        {
            return softwares.Where(s => s.NomeSoftware.Contains(Validacao.NormalizarTexto(nomeSoftware), StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Todos os softwares
        public List<Software> BuscarSoftwares()
        {
            return softwares.ToList();
        }

        // Atualizar
        public Software? AtualizarSoftware(Usuario usuarioLogado, int id, string nomeSoftware, string versao)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarResponsavelTI(usuarioLogado);
            var software = softwares.FirstOrDefault(s => s.IdSoftware == id);

            if (software == null)
                return null;

            software.NomeSoftware = Validacao.NormalizarTexto(nomeSoftware);
            software.Versao = Validacao.NormalizarTexto(versao).ToLower();

            return software;
        }

        // Excluir
        public Software? ExcluirSoftware(Usuario usuarioLogado, int id)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarResponsavelTI(usuarioLogado);
            var software = softwares.FirstOrDefault(s => s.IdSoftware == id);

            if (software != null)
                softwares.Remove(software);

            return software;
        }
    }
}