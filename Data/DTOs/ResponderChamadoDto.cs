using SolutisHelpDesk.Models.Enums;

namespace SolutisHelpDesk.Data.DTOs;

public class ResponderChamadoDto {
	public int TecnicoId { get; set; }

	public int ChamadoId { get; set; }

	public string RespostasTecnicas { get; set; }

	public EnumPrioridade Prioridade { get; set; }
}
