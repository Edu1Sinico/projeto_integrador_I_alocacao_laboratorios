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

            if (lab.StatusDisponibilidade == Disponibilidade.I)
                throw new ArgumentException("Este laboratório está indisponível para uso.");

            if (ValidarDataPassada(data))
                throw new ArgumentException("Não é possível solicitar alocação para uma data já passado.");

            if (!ValidarHorarioNoturno(horaInicio, horaFim))
                throw new ArgumentException("As alocações devem ocorrer entre 19:00 e 22:30.");

            if (!ValidarSoftwaresCompativeis(lab, disc))
                throw new ArgumentException("O laboratório não possui todos os softwares necessários para esta disciplina.");

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
            // Validar se já existe alguma locação para determinada sala em um dia e horário específicos
            return alocacoes.Any(a =>
                a.Laboratorio == lab &&
                a.Data == data &&
                horaInicio < a.HoraFim &&
                horaFim > a.HoraInicio
            );
        }

        // Validar data atual
        private bool ValidarDataPassada(DateOnly data)
        {
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);

            return data < dataAtual;
        }

        // Validar horário noturno
        private bool ValidarHorarioNoturno(TimeOnly horaInicio, TimeOnly horaFim)
        {
            TimeOnly horarioMinimo = new TimeOnly(19, 0);
            TimeOnly horarioMaximo = new TimeOnly(22, 30);

            return horaInicio >= horarioMinimo && horaFim <= horarioMaximo;
        }

        // Validar capacidade
        private bool ValidarCapacidade(Laboratorio lab, Disciplina disc)
        {
            return lab.CapacidadeMaxAluno >= disc.QtdeAlunos;
        }

        // Validar Softwares compativeis com o laboratório e disciplina
        private bool ValidarSoftwaresCompativeis(Laboratorio lab, Disciplina disc)
        {
            if (!disc.Softwares.Any())
                return true;

            // Verificar se na disciplina existe algum software que já esteja em um laboratório
            return disc.Softwares.Any(softwareDisciplina => lab.Softwares.Any(softwareLab => softwareLab.IdSoftware == softwareDisciplina.IdSoftware));
        }

        // Criar um método de buscar por ID

        // Histórico de alocações
        public List<Alocacao> HistoricoAlocacao()
        {
            return alocacoes.ToList();
        }

        // Buscar por ID
        public Alocacao? BuscarAlocacaoID(int IdAlocacao)
        {
            return alocacoes.FirstOrDefault(a => a.IdAlocacao.Equals(IdAlocacao));
        }


    }
}