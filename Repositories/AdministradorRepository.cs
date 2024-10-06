using Microsoft.EntityFrameworkCore;
using SolutisHelpDesk.Data;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Repositories;

public class AdministradorRepository {
	private UsuarioContext _context;

	public AdministradorRepository(UsuarioContext context) {
		_context = context;
	}

	internal async Task SalvarAdm(Administrador adm) {
		await _context.Administradores.AddAsync(adm);
		_context.SaveChanges();
	}

	internal async Task<List<Administrador>> RecuperarAdministradoresAsync() {
		return await _context.Administradores.ToListAsync();
	}

	internal async Task<Administrador?> RecuperarAdministradorPorIdAsync(int id) {
		var adm = await _context.Administradores.FirstOrDefaultAsync(adm => adm.AdministradorId == id);
		return adm;
	}

	internal async Task DeleteAsync(Administrador adm) {
		_context.Administradores.Remove(adm);
		await _context.SaveChangesAsync();
	}

	internal async Task AtualizarAdministradorAsync(Administrador adm) {
		_context.Administradores.Update(adm);
		await _context.SaveChangesAsync();
	}
}
