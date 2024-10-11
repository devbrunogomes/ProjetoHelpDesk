using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Services;

namespace SolutisHelpDesk.Controllers;

[Authorize(Roles = "ADMINISTRADOR")]
[ApiController]
[Route("[controller]")]
public class AdministradorController : ControllerBase {
	private AdministradorService _administradorService;

	public AdministradorController(AdministradorService administradorService) {
		_administradorService = administradorService;
	}

	[HttpPost]
	public async Task<IActionResult> RegistrarAdministradorAsync(CreateAdministradorDto dto) {
		Administrador adm = await _administradorService.RegistroAdmAsync(dto);
		return Ok(adm);

	}

	[HttpGet]
	public async Task<IActionResult> GetAllAdministradoresAsync() {
		IEnumerable<ReadAdministradorDto> listaDto = await _administradorService.GetAllAsync();
		return Ok(listaDto);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetAdministradorById(int id) {
		ReadAdministradorDto admDto = await _administradorService.GetByIdAsync(id);

		if (admDto == null)
			return NotFound();

		return Ok(admDto);
	}

	[HttpGet("username-{username}")]
	public async Task<IActionResult> GetAdministradorByUserName(string username) {
		ReadAdministradorDto admDto = await _administradorService.GetByUsernameAsync(username);

		if (admDto == null)
			return NotFound();

		return Ok(admDto);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteAdministradorAsync(int id) {
		var result = await _administradorService.DeleteAsync(id);

		if (result) {
			return NoContent();
		}

		return NotFound();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateAdministrador(int id, [FromBody] UpdateAdministradorDto admDto) {
		var resultado = await _administradorService.UpdateAdministrador(id, admDto);
		if (!resultado) {
			return NotFound("Administrador não encontrado");
		}
		return NoContent();
	}
}
