using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Services;

namespace SolutisHelpDesk.Controllers;

[ApiController]
[Route("[controller]")]
public class RespostaController : ControllerBase {
	private RespostaService _respostaService;
	private ChamadoService _chamadoService;
	private TokenService _tokenService;
	private TecnicoService _tecnicoService;

	public RespostaController(RespostaService respostaService, ChamadoService chamadoService, TokenService tokenService, TecnicoService tecnicoService) {
		_respostaService = respostaService;
		_chamadoService = chamadoService;
		_tokenService = tokenService;
		_tecnicoService = tecnicoService;
	}

	[Authorize(Roles = "CLIENTE")]
	[HttpPost("cliente")]
	public async Task<IActionResult> DarRespostaAoTecnico(CreateRespostaDto dto) {
		var result = await _respostaService.RegistrarRespostaAoTecnicoAsync(dto, User);

		return Ok(result);
	}

	[Authorize(Roles = "TECNICO")]
	[HttpPost("tecnico")]
	public async Task<IActionResult> DarRespostaAoCliente(CreateRespostaDto dto) {
		//Verificar se o id do tecnico atual é igual ao id do tecnico já atribuido ao chamado		
		string username = _tokenService.GetUsernameFromToken(User);
		int tecnicoIdToken = _tecnicoService.GetByUsernameAsync(username).Result.TecnicoId;		
		int tecnicoIdAtribuido = await _chamadoService.GetIdDoTecnicoAtribuido(dto.ChamadoId);

		if (tecnicoIdAtribuido != 0 && tecnicoIdAtribuido != tecnicoIdToken) { 
			return BadRequest("Outro técnico já esta atribuido ao chamado");
		}


		var resposta = await _respostaService.RegistrarRespostaAoClienteAsync(dto, User);
		return Ok(resposta);
	}
}
