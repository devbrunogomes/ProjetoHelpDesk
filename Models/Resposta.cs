using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SolutisHelpDesk.Models;

public class Resposta {
	[Key]
	public int Id { get; set; }
	public DateTime Data { get; set; }
	public string Mensagem { get; set; }
	public string Autor { get; set; }
	public int ChamadoId { get; set; }
	[JsonIgnore]
	public virtual Chamado Chamado { get; set; }
}
