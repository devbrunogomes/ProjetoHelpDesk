﻿using AutoMapper;
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
	private EmailApiService _emailApiService;
	private ClienteService _clienteService;

	public RespostaService(TokenService tokenService, IMapper mapper, RespostaRepository respostaRepository, ChamadoService chamadoService, EmailApiService emailApiService, ClienteService clienteService) {
		_tokenService = tokenService;
		_mapper = mapper;
		_respostaRepository = respostaRepository;
		_chamadoService = chamadoService;
		_emailApiService = emailApiService;
		_clienteService = clienteService;
	}

	internal async Task<Resposta> RegistrarRespostaAoClienteAsync(CreateRespostaDto dto, ClaimsPrincipal user) {
		//Atribuir Técnico a chamado e alterar status
		string nomeAutor = _tokenService.GetUsernameFromToken(user);
		await _chamadoService.AtribuirTecnicoAChamado(dto, nomeAutor);
		dto.Autor = nomeAutor;

		//Salvar Resposta
		Resposta resposta = _mapper.Map<Resposta>(dto);
		await _respostaRepository.SalvarResposta(resposta);

		//Notificar o cliente de atualizacao
		var clienteId = _chamadoService.GetByIdAsync(dto.ChamadoId).Result.ClienteId;
		ReadClienteDto cliente = await _clienteService.GetByIdAsync(clienteId);
		_emailApiService.EnviarNotificacaoDeAtualizacaoPorEmail(cliente).Wait();

		return resposta;
	}

	internal async Task<Resposta> RegistrarRespostaAoTecnicoAsync(CreateRespostaDto dto, ClaimsPrincipal user) {
		string nomeAutor = _tokenService.GetUsernameFromToken(user);
		dto.Autor = nomeAutor;

		Resposta resposta = _mapper.Map<Resposta>(dto);
		await _respostaRepository.SalvarResposta(resposta);
		return resposta;
	}

	internal async Task RegistrarRespostaClimaticaAsync(Chamado chamado) {
		CreateRespostaDto dto = new CreateRespostaDto();
		dto.Autor = "BotClima";
		dto.ChamadoId = chamado.ChamadoId;
		dto.Mensagem = "Foi detectado uma instabilidade climática na sua região, que pode ocasionar dificuldades técnicas, aguarde mais instruções de um técnico";

		Resposta resposta = _mapper.Map<Resposta>(dto);
		await _respostaRepository.SalvarResposta(resposta);
	}
}
