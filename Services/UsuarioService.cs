using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Models.Enums;

namespace SolutisHelpDesk.Services;

public class UsuarioService {
	private readonly UserManager<Usuario> _userManager;
	private readonly IMapper _mapper;

	public UsuarioService(UserManager<Usuario> userManager, IMapper mapper) {
		_userManager = userManager;
		_mapper = mapper;
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

		// Se houver erros, você pode lidar com eles ou retornar informações úteis
		foreach (var erro in resultado.Errors) {
			// Você pode fazer logging ou tratamento de erro
		}

		return false;
	}

	
}
