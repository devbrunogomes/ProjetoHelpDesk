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

	[HttpGet]
	public async Task<IActionResult> GetAllChamadosAsync() {
		IEnumerable<ReadChamadoDto> listaDto = await _chamadoService.GetAllAsync();
		return Ok(listaDto);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetChamadoById(int id) {
		ReadChamadoDto chamadoDto = await _chamadoService.GetByIdAsync(id);

		if (chamadoDto == null)
			return NotFound();

		return Ok(chamadoDto);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteChamadoAsync(int id) {
		var result = await _chamadoService.DeleteAsync(id);

		if (result) {
			return NoContent();
		}

		return NotFound();
	}

	[HttpPatch("resposta-chamado")]
	public async Task<IActionResult> DarRespostaTecnica(ResponderChamadoDto dto) {
		var result = await _chamadoService.RegistrarRespostaTecnicaAsync(dto);

		if (!result) {
			return NotFound("Chamado não encontrado");
		}
		return NoContent();
	}

	[HttpPatch("finalizar-chamado")]
	public async Task<IActionResult> FinalizarChamado(FinalizarChamadoDto dto) {
		var result = await _chamadoService.FinalizarChamadoAsync(dto);

		if (!result) {
			return NotFound("Chamado não encontrado");
		}
		return NoContent();

	}

	[HttpPatch("reatribuir-tenico")]
	public async Task<IActionResult> ReatribuirChamado(ReatribuirChamadoDto dto) {
		var result = await _chamadoService.ReatribuirChamadoAsync(dto);

		if (!result) {
			return NotFound("Chamado não encontrado");
		}
		return NoContent();

	}
}
