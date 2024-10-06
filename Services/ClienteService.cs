
using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Models.Enums;
using SolutisHelpDesk.Repositories;

namespace SolutisHelpDesk.Services;

public class ClienteService {
	private IMapper _mapper;
	private ClienteRepository _clienteRepository;

	public ClienteService(ClienteRepository clienteRepository, IMapper mapper) {
		_clienteRepository = clienteRepository;
		_mapper = mapper;
	}	

	internal async Task<Cliente> RegistroClienteAsync(CreateClienteDto dto) {
		Cliente cliente = _mapper.Map<Cliente>(dto);
		cliente.Perfil = EnumPerfil.Cliente;

		await _clienteRepository.SalvarCliente(cliente);
		return cliente;
	}

	internal async Task<IEnumerable<ReadClienteDto>> GetAllAsync() {
		List<Cliente> listaClientes = await _clienteRepository.RecuperarClientesAsync();
		return _mapper.Map<List<ReadClienteDto>>(listaClientes);
	}

	internal async Task<ReadClienteDto> GetByIdAsync(int id) {
		var cliente = await _clienteRepository.RecuperarClientePorIdAsync(id);
		return _mapper.Map<ReadClienteDto>(cliente);
	}

	internal async Task<bool> DeleteAsync(int id) {
		var cliente = await _clienteRepository.RecuperarClientePorIdAsync(id);

		if (cliente == null) {
			return false;
		}

		await _clienteRepository.DeleteAsync(cliente);
		return true;
	}

	internal async Task<bool> UpdateCliente(int id, UpdateClienteDto clienteDto) {
		var clienteExistente = await _clienteRepository.RecuperarClientePorIdAsync(id);

		if (clienteExistente == null) {
			return false;
		}

		_mapper.Map(clienteDto, clienteExistente);		
		await _clienteRepository.AtualizarClienteAsync(clienteExistente);
		return true;
	}

}
