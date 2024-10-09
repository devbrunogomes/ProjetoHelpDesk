using SolutisHelpDesk.Models.Enums;
using SolutisHelpDesk.Models;
using System.ComponentModel.DataAnnotations;

namespace SolutisHelpDesk.Data.DTOs;

public class ReadChamadoDto {	
	public int ChamadoId { get; set; }	
	public string Titulo { get; set; }
	public string Descricao { get; set; }	
	public EnumPrioridade Prioridade { get; set; }	
	public DateTime DataAbertura { get; set; }	
	public DateTime DataConclusao { get; set; }
	public EnumStatus Status { get; set; }
	public int ClienteId { get; set; }
	public int TecnicoId { get; set; }
	public string RespostasTecnicas { get; set; }
}
