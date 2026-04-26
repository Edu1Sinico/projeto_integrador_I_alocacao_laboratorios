using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;

namespace ProjetoIntegredor.Servicos
{
    public class LaboratorioService
    {
        List<Laboratorio> laboratorios = new();

        // Cadastrar
        public Laboratorio CadastrarLaboratorio(Usuario usuarioLogado, int numLaboratorio, int qtdeComputador, int capacidadeMaxAluno, Bloco bloco, Software software)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarDiretor(usuarioLogado);

            if (laboratorios.Any(l => l.NumLaboratorio == numLaboratorio && l.Bloco == bloco))
                throw new ArgumentException("Laboratório já cadastrado neste bloco!");

            var laboratorio = new Laboratorio(numLaboratorio, qtdeComputador, capacidadeMaxAluno, bloco);
            laboratorio.AdicionarSoftware(software);
            laboratorios.Add(laboratorio);
            return laboratorio;
        }

        // Buscar
        public Laboratorio? BuscarLaboratorioNumBloco(int numLaboratorio, Bloco bloco)
        {
            return laboratorios.FirstOrDefault(l => l.NumLaboratorio == numLaboratorio && l.Bloco == bloco);
        }

        // Todos os laboratorios
        public List<Laboratorio> Buscarlaboratorios()
        {
            return laboratorios.ToList();
        }

        // Atualizar
        public Laboratorio? AtualizarLaboratorio(Usuario usuarioLogado, int id, int numLaboratorio, int qtdeComputador, int capacidadeMaxAluno, Bloco bloco)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarDiretor(usuarioLogado);
            var laboratorio = laboratorios.FirstOrDefault(l => l.IdLaboratorio == id);

            if (laboratorio == null)
                return null;

            // Validar se já existe outro laboratório com o mesmo número e bloco
            if (laboratorios.Any(l => l.IdLaboratorio != id && l.NumLaboratorio == numLaboratorio && l.Bloco == bloco))
                throw new ArgumentException("Já existe outro laboratório com este número neste bloco!");

            laboratorio.NumLaboratorio = numLaboratorio;
            laboratorio.QtdeComputador = qtdeComputador;
            laboratorio.CapacidadeMaxAluno = capacidadeMaxAluno;
            laboratorio.AlterarBloco(bloco);

            return laboratorio;
        }

        // Excluir
        public Laboratorio? ExcluirLaboratorio(Usuario usuarioLogado, int id)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarDiretor(usuarioLogado);
            var laboratorio = laboratorios.FirstOrDefault(l => l.IdLaboratorio == id);

            if (laboratorio != null)
                laboratorios.Remove(laboratorio);

            return laboratorio;
        }

        // Adicionando softwares para às laboratorios
        public Laboratorio? AdicionarSoftwareLaboratorio(Usuario usuarioLogado, int idLaboratorio, Software software)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarDiretor(usuarioLogado);

            var laboratorio = laboratorios.FirstOrDefault(l => l.IdLaboratorio == idLaboratorio);

            if (laboratorio == null)
                return null;

            laboratorio.AdicionarSoftware(software);

            return laboratorio;
        }

        // Remover os softwares das laboratorios
        public Laboratorio? RemoverSoftwareLaboratorio(Usuario usuarioLogado, int idLaboratorio, Software software)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarDiretor(usuarioLogado);

            var laboratorio = laboratorios.FirstOrDefault(l => l.IdLaboratorio == idLaboratorio);

            if (laboratorio == null)
                return null;

            laboratorio.RemoverSoftware(software);

            return laboratorio;
        }
    }
}