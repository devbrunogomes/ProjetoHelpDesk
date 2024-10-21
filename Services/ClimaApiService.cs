using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Repositories;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SolutisHelpDesk.Services;

public class ClimaApiService {
	private TokenService _tokenService;
	private ClienteService _clienteService;
	private RespostaRepository _respostaRepository;
	private IMapper _mapper;
	private EmailApiService _emailApiService;
	private readonly IConfiguration _configuration;


	public ClimaApiService(TokenService tokenService, ClienteService clienteService, IConfiguration configuration, RespostaRepository respostaRepository, IMapper mapper, EmailApiService emailApiService) {
		_tokenService = tokenService;
		_clienteService = clienteService;
		_configuration = configuration;
		_respostaRepository = respostaRepository;
		_mapper = mapper;
		_emailApiService = emailApiService;
	}

	internal async Task ConferirClimaDaRegiao(Chamado chamado, ClaimsPrincipal user) {
		string username = _tokenService.GetUsernameFromToken(user);
		ReadClienteDto cliente = _clienteService.GetByUsernameAsync(username).Result;
		string cep = cliente.Cep;

		//Pegar o municipio do cliente através do via cep
		string municipio = await GetMunicipioViaCep(cep);

		//Pegar o clima através do municipio
		//string climaRegiao = await GetClimaViaApi(municipio);
		string climaRegiao = "Storm";

		if (climaRegiao != "Clean" && climaRegiao != "Clouds") {
			await RegistrarRespostaClimaticaAsync(chamado);
			await _emailApiService.EnviarNotificacaoDeInstabilidadeClimatica(username, cliente.Email);
		}

	}

	internal async Task RegistrarRespostaClimaticaAsync(Chamado chamado) {
		CreateRespostaDto dto = new CreateRespostaDto();
		dto.Autor = "BotClima";
		dto.ChamadoId = chamado.ChamadoId;
		dto.Mensagem = "Foi detectado uma instabilidade climática na sua região, que pode ocasionar dificuldades técnicas, aguarde mais instruções de um técnico";

		Resposta resposta = _mapper.Map<Resposta>(dto);
		await _respostaRepository.SalvarResposta(resposta);
	}

	private async Task<string> GetClimaViaApi(string municipio) {
		string apiKey = _configuration["ClimaApiKey"]!;
		string url = $"https://api.openweathermap.org/data/2.5/weather?q={Uri.EscapeDataString(municipio)},br&appid={apiKey}";

		using (HttpClient httpClient = new HttpClient()) {
			HttpResponseMessage response = await httpClient.GetAsync(url);

			if (response.IsSuccessStatusCode) {
				string jsonResponse = await response.Content.ReadAsStringAsync();
				var data = JsonSerializer.Deserialize<OpenWeatherResponse>(jsonResponse);

				if (data != null && data.Weather != null && data.Weather.Any()) {
					return data.Weather[0].Main;
				} else {
					throw new Exception("Informação de clima não encontrada.");
				}
			} else {
				throw new Exception("Erro ao acessar o serviço OpenWeather.");
			}
		}
	}

	private async Task<string> GetMunicipioViaCep(string cep) {
		string url = $"https://viacep.com.br/ws/{cep}/json/";

		using (var httpClient = new HttpClient()) {
			HttpResponseMessage response = await httpClient.GetAsync(url);

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

	}
	#region("Classes para desserialização")
	// Classe auxiliar para deserializar a resposta JSON
	private class OpenWeatherResponse {
		[JsonPropertyName("weather")]
		public List<WeatherInfo> Weather { get; set; }
	}

	private class WeatherInfo {
		[JsonPropertyName("main")]
		public string Main { get; set; }
	}

	private class ViaCepResponse {
		[JsonPropertyName("localidade")]
		public string Localidade { get; set; }
	}

	#endregion
}

