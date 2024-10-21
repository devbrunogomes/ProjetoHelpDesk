using System.ComponentModel.DataAnnotations;

namespace SolutisHelpDesk.Data.DTOs;

public class TrocarSenhaDto {
	[Required]
	public string SenhaAtual { get; set; }

	[Required]
	[MinLength(6, ErrorMessage = "A nova senha deve ter no mínimo 6 caracteres.")]
	public string NovaSenha { get; set; }
}
