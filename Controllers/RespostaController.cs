using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Services;

namespace SolutisHelpDesk.Controllers;

[ApiController]
[Route("[controller]")]
public class RespostaController : ControllerBase {
	private RespostaService _respostaService;

	public RespostaController(RespostaService respostaService) {
		_respostaService = respostaService;
	}

	[Authorize(Roles = "CLIENTE")]
	[HttpPost("cliente")]
	public async Task<IActionResult> DarRespostaAoTecnico(CreateRespostaDto dto) {
		var result = await _respostaService.RegistrarRespostaAsync(dto, User);

		return Ok(result);
	}
}
