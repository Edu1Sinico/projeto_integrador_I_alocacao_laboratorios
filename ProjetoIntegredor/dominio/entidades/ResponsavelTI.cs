using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;

namespace ProjetoIntegredor.model
{
    public class ResponsavelTI : Usuario
    {
        public ResponsavelTI(int re, string nome, string senha, string email, TipoUsuario tipo) : base(re, nome, senha, email, tipo){}
    }
}