
using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Models.Enums;
using SolutisHelpDesk.Repositories;
using SolutisHelpDesk.Services.Interfaces;

namespace SolutisHelpDesk.Services;

public class ClienteService : IClienteService {
	private IMapper _mapper;
	private ClienteRepository _clienteRepository;
	private UsuarioService _usuarioService;

	public ClienteService(ClienteRepository clienteRepository, IMapper mapper, UsuarioService usuarioService) {
		_clienteRepository = clienteRepository;
		_mapper = mapper;
		_usuarioService = usuarioService;
	}

	public async Task<bool> RegistroClienteAsync(CreateClienteDto dto) {
		Cliente cliente = _mapper.Map<Cliente>(dto);
		cliente.Perfil = EnumPerfil.Cliente;


		var usuarioDto = _mapper.Map<CreateUsuarioDto>(dto);
		bool result = await _usuarioService.RegistrarUsuarioAsync(usuarioDto, EnumPerfil.Cliente); //Salvar com identity

		if (result) {
			await _clienteRepository.SalvarCliente(cliente); //Salvar na tabela individual
			return true;
		}

		return false;

	}

	public async Task<IEnumerable<ReadClienteDto>> GetAllAsync() {
		List<Cliente> listaClientes = await _clienteRepository.RecuperarClientesAsync();
		return _mapper.Map<List<ReadClienteDto>>(listaClientes);
	}

	public async Task<ReadClienteDto> GetByIdAsync(int id) {
		var cliente = await _clienteRepository.RecuperarClientePorIdAsync(id);
		return _mapper.Map<ReadClienteDto>(cliente);
	}

	public async Task<ReadClienteDto> GetByUsernameAsync(string username) {
		var cliente = await _clienteRepository.RecuperarClientePorUserNameAsync(username);
		return _mapper.Map<ReadClienteDto>(cliente);
	}

	public async Task<bool> DeleteAsync(int id) {
		var cliente = await _clienteRepository.RecuperarClientePorIdAsync(id);

		if (cliente == null) {
			return false;
		}

		await _clienteRepository.DeleteAsync(cliente);
		await _usuarioService.DeletarUsuarioAsync(cliente.UserName);
		return true;
	}

	public async Task<bool> UpdateCliente(int id, UpdateClienteDto clienteDto) {
		var clienteExistente = await _clienteRepository.RecuperarClientePorIdAsync(id);

		if (clienteExistente == null) {
			return false;
		}

		_mapper.Map(clienteDto, clienteExistente);
		await _clienteRepository.AtualizarClienteAsync(clienteExistente);
		return true;
	}

}
