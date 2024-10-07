﻿using SolutisHelpDesk.Models.Enums;
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

	public EnumPrioridade? Prioridade { get; set; }

	[Required(ErrorMessage = "A Data de Abertura é obrigatória.")]
	public DateTime DataAbertura { get; set; }
	
	public DateTime? DataConclusao { get; set; }

	public EnumStatus? Status { get; set; }
	
	public int? ClienteId { get; set; }
	
	public int? TecnicoId { get; set; }

	public virtual Cliente? Cliente { get; set; }

	public virtual Tecnico? Tecnico { get; set; }

	public string? RespostasTecnicas { get; set; }

	public Chamado() {
		DataAbertura = DateTime.Now;
		Status = EnumStatus.Aberto;
	}
}