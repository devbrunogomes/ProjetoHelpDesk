using Microsoft.EntityFrameworkCore;
using SolutisHelpDesk.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolutisHelpDesk.Models;

[Table("Clientes")]
public class Cliente {
	[Key]
	[Required(ErrorMessage = "O campo ClienteId é obrigatório.")]
	public int ClienteId { get; set; }

	[Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
	public string NomeCompleto { get; set; }

	[Required(ErrorMessage = "O campo Email é obrigatório.")]
	[EmailAddress(ErrorMessage = "O formato do Email é inválido.")]
	public string Email { get; set; }

	[Required(ErrorMessage = "O campo Nome de Usuário é obrigatório.")]
	public string UserName { get; set; }

	[Required(ErrorMessage = "O campo CEP é obrigatório.")]
	public string Cep { get; set; }

	public EnumPerfil Perfil { get; set; } //O valor é armazenado como 0, 1 ou 2, dependendo do valor do Enum

	public virtual List<Chamado> Chamados { get; set; } = new List<Chamado>();

}
