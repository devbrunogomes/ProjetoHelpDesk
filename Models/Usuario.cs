using Microsoft.AspNetCore.Identity;
using SolutisHelpDesk.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SolutisHelpDesk.Models;

public class Usuario : IdentityUser<int> {
	[Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
	public string NomeCompleto { get; set; }

	[Required(ErrorMessage = "O campo CEP é obrigatório.")]
	public string Cep { get; set; }
	public EnumPerfil Perfil { get; set; } //O valor é armazenado como 0, 1 ou 2, dependendo do valor do Enum
}
