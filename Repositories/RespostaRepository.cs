using SolutisHelpDesk.Data;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Repositories;

public class RespostaRepository {
	private UsuarioContext _context;

	public RespostaRepository(UsuarioContext context) {
		_context = context;
	}

	internal async Task SalvarResposta(Resposta resposta) {
		_context.Respostas.Add(resposta);
		await _context.SaveChangesAsync();
	}
}
