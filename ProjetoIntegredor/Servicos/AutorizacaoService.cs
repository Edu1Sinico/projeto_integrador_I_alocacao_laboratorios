using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;

namespace ProjetoIntegredor.Servicos
{
    public class AutorizacaoService
    {
        // Validação dos usuários (evita que alguém acesse sem estar logado)
        public static void ValidarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));
        }

        // Validação de funcionalidades do diretor
        public static void ValidarDiretor(Usuario usuario)
        {
            if (usuario.Tipo != TipoUsuario.DI) // Valida se o tipo de usuário está de acordo como 'diretor'
                throw new UnauthorizedAccessException("Apenas diretores podem executar essa ação!");
        }

        // Validação de funcionalidades do Coordenador
        public static void ValidarCoordenador(Usuario usuario)
        {
            if (usuario.Tipo != TipoUsuario.CO) // Valida se o tipo de usuário está de acordo como 'coordenador'
                throw new UnauthorizedAccessException("Apenas coordenadores podem executar essa ação!");
        }

        // Validação de funcionalidades do Responsável de TI
        public static void ValidarResponsavelTI(Usuario usuario)
        {
            if (usuario.Tipo != TipoUsuario.RT) // Valida se o tipo de usuário está de acordo como 'Responsável de TI'
                throw new UnauthorizedAccessException("Apenas os responsáveis do TI podem executar essa ação!");
        }
    }
}