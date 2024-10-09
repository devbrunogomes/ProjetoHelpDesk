using SolutisHelpDesk.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SolutisHelpDesk.Data.DTOs;

public class ReadClienteDto {
	public int ClienteId { get; set; }
	public string NomeCompleto { get; set; }
	public string Email { get; set; }
	public string UserName { get; set; }
	public string Cep { get; set; }
	public EnumPerfil Perfil { get; set; } //O valor é armazenado como 0, 1 ou 2, dependendo do valor do Enum

	public List<ReadChamadoDto> Chamados { get; set; }
}
