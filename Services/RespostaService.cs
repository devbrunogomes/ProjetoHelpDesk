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

	public RespostaService(TokenService tokenService, IMapper mapper, RespostaRepository respostaRepository) {
		_tokenService = tokenService;
		_mapper = mapper;
		_respostaRepository = respostaRepository;
	}

	internal async Task<Resposta> RegistrarRespostaAsync(CreateRespostaDto dto, ClaimsPrincipal user) {
		string nomeAutor = _tokenService.GetUsernameFromToken(user);
		dto.Autor = nomeAutor;

		Resposta resposta = _mapper.Map<Resposta>(dto);
		await _respostaRepository.SalvarResposta(resposta);
		return resposta;
	}
}
