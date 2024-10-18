
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
	private TecnicoService _tecnicoService;
	private ClimaApiService _climaApiService;

	public ChamadoService(IMapper mapper, ChamadoRepository chamadoRepository, TokenService tokenService, ClienteService clienteService, TecnicoService tecnicoService, ClimaApiService climaApiService) {
		_mapper = mapper;
		_chamadoRepository = chamadoRepository;
		_tokenService = tokenService;
		_clienteService = clienteService;
		_tecnicoService = tecnicoService;
		_climaApiService = climaApiService;
		
	}


	internal async Task<Chamado> RegistroChamadaAsync(CreateChamadoDto chamadoDto, ClaimsPrincipal user) {
		string username = _tokenService.GetUsernameFromToken(user);
		int clienteId =  _clienteService.GetByUsernameAsync(username).Result.ClienteId;
		chamadoDto.ClienteId = clienteId;

		Chamado chamado = _mapper.Map<Chamado>(chamadoDto);
		await _chamadoRepository.SalvarChamado(chamado);

		//Conferencia do clima da regiao com Api externa
		await _climaApiService.ConferirClimaDaRegiao(chamado, user);

		return chamado;
	}
	internal async Task<IEnumerable<ReadChamadoDto>> GetAllAsync() {
		List<Chamado> listaChamado = await _chamadoRepository.RecuperarChamadosAsync();
		return _mapper.Map<List<ReadChamadoDto>>(listaChamado);
	}
	internal async Task<IEnumerable<ReadChamadoDto>> GetAllOpenAsync() {
		List<Chamado> listaChamado = await _chamadoRepository.RecuperarChamadosAbertosAsync();
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

	internal async Task<IEnumerable<ReadChamadoDto>> GetChamadosDoTecnico(ClaimsPrincipal user) {
		string username = _tokenService.GetUsernameFromToken(user);
		int tecnicoId = _tecnicoService.GetByUsernameAsync(username).Result.TecnicoId;

		List<Chamado> listaChamado = await _chamadoRepository.RecuperarChamadosDeTecnico(tecnicoId);
		return _mapper.Map<List<ReadChamadoDto>>(listaChamado);
	}


	internal async Task<bool> FinalizarChamadoAsync(FinalizarChamadoDto dto, ClaimsPrincipal user) {
		string username = _tokenService.GetUsernameFromToken(user);
		int tecnicoId = _tecnicoService.GetByUsernameAsync(username).Result.TecnicoId;
		dto.TecnicoId = tecnicoId;

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

	internal async Task<int> GetIdDoTecnicoAtribuido(int chamadoId) {
		ReadChamadoDto chamado = await GetByIdAsync(chamadoId);
		return chamado.TecnicoId;
	}

	internal async Task AtribuirTecnicoAChamado(CreateRespostaDto dto, string nomeAutor) {
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(dto.ChamadoId);
		int tecnicoId = _tecnicoService.GetByUsernameAsync(nomeAutor).Result.TecnicoId;
		chamado!.TecnicoId = tecnicoId;
		chamado.Status = EnumStatus.EmAndamento;
		await _chamadoRepository.UpdateChamadoAsync(chamado);
	}

	internal async Task<bool> GetStatusFechadoChamado(int chamadoId) {
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(chamadoId);

		if (chamado!.Status == EnumStatus.Fechado)
			return true;
		return false;
	}
}
