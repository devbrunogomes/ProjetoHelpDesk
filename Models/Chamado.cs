using SolutisHelpDesk.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.ComTypes;

namespace SolutisHelpDesk.Models;

[Table("Chamados")]
public class Chamado {
	[Key]
	[Required(ErrorMessage = "O campo ChamadoId é obrigatório.")]
	public int ChamadoId { get; set; }

	[Required(ErrorMessage = "O campo Título é obrigatório.")]
	public string Titulo { get; set; }

	[Required(ErrorMessage = "O campo Descrição é obrigatório.")]
	public string Descricao { get; set; }

	[Required(ErrorMessage = "O campo Prioridade é obrigatório.")]
	public EnumPrioridade Prioridade { get; set; }

	[Required(ErrorMessage = "O campo Data de Abertura é obrigatório.")]
	public DateTime DataAbertura { get; set; }

	[Required(ErrorMessage = "O campo Data de Conclusão é obrigatório.")]
	public DateTime DataConclusao { get; set; }

	[Required(ErrorMessage = "O campo Status é obrigatório.")]
	public EnumStatus Status { get; set; }

	[Required(ErrorMessage = "O campo ClienteId é obrigatório.")]
	public int ClienteId { get; set; }

	[Required(ErrorMessage = "O campo TecnicoId é obrigatório.")]
	public int TecnicoId { get; set; }

	[Required(ErrorMessage = "O campo Cliente é obrigatório.")]
	public virtual Cliente Cliente { get; set; }

	[Required(ErrorMessage = "O campo Técnico é obrigatório.")]
	public virtual Tecnico Tecnico { get; set; }

	[Required(ErrorMessage = "O campo Respostas Técnicas é obrigatório.")]
	public string RespostasTecnicas { get; set; }

	public Chamado() {
		DataAbertura = DateTime.Now;
		Status = EnumStatus.Aberto;
	}
}
