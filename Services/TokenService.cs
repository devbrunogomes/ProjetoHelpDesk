using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SolutisHelpDesk.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SolutisHelpDesk.Services;

public class TokenService {
	private readonly IConfiguration _configuration;
	private readonly UserManager<Usuario> _userManager;

	public TokenService(IConfiguration configuration, UserManager<Usuario> userManager) {
		_configuration = configuration;
		_userManager = userManager;
	}

	public async Task<string> GerarToken(Usuario usuario) {
		var claims = new List<Claim>
		{
				new Claim("id", usuario.Id.ToString()),
				new Claim("username", usuario.UserName!),
				new Claim("email", usuario.Email!),
				//new Claim(ClaimTypes.Role, usuario.Perfil.ToString())
		  };

		var roles = await _userManager.GetRolesAsync(usuario);
		claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			 _configuration["Jwt:Issuer"],
			 _configuration["Jwt:Audience"],
			 claims,
			 expires: DateTime.UtcNow.AddYears(5),
			 signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public string GetUsernameFromToken(ClaimsPrincipal user) {
		return user?.Claims.FirstOrDefault(claim => claim.Type == "username")?.Value!;
	}
}
