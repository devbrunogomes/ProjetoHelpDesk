using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SolutisHelpDesk.Data.DTOs;

public class FinalizarChamadoDto {	
	[Required]
	public int ChamadoId { get; set; }

}
