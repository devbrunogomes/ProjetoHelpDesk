using SolutisHelpDesk.Models.Enums;

namespace SolutisHelpDesk.Data.DTOs;

public class AlterarPrioridadeChamadoDto {
	public int ChamadoId { get; set; }
	public EnumPrioridade novaPrioridade { get; set; }
}
