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
            if (alocacoes.Any(a => a.Laboratorio == lab && a.Data == data && a.HoraInicio == horaInicio && a.HoraFim == horaFim))
                throw new ArgumentException("Essa sala já está alocada para esta data!");

            var alocacao = new Alocacao(data, horaInicio, horaFim, lab, disc, usuario);
            return alocacao;
        }

        // Aprovar uma alocação (função do diretor)
        public Alocacao AprovarAlocacao(Usuario usuario, Alocacao alocacao)
        {
            AutorizacaoService.ValidarUsuario(usuario);
            AutorizacaoService.ValidarDiretor(usuario);

            if (alocacao == null)
                return null;
            
            var aprovarAlocacao = alocacoes.FirstOrDefault(a => a.IdAlocacao == alocacao.IdAlocacao)
            aprovarAlocacao.StatusAprovacao = alocacao.AlterarStatusAprovacao(true); // Verificar erro
                
            return aprovarAlocacao;
        }

        // Aprovar uma alocação (função do diretor)
        public Alocacao ReprovarAlocacao(Usuario usuario, Alocacao alocacao)
        {
            AutorizacaoService.ValidarUsuario(usuario);
            AutorizacaoService.ValidarDiretor(usuario);

            if (alocacao == null)
                return null;

            return alocacao;
        }




    }
}