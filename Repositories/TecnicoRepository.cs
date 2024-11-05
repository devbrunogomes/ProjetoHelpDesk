using Microsoft.EntityFrameworkCore;
using SolutisHelpDesk.Data;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Repositories;

public class TecnicoRepository {
	private UsuarioContext _context;

	public TecnicoRepository(UsuarioContext context) {
		_context = context;
	}


	internal async Task SalvarTecnico(Tecnico tecnico) {
		await _context.Tecnicos.AddAsync(tecnico);
		_context.SaveChanges();		
	}
	internal async Task<List<Tecnico>> RecuperarTecnicosAsync() {
		return await _context.Tecnicos.ToListAsync();
	}

	internal async Task<Tecnico?> RecuperarTecnicoPorIdAsync(int id) {
		var tecnico = await _context.Tecnicos
			.Include(tecnico => tecnico.Chamados)
			.ThenInclude(chamado => chamado.Resposta)
			.FirstOrDefaultAsync(tecnico => tecnico.TecnicoId == id);
		return tecnico;
	}


	internal async Task<Tecnico?> RecuperarTecnicoPorUserNameAsync(string username) {		
			var tecnico = await _context.Tecnicos
				.Include(tecnico => tecnico.Chamados)
				.FirstOrDefaultAsync(tecnico => tecnico.UserName == username);
			return tecnico;
		}

	internal async Task DeleteAsync(Tecnico tecnico) {
		_context.Tecnicos.Remove(tecnico);
		await _context.SaveChangesAsync();
	}

	internal async Task AtualizarTecnicoAsync(Tecnico tecnico) {
		_context.Tecnicos.Update(tecnico);
		await _context.SaveChangesAsync();
	}

}
