using Microsoft.AspNetCore.Mvc;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Services;

namespace SolutisHelpDesk.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AcessoController : ControllerBase {
	private readonly UsuarioService _usuarioService;
	private readonly TokenService _tokenService;

	public AcessoController(UsuarioService usuarioService, TokenService tokenService) {
		_usuarioService = usuarioService;
		_tokenService = tokenService;
	}

	[HttpPost("login")]
	public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto) {
		var token = await _usuarioService.Login(loginDto);
		
		return Ok(token);
	}
}
