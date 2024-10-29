using Microsoft.EntityFrameworkCore;
using SolutisHelpDesk.Data;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Models.Enums;

namespace SolutisHelpDesk.Repositories;

public class ChamadoRepository {
	private UsuarioContext _context;

	public ChamadoRepository(UsuarioContext context) {
		_context = context;
	}


	internal async Task SalvarChamado(Chamado chamado) {
		_context.Chamados.Add(chamado);
		await _context.SaveChangesAsync();
	}
	internal async Task<List<Chamado>> RecuperarChamadosAsync() {
		return await _context.Chamados.ToListAsync();
	}
	internal async Task<List<Chamado>> RecuperarChamadosAbertosAsync() {
		return await _context.Chamados
			.Where(chamado => chamado.Status == EnumStatus.Aberto)
			.ToListAsync();
	}

	internal async Task<Chamado?> RecuperarChamadoPorIdAsync(int id) {
		var chamado = await _context.Chamados
			.Include(chamado => chamado.Resposta)
			.FirstOrDefaultAsync(chamado => chamado.ChamadoId == id);
		return chamado;
	}
	internal async Task<List<Chamado>> RecuperarChamadosDeCliente(int clienteId) {
		List<Chamado> chamados = await _context.Chamados
			 .Where(chamado => chamado.ClienteId == clienteId)
			 .Include(chamado => chamado.Resposta)
			 .ToListAsync();
		return chamados;
	}
	internal async Task<List<Chamado>> RecuperarChamadosDeTecnico(int tecnicoId) {
		List<Chamado> chamados = await _context.Chamados
			 .Where(chamado => chamado.TecnicoId == tecnicoId)
			 .Include(chamado => chamado.Resposta)
			 .ToListAsync();
		return chamados;
	}

	internal async Task UpdateChamadoAsync(Chamado chamado) {
		_context.Chamados.Update(chamado);
		await _context.SaveChangesAsync();
	}

	internal async Task DeleteAsync(Chamado chamado) {
		_context.Chamados.Remove(chamado);
		await _context.SaveChangesAsync();
	}

}
