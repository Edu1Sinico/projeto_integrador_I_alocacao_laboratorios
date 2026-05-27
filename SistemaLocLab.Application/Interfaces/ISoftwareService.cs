using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaLocLab.Application.Interfaces
{
    public interface ISoftwareService
    {
        List<SoftwareDTO> ObterSoftwares();
        SoftwareDTO ObterSoftwareId (int id);
        List<SoftwareDTO> BuscarSoftwaresNome(string nome);
        SoftwareDTO CriarSoftware(CreateSoftwareDTO dto);
        SoftwareDTO AtualizarSoftware(int id, UpdateSoftwareDTO dto);
        bool RemoverSoftware(int id);
    }
}