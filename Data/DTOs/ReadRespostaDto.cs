using SolutisHelpDesk.Models;
using System.ComponentModel.DataAnnotations;

namespace SolutisHelpDesk.Data.DTOs;

public class ReadRespostaDto {
	public DateTime Data { get; set; }
	public string Autor { get; set; }
	public string Mensagem { get; set; }

}
