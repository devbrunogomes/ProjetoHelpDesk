using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Models.Enums;
using SolutisHelpDesk.Services.Interfaces;
using System.Security.Claims;

namespace SolutisHelpDesk.Services;

public class UsuarioService : IUsuarioService{
	private readonly UserManager<Usuario> _userManager;
	private SignInManager<Usuario> _signInManager;
	private readonly IMapper _mapper;
	private TokenService _tokenService;

	public UsuarioService(UserManager<Usuario> userManager, IMapper mapper, SignInManager<Usuario> signInManager, TokenService tokenService) {
		_userManager = userManager;
		_mapper = mapper;
		_signInManager = signInManager;
		_tokenService = tokenService;
	}

	public async Task<bool> RegistrarUsuarioAsync(CreateUsuarioDto usuarioDto, EnumPerfil perfil) {
		// Mapear de DTO para Usuario
		var usuario = _mapper.Map<Usuario>(usuarioDto);

		usuario.Perfil = perfil; // Definindo o perfil de acordo com o argumento passado

		// Criar o usuário com o UserManager do Identity
		var resultado = await _userManager.CreateAsync(usuario, usuarioDto.Password);

		if (resultado.Succeeded && usuario != null) {
			await _userManager.AddToRoleAsync(usuario, perfil.ToString().ToUpper());
			return true;
		}


		return false;
	}

	public async Task<string> Login(LoginDto loginDto) {
		var resultado = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);

		if (!resultado.Succeeded) {
			throw new ApplicationException("Usuário não Autenticado");
		}

		var usuario = _signInManager
			.UserManager
			.Users
			.FirstOrDefault(user => user.NormalizedUserName == loginDto.UserName.ToUpper());


		var token = await _tokenService.GerarToken(usuario);

		return token;
	}

	public async Task DeletarUsuarioAsync(string username) {
		Usuario? usuario = await _userManager.FindByNameAsync(username);

		if (usuario != null) {

			var result = await _userManager.DeleteAsync(usuario);

			if (!result.Succeeded) {

				throw new Exception("Erro ao excluir o usuário do Identity.");
			}
		}
	}

	public async Task<bool> TrocarSenhaAsync(ClaimsPrincipal user, TrocarSenhaDto trocarSenhaDto) {
		var usuarioId = user?.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value!;
		var usuario = await _userManager.FindByIdAsync(usuarioId);

		if (usuario == null)
			throw new Exception("Usuário não encontrado.");

		var resultado = await _userManager.ChangePasswordAsync(usuario, trocarSenhaDto.SenhaAtual, trocarSenhaDto.NovaSenha);

		return resultado.Succeeded;

	}

	public async Task<bool> VerificarEmailExistenteAsync(string email) {
		var result = await _userManager.FindByEmailAsync(email);

		if (result == null) {
			return false;
		}

		return true;
	}

	public async Task<bool> VerificarUsernamelExistenteAsync(string username) {
		var result = await _userManager.FindByNameAsync(username);

		if (result == null) {
			return false;
		}

		return true;
	}
}
