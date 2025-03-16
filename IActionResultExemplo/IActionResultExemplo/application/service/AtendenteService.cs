using IActionResultExemplo.application.dto;
using IActionResultExemplo.config.exception;
using IActionResultExemplo.domain.models;
using IActionResultExemplo.domain.repository;
using IActionResultExemplo.infrastructure.atendente;
using Lombok.NET;
using Microsoft.AspNetCore.Identity;

namespace IActionResultExemplo.application.service
{
    [RequiredArgsConstructor]
    public partial class AtendenteService : IAtendenteService
    {
        private readonly IAtendenteRepository _repo;
        private readonly IPasswordHasher<Atendente> _passwordHasher;

        public async Task SalvarAtendenteAsync(AtendenteRegistrationRequest request)
        {
            var atendenteExistente = await _repo.EncontraPorEmailAsync(request.Email);
            if (atendenteExistente != null)
                throw ExceptionFactory.EntityAlreadyExists(request.Email);

            var atendente = AtendenteMapper.ToAtendente(request);

            atendente.Senha = _passwordHasher.HashPassword(atendente, request.Senha);

            var atendenteAdicionado = await _repo.AdicionarAtendenteAsync(atendente);
        }
    }
}
