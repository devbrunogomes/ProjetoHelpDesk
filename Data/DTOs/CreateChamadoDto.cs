using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SolutisHelpDesk.Data.DTOs;

public class CreateChamadoDto {

	[Required(ErrorMessage = "O campo Título é obrigatório.")]
	public string Titulo { get; set; }

	[Required(ErrorMessage = "O campo Descrição é obrigatório.")]
	public string Descricao { get; set; }

	[JsonIgnore]
	public int ClienteId { get; set; }
}
