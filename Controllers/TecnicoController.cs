using Microsoft.AspNetCore.Mvc;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Services;

namespace SolutisHelpDesk.Controllers;

[ApiController]
[Route("[controller]")]
public class TecnicoController : ControllerBase	{
	private TecnicoService _tecnicoService;

	public TecnicoController(TecnicoService tecnicoService) {
		_tecnicoService = tecnicoService;
	}

	[HttpPost]
	public async Task<IActionResult> RegistrarTecnicoAsync(CreateTecnicoDto dto) {
		var Cliente = await _tecnicoService.RegistroTecnicoAsync(dto);
		return Ok(Cliente);
		
	}

	[HttpGet]
	public async Task<IActionResult> GetAllTecnicosAsync() {
		IEnumerable<ReadTecnicoDto> listaDto = await _tecnicoService.GetAllAsync();
		return Ok(listaDto);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetTecnicoById(int id) {
		ReadTecnicoDto tecnicoDto = await _tecnicoService.GetByIdAsync(id);

		if (tecnicoDto == null)
			return NotFound();

		return Ok(tecnicoDto);
	}

	[HttpGet("username-{username}")]
	public async Task<IActionResult> GetClienteByUserName(string username) {
		ReadTecnicoDto tecnicoDto = await _tecnicoService.GetByUsernameAsync(username);

		if (tecnicoDto == null)
			return NotFound();

		return Ok(tecnicoDto);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteTecnicoAsync(int id) {
		var result = await _tecnicoService.DeleteAsync(id);

		if (result) {
			return NoContent();
		}

		return NotFound();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateTecnico(int id, [FromBody] UpdateTecnicoDto tecnicoDto) {
		var resultado = await _tecnicoService.UpdateTecnico(id, tecnicoDto);

		if (!resultado) {
			return NotFound("Tecnico não encontrado");
		}
		return NoContent();
	}

}
