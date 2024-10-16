﻿using SolutisHelpDesk.Data.DTOs;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SolutisHelpDesk.Services;

public class ClimaApiService {
	private TokenService _tokenService;
	private ClienteService _clienteService;
	private readonly HttpClient _httpClient;

	public ClimaApiService(TokenService tokenService, ClienteService clienteService, HttpClient httpClient) {
		_tokenService = tokenService;
		_clienteService = clienteService;
		_httpClient = httpClient;
	}

	internal async Task ConferirClimaDaRegiao(CreateChamadoDto chamadoDto, ClaimsPrincipal user) {
		string username = _tokenService.GetUsernameFromToken(user);
		ReadClienteDto cliente = _clienteService.GetByUsernameAsync(username).Result;
		string cep = cliente.Cep;

		//Pegar o municipio do cliente através do via cep
		string municipio = await GetMunicipioViaCep(cep);      
	}

	private async Task<string> GetMunicipioViaCep(string cep) {
		string url = $"https://viacep.com.br/ws/{cep}/json/";		

		HttpResponseMessage response = await _httpClient.GetAsync(url);

		if (response.IsSuccessStatusCode) {
			string jsonResponse = await response.Content.ReadAsStringAsync();
			var data = JsonSerializer.Deserialize<ViaCepResponse>(jsonResponse);

			if (data != null && !string.IsNullOrEmpty(data.Localidade)) {
				return data.Localidade;
			} else {
				throw new Exception("Município não encontrado para o CEP informado.");
			}
		} else {
			throw new Exception("Erro ao acessar o serviço ViaCEP.");
		}

	}

	private class ViaCepResponse {
		[JsonPropertyName("localidade")]
		public string Localidade { get; set; }
	}
}
