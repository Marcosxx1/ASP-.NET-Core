using IActionResultExemplo.application.dto;
using IActionResultExemplo.domain.models;

namespace IActionResultExemplo.application.service
{
    public interface IAtendenteService
    {
        Task SalvarAtendenteAsync(AtendenteRegistrationRequest request);
    }

}
