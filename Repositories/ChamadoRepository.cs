using SolutisHelpDesk.Data;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Repositories;

public class ChamadoRepository {
	private UsuarioContext _context;

	public ChamadoRepository(UsuarioContext context) {
		_context = context;
	}

	internal async Task SalvarChamado(Chamado chamado) {
		await _context.Chamados.AddAsync(chamado);
		_context.SaveChanges();	
	}
}
