using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaLocLab.Application.Interfaces
{
    public interface ISoftwareService
    {
        Task<List<SoftwareDTO>> ObterSoftwares();
        Task<SoftwareDTO?> ObterSoftwareId (Guid id);
        Task<List<SoftwareDTO>> BuscarSoftwaresNome(string nome);
        Task<SoftwareDTO> CriarSoftware(CreateSoftwareDTO dto);
        Task<SoftwareDTO?> AtualizarSoftware(Guid id, UpdateSoftwareDTO dto);
        Task<bool> RemoverSoftware(Guid id);
    }
}