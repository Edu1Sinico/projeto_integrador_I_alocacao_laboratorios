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
        public DateOnly Data { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFim { get; set; }
        public Aprovacao StatusAprovacao { get; set; } = Aprovacao.P; // Define automaticamente a aprovação como "P - Pendente".

        public Alocacao(DateOnly data, TimeOnly horaInicio, TimeOnly horaFim)
        {
            IdAlocacao = contador++;
            Data = data;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }

    }
}