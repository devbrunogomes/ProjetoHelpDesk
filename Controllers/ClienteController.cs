using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Services;
using SolutisHelpDesk.Services.Interfaces;

namespace SolutisHelpDesk.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase {
	private IClienteService _clienteService;

	public ClienteController(IClienteService clienteService) {
		_clienteService = clienteService;
	}

	[HttpPost]
	public async Task<IActionResult> RegistrarClienteAsync([FromBody] CreateClienteDto dto) {
		// Verifica se o modelo enviado no body é válido
		if (!ModelState.IsValid) {
			return BadRequest(ModelState);
		}

		var sucess = await _clienteService.RegistroClienteAsync(dto);

		if (sucess) {
			// Retorna 201 (Created) com uma mensagem de sucesso
			return CreatedAtAction(nameof(GetClienteByUserName), new { userName = dto.UserName }, "Cliente registrado com sucesso.");
		}

		return BadRequest("Problema ao registrar cliente, verifique as informações passadas");


	}

	[Authorize(Roles = "ADMINISTRADOR")]
	[HttpGet]
	public async Task<IActionResult> GetAllClientesAsync() {
		IEnumerable<ReadClienteDto> listaDto = await _clienteService.GetAllAsync();
		return Ok(listaDto);
	}

	[Authorize(Roles = "TECNICO,ADMINISTRADOR")]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetClienteById(int id) {
		ReadClienteDto clienteDto = await _clienteService.GetByIdAsync(id);

		if (clienteDto == null)
			return NotFound();

		return Ok(clienteDto);
	}

	[Authorize(Roles = "TECNICO,ADMINISTRADOR")]
	[HttpGet("username-{username}")]
	public async Task<IActionResult> GetClienteByUserName(string username) {
		ReadClienteDto clienteDto = await _clienteService.GetByUsernameAsync(username);

		if (clienteDto == null)
			return NotFound();

		return Ok(clienteDto);
	}
	[Authorize(Roles = "ADMINISTRADOR")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteClienteAsync(int id) {
		var result = await _clienteService.DeleteAsync(id);

		if (result) {
			return NoContent();
		}

		return NotFound();
	}

	[Authorize(Roles = "ADMINISTRADOR")]
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateCliente(int id, [FromBody] UpdateClienteDto clienteDto) {
		var resultado = await _clienteService.UpdateCliente(id, clienteDto);
		if (!resultado) {
			return NotFound("Cliente não encontrado");
		}
		return NoContent();
	}

}
