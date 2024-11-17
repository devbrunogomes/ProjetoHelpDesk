using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models.Enums;
using System.Security.Claims;

namespace SolutisHelpDesk.Services.Interfaces;

public interface IUsuarioService {
	Task DeletarUsuarioAsync(string userName);
	Task<string> Login(LoginDto loginDto);
	Task<bool> RegistrarUsuarioAsync(CreateUsuarioDto usuarioDto, EnumPerfil cliente);
	Task<bool> TrocarSenhaAsync(ClaimsPrincipal user, TrocarSenhaDto trocarSenhaDto);
	Task<bool> VerificarEmailExistenteAsync(string email);
	Task<bool> VerificarUsernamelExistenteAsync(string username);
}
