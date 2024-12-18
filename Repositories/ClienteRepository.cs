﻿using Microsoft.EntityFrameworkCore;
using SolutisHelpDesk.Data;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Repositories;

public class ClienteRepository {
	private UsuarioContext _context;

	public ClienteRepository(UsuarioContext context) {
		_context = context;

	}

	internal async Task SalvarCliente(Cliente cliente) {
		await _context.Clientes.AddAsync(cliente);
		_context.SaveChanges();
		
	}
	internal async Task<Cliente?> RecuperarClientePorIdAsync(int id) {
		var cliente = await _context.Clientes
			.Include(cliente => cliente.Chamados)
			.ThenInclude(chamado => chamado.Resposta)
			.FirstOrDefaultAsync(cliente => cliente.ClienteId == id);
		return cliente;
	}
	internal async Task<Cliente?> RecuperarClientePorUserNameAsync(string username) {
		var cliente = await _context.Clientes
			.Include(cliente => cliente.Chamados)
			.FirstOrDefaultAsync(cliente => cliente.UserName == username);
		return cliente;
	}

	internal async Task<List<Cliente>> RecuperarClientesAsync() {
		return await _context.Clientes.ToListAsync();
	}

	internal async Task DeleteAsync(Cliente cliente) {
		_context.Clientes.Remove(cliente);
		await _context.SaveChangesAsync();
	}

	internal async Task AtualizarClienteAsync(Cliente cliente) {
		_context.Clientes.Update(cliente);
		await _context.SaveChangesAsync();
	}

}
