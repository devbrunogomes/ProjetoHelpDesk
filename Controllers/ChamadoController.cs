using Microsoft.AspNetCore.Authorization;
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

	[Authorize(Roles = "CLIENTE")]
	[HttpPost]
	public async Task<IActionResult> RegistrarChamadoAsync(CreateChamadoDto chamadoDto) {
		var chamado = await _chamadoService.RegistroChamadaAsync(chamadoDto, User);
		return CreatedAtAction(nameof(GetChamadoById), new { id = chamado.ChamadoId }, chamado);
	}

	[Authorize(Roles = "TECNICO,ADMINISTRADOR")]
	[HttpGet]
	public async Task<IActionResult> GetAllChamadosAsync() {
		IEnumerable<ReadChamadoDto> listaDto = await _chamadoService.GetAllAsync();
		return Ok(listaDto);
	}

	[Authorize(Roles = "TECNICO,ADMINISTRADOR")]
	[HttpGet("chamados-abertos")]
	public async Task<IActionResult> GetAllChamadosAbertosAsync() {
		IEnumerable<ReadChamadoDto> listaDto = await _chamadoService.GetAllOpenAsync();
		return Ok(listaDto);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetChamadoById(int id) {
		ReadChamadoDto chamadoDto = await _chamadoService.GetByIdAsync(id);

		if (chamadoDto == null)
			return NotFound();

		return Ok(chamadoDto);
	}

	[Authorize(Roles = "CLIENTE")]
	[HttpGet("meus-chamados")]
	public async Task<IActionResult> GetChamadosDoCliente() {
		IEnumerable<ReadChamadoDto> listaDto = await _chamadoService.GetChamadosDoCliente(User);
		return Ok(listaDto);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteChamadoAsync(int id) {
		var result = await _chamadoService.DeleteAsync(id);

		if (result) {
			return NoContent();
		}

		return NotFound();
	}

	[Authorize(Roles = "TECNICO")]
	[HttpPatch("resposta-chamado")]
	public async Task<IActionResult> DarRespostaTecnica(ResponderChamadoDto dto) {
		var result = await _chamadoService.RegistrarRespostaTecnicaAsync(dto);

		if (!result) {
			return NotFound("Chamado não encontrado");
		}
		return NoContent();
	}

	[Authorize(Roles = "TECNICO")]
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
