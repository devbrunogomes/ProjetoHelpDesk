using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Services;
using SolutisHelpDesk.Services.Interfaces;

namespace SolutisHelpDesk.Controllers;

[ApiController]
[Route("[controller]")]
public class ChamadoController : ControllerBase {
	private ChamadoService _chamadoService;
	private IUsuarioService _usuarioService;

	public ChamadoController(ChamadoService chamadoService, IUsuarioService usuarioService = null) {
		_chamadoService = chamadoService;
		_usuarioService = usuarioService;
	}

	[Authorize(Roles = "CLIENTE")]
	[HttpPost]
	public async Task<IActionResult> RegistrarChamadoAsync(CreateChamadoDto chamadoDto) {
		var chamado = await _chamadoService.RegistroChamadaAsync(chamadoDto, User);
		return CreatedAtAction(nameof(GetChamadoById), new { id = chamado.ChamadoId }, chamado);
	}

	[Authorize(Roles = "TECNICO, ADMINISTRADOR")]
	[HttpGet]
	public async Task<IActionResult> GetAllChamadosAsync() {
		IEnumerable<ReadChamadoDto> listaDto = await _chamadoService.GetAllAsync();
		return Ok(listaDto);
	}

	[Authorize(Roles = "TECNICO, ADMINISTRADOR")]
	[HttpGet("chamados-abertos")]
	public async Task<IActionResult> GetAllChamadosAbertosAsync() {
		IEnumerable<ReadChamadoDto> listaDto = await _chamadoService.GetAllOpenAsync();
		return Ok(listaDto);
	}

	[Authorize(Roles = "TECNICO, ADMINISTRADOR")]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetChamadoById(int id) {
		ReadChamadoDto chamadoDto = await _chamadoService.GetByIdAsync(id);

		if (chamadoDto == null)
			return NotFound();

		return Ok(chamadoDto);
	}

	[Authorize(Roles = "CLIENTE")]
	[HttpGet("/cliente/meus-chamados")]
	public async Task<IActionResult> GetChamadosDoCliente() {
		IEnumerable<ReadChamadoDto> listaDto = await _chamadoService.GetChamadosDoCliente(User);
		return Ok(listaDto);
	}

	[Authorize(Roles = "TECNICO")]
	[HttpGet("/tecnico/meus-chamados")]
	public async Task<IActionResult> GetChamadosDoTecnico() {
		IEnumerable<ReadChamadoDto> listaDto = await _chamadoService.GetChamadosDoTecnico(User);
		return Ok(listaDto);
	}

	[Authorize(Roles = "TECNICO, ADMINISTRADOR")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteChamadoAsync(int id) {
		var result = await _chamadoService.DeleteAsync(id);

		if (result) {
			return NoContent();
		}

		return NotFound();
	}
	[Authorize(Roles = "TECNICO")]
	[HttpPatch("finalizar-chamado")]
	public async Task<IActionResult> FinalizarChamado(FinalizarChamadoDto dto) {
		//Verificar se o id do tecnico atual é igual ao id do tecnico já atribuido ao chamado		
		bool ehOMesmoTecnico = await _chamadoService.ValidarIgualdadeDeTecnico(User, dto.ChamadoId);

		if (!ehOMesmoTecnico) {
			return BadRequest("Este chamado é de outro técnico");
		}

		var result = await _chamadoService.FinalizarChamadoAsync(dto, User);

		if (!result) {
			return NotFound("Chamado não encontrado ou Sem Resposta");
		}
		return NoContent();

	}

	[Authorize(Roles = "TECNICO")]
	[HttpPatch("reatribuir-tecnico")]
	public async Task<IActionResult> ReatribuirChamado(ReatribuirChamadoDto dto) {
		//Verificar se Técnico Existe
		var tecnicoExiste = await _usuarioService.VerificarUsernamelExistenteAsync(dto.TecnicoUsername);

		if (!tecnicoExiste) {
			return NotFound("Técnico não encontrado!");
		}

		var result = await _chamadoService.ReatribuirChamadoAsync(dto);

		if (!result) {
			return NotFound("Chamado não encontrado");
		}
		return NoContent();

	}

	[Authorize(Roles = "TECNICO")]
	[HttpPatch("alterar-prioridade")]
	public async Task<IActionResult> AlterarPrioridade(AlterarPrioridadeChamadoDto dto) {
		//Verificar se o id do tecnico atual é igual ao id do tecnico já atribuido ao chamado		
		bool ehOMesmoTecnico = await _chamadoService.ValidarIgualdadeDeTecnico(User, dto.ChamadoId);

		if (!ehOMesmoTecnico) {
			return BadRequest("Este chamado é de outro técnico");
		}

		var result = await _chamadoService.AlterarPrioridadeAsync(dto);

		if (!result) {
			return NotFound("Chamado não encontrado");
		}

		return NoContent();
	}

	[Authorize(Roles = "ADMINISTRADOR")]
	[HttpGet("chamados-dashboard")]
	public async Task<IActionResult> GetDadosChamadosParaGrafico() {
		var result = await _chamadoService.RetornarDadosChamadosDashboard();
		return Ok(result);
	}

	[Authorize(Roles = "ADMINISTRADOR")]
	[HttpGet("tecnicos-dashboard")]
	public async Task<IActionResult> GetDadosTecnicosParaGrafico() {
		var result = await _chamadoService.RetornarDadosTecnicosDashboard();
		return Ok(result);
	}
}
