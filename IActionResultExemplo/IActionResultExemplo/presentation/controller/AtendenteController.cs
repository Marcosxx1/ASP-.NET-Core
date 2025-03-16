using IActionResultExemplo.application.dto;
using IActionResultExemplo.application.service;
using Lombok.NET;
using Microsoft.AspNetCore.Mvc;

namespace IActionResultExemplo.presentation.controller
{

    [ApiController]
    [Route("api/[controller]")]
    [RequiredArgsConstructor]
    public partial class AtendenteController : ControllerBase
    {
        private readonly IAtendenteService _atendenteService;

        [HttpPost]
        public async Task<IActionResult> RegistrarAtendente([FromBody] AtendenteRegistrationRequest request)
        {
           await _atendenteService.SalvarAtendenteAsync(request);
            return NoContent();
        }
    }
}
