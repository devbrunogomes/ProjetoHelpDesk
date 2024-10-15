using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Repositories;
using System.Security.Claims;

namespace SolutisHelpDesk.Services;

public class RespostaService {
	private TokenService _tokenService;
	private IMapper _mapper;
	private RespostaRepository _respostaRepository;
	private ChamadoService _chamadoService;

	public RespostaService(TokenService tokenService, IMapper mapper, RespostaRepository respostaRepository, ChamadoService chamadoService) {
		_tokenService = tokenService;
		_mapper = mapper;
		_respostaRepository = respostaRepository;
		_chamadoService = chamadoService;
	}

	internal async Task<Resposta> RegistrarRespostaAoClienteAsync(CreateRespostaDto dto, ClaimsPrincipal user) {
		//Atribuir Técnico a chamado e alterar status
		string nomeAutor = _tokenService.GetUsernameFromToken(user);
		await _chamadoService.AtribuirTecnicoAChamado(dto, nomeAutor);

		//Salvar Resposta
		dto.Autor = nomeAutor;

		Resposta resposta = _mapper.Map<Resposta>(dto);
		await _respostaRepository.SalvarResposta(resposta);
		return resposta;
	}

	internal async Task<Resposta> RegistrarRespostaAoTecnicoAsync(CreateRespostaDto dto, ClaimsPrincipal user) {
		string nomeAutor = _tokenService.GetUsernameFromToken(user);
		dto.Autor = nomeAutor;

		Resposta resposta = _mapper.Map<Resposta>(dto);
		await _respostaRepository.SalvarResposta(resposta);
		return resposta;
	}
}
