using System.ComponentModel.DataAnnotations;

namespace SolutisHelpDesk.Data.DTOs;

public class CreateTecnicoDto {
	[Required]
	public string NomeCompleto { get; set; }

	[Required]
	[EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
	public string Email { get; set; }

	[Required]
	[Compare("Email")]
	public string EmailConfirmation { get; set; }

	[Required]
	public string UserName { get; set; }

	[Required]
	public string Password { get; set; }

	[Required]
	[Compare("Password")]
	public string RePassword { get; set; }

	[Required]
	public string Cep { get; set; }
}
