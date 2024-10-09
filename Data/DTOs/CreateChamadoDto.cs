using System.ComponentModel.DataAnnotations;

namespace SolutisHelpDesk.Data.DTOs;

public class CreateChamadoDto {

	[Required(ErrorMessage = "O campo Título é obrigatório.")]
	public string Titulo { get; set; }

	[Required(ErrorMessage = "O campo Descrição é obrigatório.")]
	public string Descricao { get; set; }

	[Required(ErrorMessage = "O campo ClienteId é obrigatório.")]
	public int ClienteId { get; set; }
}
