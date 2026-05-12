using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.model;

namespace ProjetoIntegredor.Servicos
{
    public class DisciplinaService
    {
        List<Disciplina> disciplinas = new();

        // Cadastrar
        public Disciplina CadastrarDisciplina(Usuario usuarioLogado, string nomeDisciplina, int qtdeAlunos, Software software)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarCoordenador(usuarioLogado);

            if (disciplinas.Any(s => s.NomeDisciplina.Equals(nomeDisciplina, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Disciplina já cadastrada!");

            var disciplina = new Disciplina(nomeDisciplina, qtdeAlunos);
            disciplina.AdicionarSoftware(software);
            disciplinas.Add(disciplina);
            return disciplina;
        }

        // Buscar
        public Disciplina? BuscarDisciplinaNome(string nomeDisciplina)
        {
            return disciplinas.FirstOrDefault(s => s.NomeDisciplina.Equals(nomeDisciplina, StringComparison.OrdinalIgnoreCase));
        }

        // Todos as disciplinas
        public List<Disciplina> BuscarDisciplinas()
        {
            return disciplinas.ToList();
        }

        // Atualizar
        public Disciplina? AtualizarDisciplina(Usuario usuarioLogado, int id, string nomeDisciplina, int qtdeAlunos)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarCoordenador(usuarioLogado);
            var disciplina = disciplinas.FirstOrDefault(s => s.IdDisciplina == id);

            if (disciplina == null)
                return null;

            disciplina.NomeDisciplina = nomeDisciplina;
            disciplina.QtdeAlunos = qtdeAlunos;

            return disciplina;
        }

        // Excluir
        public Disciplina? ExcluirDisciplina(Usuario usuarioLogado, int id)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarCoordenador(usuarioLogado);
            var disciplina = disciplinas.FirstOrDefault(s => s.IdDisciplina == id);

            if (disciplina != null)
                disciplinas.Remove(disciplina);

            return disciplina;
        }

        // Adicionando softwares para às disciplinas
        public Disciplina? AdicionarSoftwareDisciplina(Usuario usuarioLogado, int idDisciplina, Software software)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarCoordenador(usuarioLogado);

            var disciplina = disciplinas.FirstOrDefault(d => d.IdDisciplina == idDisciplina);

            if (disciplina == null)
                return null;

            disciplina.AdicionarSoftware(software);

            return disciplina;
        }

        // Remover os softwares das disciplinas
        public Disciplina? RemoverSoftwareDisciplina(Usuario usuarioLogado, int idDisciplina, Software software)
        {
            AutorizacaoService.ValidarUsuario(usuarioLogado);
            AutorizacaoService.ValidarCoordenador(usuarioLogado);

            var disciplina = disciplinas.FirstOrDefault(d => d.IdDisciplina == idDisciplina);

            if (disciplina == null)
                return null;

            disciplina.RemoverSoftware(software);

            return disciplina;
        }
    }
}