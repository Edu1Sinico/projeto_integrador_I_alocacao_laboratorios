using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;

namespace ProjetoIntegredor.model
{
    public class Alocacao
    {
        private static int contador = 1;
        public int IdAlocacao { get; private set; }
        public DateOnly Data { get; private set; }
        public TimeOnly HoraInicio { get; private set; }
        private TimeOnly horaFim;
        public Aprovacao StatusAprovacao { get; set; } = Aprovacao.P; // Define automaticamente a aprovação como "P - Pendente".
        public Laboratorio Laboratorio { get; private set; }
        public Disciplina Disciplina { get; private set; }
        public Usuario Usuario { get; private set; }

        public TimeOnly HoraFim
        {
            get => horaFim;
            set
            {
                if (value <= HoraInicio)
                    throw new ArgumentException("Horário final deve ser maior que o inicial!");
                horaFim = value;
            }
        }

        public Alocacao(DateOnly data, TimeOnly horaInicio, TimeOnly horaFim, Laboratorio lab, Disciplina disc, Usuario usuario)
        {
            IdAlocacao = contador++;
            Data = data;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            Laboratorio = lab;
            Disciplina = disc;
            Usuario = usuario;
        }

        // Alterar status de aprovação
        public void Aprovar()
        {
            StatusAprovacao = Aprovacao.A;
        }

        public void Reprovar()
        {
            StatusAprovacao = Aprovacao.R;
        }
    }
}