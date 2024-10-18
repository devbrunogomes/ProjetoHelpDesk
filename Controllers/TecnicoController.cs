using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Services;

namespace SolutisHelpDesk.Controllers;

[ApiController]
[Route("[controller]")]
public class TecnicoController : ControllerBase {
	private TecnicoService _tecnicoService;

	public TecnicoController(TecnicoService tecnicoService) {
		_tecnicoService = tecnicoService;
	}

	[Authorize(Roles = "ADMINISTRADOR")]
	[HttpPost]
	public async Task<IActionResult> RegistrarTecnicoAsync([FromBody] CreateTecnicoDto dto) {
		// Verifica se o modelo enviado no body é válido
		if (!ModelState.IsValid) {
			return BadRequest(ModelState);
		}

		var sucess = await _tecnicoService.RegistroTecnicoAsync(dto);

		if (sucess) {
			return CreatedAtAction(nameof(GetTecnicoByUserName), new { userName = dto.UserName }, "Técnico registrado com sucesso.");
		}

		return BadRequest("Problema ao registrar técnico, verifique as informações passadas");

	}

	[Authorize(Roles = "ADMINISTRADOR")]
	[HttpGet]
	public async Task<IActionResult> GetAllTecnicosAsync() {
		IEnumerable<ReadTecnicoDto> listaDto = await _tecnicoService.GetAllAsync();
		return Ok(listaDto);
	}

	[Authorize(Roles = "ADMINISTRADOR")]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetTecnicoById(int id) {
		ReadTecnicoDto tecnicoDto = await _tecnicoService.GetByIdAsync(id);

		if (tecnicoDto == null)
			return NotFound();

		return Ok(tecnicoDto);
	}

	[Authorize(Roles = "ADMINISTRADOR")]
	[HttpGet("username-{username}")]
	public async Task<IActionResult> GetTecnicoByUserName(string username) {
		ReadTecnicoDto tecnicoDto = await _tecnicoService.GetByUsernameAsync(username);

		if (tecnicoDto == null)
			return NotFound();

		return Ok(tecnicoDto);
	}

	[Authorize(Roles = "ADMINISTRADOR")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteTecnicoAsync(int id) {
		var result = await _tecnicoService.DeleteAsync(id);

		if (result) {
			return NoContent();
		}

		return NotFound();
	}

	[Authorize(Roles = "ADMINISTRADOR")]
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateTecnico(int id, [FromBody] UpdateTecnicoDto tecnicoDto) {
		var resultado = await _tecnicoService.UpdateTecnico(id, tecnicoDto);

		if (!resultado) {
			return NotFound("Tecnico não encontrado");
		}
		return NoContent();
	}

}
