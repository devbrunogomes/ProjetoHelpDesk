
using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Models.Enums;
using SolutisHelpDesk.Repositories;
using System.Security.Claims;

namespace SolutisHelpDesk.Services;

public class ChamadoService {
	private IMapper _mapper;
	private ChamadoRepository _chamadoRepository;
	private TokenService _tokenService;
	private ClienteService _clienteService;

	public ChamadoService(IMapper mapper, ChamadoRepository chamadoRepository, TokenService tokenService, ClienteService clienteService) {
		_mapper = mapper;
		_chamadoRepository = chamadoRepository;
		_tokenService = tokenService;
		_clienteService = clienteService;
	}


	internal async Task<Chamado> RegistroChamadaAsync(CreateChamadoDto chamadoDto, ClaimsPrincipal user) {
		string username = _tokenService.GetUsernameFromToken(user);
		int clienteId = _clienteService.GetByUsernameAsync(username).Result.ClienteId;
		chamadoDto.ClienteId = clienteId;

		Chamado chamado = _mapper.Map<Chamado>(chamadoDto);	
		await _chamadoRepository.SalvarChamado(chamado);
		return chamado;
	}
	internal async Task<IEnumerable<ReadChamadoDto>> GetAllAsync() {
		List<Chamado> listaChamado = await _chamadoRepository.RecuperarChamadosAsync();
		return _mapper.Map<List<ReadChamadoDto>>(listaChamado);
	}

	internal async Task<ReadChamadoDto> GetByIdAsync(int id) {
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(id);
		return _mapper.Map<ReadChamadoDto>(chamado);
	}
	internal async Task<IEnumerable<ReadChamadoDto>> GetChamadosDoCliente(ClaimsPrincipal user) {
		string username = _tokenService.GetUsernameFromToken(user);
		int clienteId = _clienteService.GetByUsernameAsync(username).Result.ClienteId;

		List<Chamado> listaChamado = await _chamadoRepository.RecuperarChamadosDeCliente(clienteId);
		return _mapper.Map<List<ReadChamadoDto>>(listaChamado);
	}

	internal async Task<bool> RegistrarRespostaTecnicaAsync(ResponderChamadoDto dto) {
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(dto.ChamadoId);

		if (chamado == null || chamado.Status == EnumStatus.Fechado) {
			return false;
		}

		chamado.Status = EnumStatus.EmAndamento;
		_mapper.Map(dto, chamado);
		await _chamadoRepository.UpdateChamadoAsync(chamado);
		return true;

	}

	internal async Task<bool> FinalizarChamadoAsync(FinalizarChamadoDto dto) {
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(dto.ChamadoId);

		if (chamado == null) {
			return false;
		}

		chamado.Status = EnumStatus.Fechado;
		chamado.DataConclusao = DateTime.Now;
		_mapper.Map(dto, chamado);
		await _chamadoRepository.UpdateChamadoAsync(chamado);
		return true;
	}

	internal async Task<bool> ReatribuirChamadoAsync(ReatribuirChamadoDto dto) {
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(dto.ChamadoId);

		if (chamado == null || chamado.Status == EnumStatus.Fechado) {
			return false;
		}

		chamado.TecnicoId = dto.TecnicoId;
		_mapper.Map(dto, chamado);
		await _chamadoRepository.UpdateChamadoAsync(chamado);
		return true;
	}

	internal async Task<bool> DeleteAsync(int id) {
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(id);

		if (chamado == null) {
			return false;
		}

		await _chamadoRepository.DeleteAsync(chamado);
		return true;
	}

}
