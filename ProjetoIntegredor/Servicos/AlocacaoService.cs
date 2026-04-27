using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;

namespace ProjetoIntegredor.Servicos
{
    public class AlocacaoService
    {
        List<Alocacao> alocacoes = new();
        // Solicitando uma alocação (função do coordenador)
        public Alocacao SolicitarAlocacao(Usuario usuario, Disciplina disc, Laboratorio lab, DateOnly data, TimeOnly horaInicio, TimeOnly horaFim)
        {
            AutorizacaoService.ValidarUsuario(usuario);
            AutorizacaoService.ValidarCoordenador(usuario);

            // Validar se já existe alguma locação para determinada sala em um dia e horário específicos
            if (ValidarConflitoHorario(lab, data, horaInicio, horaFim))
                throw new ArgumentException("Essa sala já está alocada para esta data!");

            if (!ValidarCapacidade(lab, disc))
                throw new ArgumentException("Quantidade de alunos ultrapassa a capacidade máxima de alunos no laboratório!");

            var alocacao = new Alocacao(data, horaInicio, horaFim, lab, disc, usuario);
            alocacoes.Add(alocacao);
            return alocacao;
        }

        // Aprovar uma alocação (função do diretor)
        public Alocacao AprovarAlocacao(Usuario usuario, Alocacao alocacao)
        {
            AutorizacaoService.ValidarUsuario(usuario);
            AutorizacaoService.ValidarDiretor(usuario);

            if (alocacao == null)
                return null;

            var aprovarAlocacao = alocacoes.FirstOrDefault(a => a.IdAlocacao == alocacao.IdAlocacao);
            alocacao.Aprovar();

            return alocacao;
        }

        // Reprovar uma alocação (função do diretor)
        public Alocacao ReprovarAlocacao(Usuario usuario, Alocacao alocacao)
        {
            AutorizacaoService.ValidarUsuario(usuario);
            AutorizacaoService.ValidarDiretor(usuario);

            if (alocacao == null)
                return null;

            var reprovarAlocacao = alocacoes.FirstOrDefault(a => a.IdAlocacao == alocacao.IdAlocacao);
            alocacao.Reprovar();

            return alocacao;
        }

        // Validar conflito de horários
        private bool ValidarConflitoHorario(Laboratorio lab, DateOnly data, TimeOnly horaInicio, TimeOnly horaFim)
        {
            return alocacoes.Any(a =>
                a.Laboratorio == lab &&
                a.Data == data &&
                horaInicio < a.HoraFim &&
                horaFim > a.HoraInicio
            );
        }

        // Validar capacidade
        public bool ValidarCapacidade(Laboratorio lab, Disciplina disc)
        {
            return lab.CapacidadeMaxAluno >= disc.QtdeAlunos;
        }

        // Histórico de alocações
        public List<Alocacao> HistoricoAlocacao()
        {
            return alocacoes.ToList();
        }

    }
}