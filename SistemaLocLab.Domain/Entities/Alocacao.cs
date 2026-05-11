using System;
using SistemaLocLab.Domain.Enum;
namespace SistemaLocLab.Domain.Entities
{
    public class Alocacao
    {
        public Guid IdAlocacao { get; private set; }
        public DateTime Data { get; private set; }
        public TimeSpan HoraInicio { get; private set; }
        public TimeSpan HoraFim { get; private set; }
        public StatusAlocacao Status { get; private set; }
        public DateTime DataCriacao { get; private set; }

        // CHAVES E RELACIONAMENTOS
        public Guid LaboratorioId { get; private set; }
        public Laboratorios Laboratorio { get; private set; }
        public Guid DisciplinaId { get; private set; }
        public Disciplina Disciplina { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Usuarios Usuario { get; private set; }
        protected Alocacao() { }
        public Alocacao(DateTime data,TimeSpan horaInicio,TimeSpan horaFim,Laboratorios laboratorio,Disciplina disciplina,Usuarios usuario)
        {
            Validacao(data,horaInicio,horaFim,laboratorio,disciplina,usuario);
            IdAlocacao = Guid.NewGuid();
            Data = data.Date;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            Laboratorio = laboratorio;
            LaboratorioId = laboratorio.IDLaboratorio;
            Disciplina = disciplina;
            DisciplinaId = disciplina.IdDisciplina;

            Usuario = usuario;
            UsuarioId = usuario.ID;
            Status = StatusAlocacao.Pendente;
            DataCriacao = DateTime.Now;
        }
        public void Aprovar()
        {
            Status = StatusAlocacao.Aprovada;
        }
        public void Reprovar()
        {
            Status = StatusAlocacao.Reprovada;
        }
        public void AtualizarHorario(TimeSpan horaInicio, TimeSpan horaFim)
        {
            ValidacaoHorario(horaInicio, horaFim);
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }
        public bool ExisteConflito(Alocacao outraAlocacao)
        {
            if (outraAlocacao == null)
                return false;
            bool mesmoLaboratorio = LaboratorioId == outraAlocacao.LaboratorioId;
            bool mesmaData = Data.Date == outraAlocacao.Data.Date;
            bool conflitoHorario = HoraInicio < outraAlocacao.HoraFim && HoraFim > outraAlocacao.HoraInicio;

            return mesmoLaboratorio && mesmaData && conflitoHorario;
        }
        private void Validacao(DateTime data,TimeSpan horaInicio,TimeSpan horaFim,Laboratorios laboratorio,Disciplina disciplina,Usuarios usuario)
        {
            ValidacaoData(data);
            ValidacaoHorario(horaInicio, horaFim);
            ValidacaoLaboratorio(laboratorio);
            ValidacaoDisciplina(disciplina);
            ValidacaoUsuario(usuario);
            ValidacaoCapacidade(
            laboratorio,
            disciplina);
        }
        private void ValidacaoData(DateTime data)
        {
            if (data.Date < DateTime.Now.Date)
                throw new ArgumentException(
                "Não é possível realizar alocações em datas passadas.");
        }
        private void ValidacaoHorario(
        TimeSpan horaInicio,
        TimeSpan horaFim)
        {
            if (horaInicio >= horaFim)
                throw new ArgumentException(
                "Horário inicial deve ser menor que o horário final.");
        }
        private void ValidacaoLaboratorio(
        Laboratorios laboratorio)
        {

            if (laboratorio == null)
                throw new ArgumentException(
                "Laboratório inválido.");
        }
        private void ValidacaoDisciplina(
        Disciplina disciplina)
        {
            if (disciplina == null)
                throw new ArgumentException(
                "Disciplina inválida.");
        }
        private void ValidacaoUsuario(
        Usuarios usuario)
        {
            if (usuario == null)
                throw new ArgumentException(
                "Usuário inválido.");
        }
        private void ValidacaoCapacidade(
        Laboratorios laboratorio,
        Disciplina disciplina)
        {
            bool comporta =
            laboratorio.PodeComportar(
            disciplina.QtdeAlunos);
            if (!comporta)
                throw new Exception("Laboratório não suporta a quantidade de alunos da disciplina.");
        }
    }
}