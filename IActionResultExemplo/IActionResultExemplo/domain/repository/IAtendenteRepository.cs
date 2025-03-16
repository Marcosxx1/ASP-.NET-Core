using IActionResultExemplo.application.dto;
using IActionResultExemplo.domain.models;

namespace IActionResultExemplo.domain.repository
{
    public interface IAtendenteRepository
    {
        Task<Atendente?> EncontraPorEmailAsync(string email);
        Task<Atendente?> EncontraPorIdAsync(long id);
        Task SalvarAsync(Atendente atendente);
        Task<Atendente> AdicionarAtendenteAsync(Atendente request);
    }
}
