
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
	private EmailApiService _emailApiService;

	public ChamadoService(IMapper mapper, ChamadoRepository chamadoRepository, TokenService tokenService, ClienteService clienteService, TecnicoService tecnicoService, ClimaApiService climaApiService, EmailApiService emailApiService) {
		_mapper = mapper;
		_chamadoRepository = chamadoRepository;
		_tokenService = tokenService;
		_clienteService = clienteService;
		_tecnicoService = tecnicoService;
		_climaApiService = climaApiService;
		_emailApiService = emailApiService;
	}


	internal async Task<Chamado> RegistroChamadaAsync(CreateChamadoDto chamadoDto, ClaimsPrincipal user) {
		string username = _tokenService.GetUsernameFromToken(user);
		int clienteId = _clienteService.GetByUsernameAsync(username).Result.ClienteId;
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
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(dto.ChamadoId);

		if (chamado == null || chamado.TecnicoId == null) {
			return false;
		}

		chamado.Status = EnumStatus.Fechado;
		chamado.DataConclusao = DateTime.Now;
		await _chamadoRepository.UpdateChamadoAsync(chamado);

		//Notificar o cliente de atualizacao
		var clienteId = chamado.ClienteId!.Value;
		ReadClienteDto cliente = await _clienteService.GetByIdAsync(clienteId);
		_emailApiService.EnviarNotificacaoDeAtualizacaoPorEmail(cliente).Wait();

		return true;
	}

	internal async Task<bool> ReatribuirChamadoAsync(ReatribuirChamadoDto dto) {
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(dto.ChamadoId);
		int tecnicoId = _tecnicoService.GetByUsernameAsync(dto.TecnicoUsername).Result.TecnicoId;

		if (chamado == null || chamado.Status == EnumStatus.Fechado) {
			return false;
		}

		chamado.TecnicoId = tecnicoId;
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

	internal async Task<bool> ValidarIgualdadeDeTecnico(ClaimsPrincipal user, int chamadoId) {
		string username = _tokenService.GetUsernameFromToken(user);
		int tecnicoIdToken = _tecnicoService.GetByUsernameAsync(username).Result.TecnicoId;
		int tecnicoIdAtribuido = await GetIdDoTecnicoAtribuido(chamadoId);

		if (tecnicoIdAtribuido != 0 && tecnicoIdAtribuido != tecnicoIdToken) {
			return false;
		}

		return true;
	}

	internal async Task<bool> AlterarPrioridadeAsync(AlterarPrioridadeChamadoDto dto) {
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(dto.ChamadoId);

		if (chamado == null) {
			return false;
		}

		chamado.Prioridade = dto.novaPrioridade;
		await _chamadoRepository.UpdateChamadoAsync(chamado);
		return true;
	}

	internal async Task<DadosChamadosDashboardDto> RetornarDadosChamadosDashboard() {
		var resultado = await _chamadoRepository.ObterContagemChamadosAsync();

		return resultado;
	}

	internal async Task<List<DadosTecnicosDashboardDto>> RetornarDadosTecnicosDashboard() {
		var resultado = await _chamadoRepository.ObterContagemTecnicosAsync();

		return resultado;
	}
}
