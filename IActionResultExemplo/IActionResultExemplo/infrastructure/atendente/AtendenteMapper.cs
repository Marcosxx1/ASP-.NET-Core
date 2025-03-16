using IActionResultExemplo.application.dto;
using IActionResultExemplo.domain.models;

namespace IActionResultExemplo.infrastructure.atendente
{
    public class AtendenteMapper
    {
        public static Atendente ToAtendente(AtendenteRegistrationRequest request)
        {
            var atendente = new Atendente
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha,
                Telefone = request.Telefone,
                DataNascimento = request.DataNascimento
            };

            return atendente;
        }
    }
}
