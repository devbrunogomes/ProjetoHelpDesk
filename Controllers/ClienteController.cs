using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Services;

namespace SolutisHelpDesk.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase	{
	private ClienteService _clienteService;

	public ClienteController(ClienteService clienteService) {
		_clienteService = clienteService;
	}

	[HttpPost]
	public async Task<IActionResult> RegistrarClienteAsync(CreateClienteDto dto) {
		var Cliente = await _clienteService.RegistroClienteAsync(dto);
		return Ok(Cliente);
		
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
		return NoContent(); // 204 No Content para operações de atualização bem-sucedidas
	}

}
