using SolutisHelpDesk.Models.Enums;
using System.Text.Json.Serialization;

namespace SolutisHelpDesk.Data.DTOs;

public class ResponderChamadoDto {

	[JsonIgnore]
	public int TecnicoId { get; set; }

	public int ChamadoId { get; set; }

	public string RespostasTecnicas { get; set; }

	public EnumPrioridade Prioridade { get; set; }
}
