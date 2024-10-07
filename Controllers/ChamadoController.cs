using Microsoft.AspNetCore.Mvc;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Services;

namespace SolutisHelpDesk.Controllers;

[ApiController]
[Route("[controller]")]
public class ChamadoController : ControllerBase {
	private ChamadoService _chamadoService;

	public ChamadoController(ChamadoService chamadoService) {
		_chamadoService = chamadoService;
	}

	[HttpPost]
	public async Task<IActionResult> RegistrarChamadoAsync(CreateChamadoDto chamadoDto) {
		var chamado = await _chamadoService.RegistroChamadaAsync(chamadoDto);
		return Ok(chamado);
	}

	//[HttpGet]
	//public async Task<IActionResult> GetAllChamadosAsync() {
	//	IEnumerable<ReadChamadoDto> listaDto = await _chamadoService.GetAllAsync();
	//	return Ok(listaDto);
	//}
}
