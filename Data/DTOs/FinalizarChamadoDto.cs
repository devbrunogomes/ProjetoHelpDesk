using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SolutisHelpDesk.Data.DTOs;

public class FinalizarChamadoDto {
	[JsonIgnore]
	public int? TecnicoId { get; set; }

	[Required]
	public int ChamadoId { get; set; }

}
