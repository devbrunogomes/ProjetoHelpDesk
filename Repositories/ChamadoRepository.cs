using Microsoft.EntityFrameworkCore;
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
	internal async Task<List<Chamado>> RecuperarChamadosAsync() {
		return await _context.Chamados.ToListAsync();
	}

	internal async Task<Chamado?> RecuperarChamadoPorIdAsync(int id) {
		var chamado = await _context.Chamados
			.FirstOrDefaultAsync(chamado => chamado.ClienteId == id);
		return chamado;
	}

	internal async Task RegistrarRespostaAsync(Chamado chamado) {
		_context.Chamados.Update(chamado);
		await _context.SaveChangesAsync();
	}
}
