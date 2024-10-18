﻿
using SendGrid;
using System.Security.Claims;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Data.DTOs;

namespace SolutisHelpDesk.Services;

public class EmailApiService {
	private IConfiguration _config;

	public EmailApiService(IConfiguration config) {
		_config = config;
	}

	public async Task EnviarNotificacaoDeAtualizacaoPorEmail(ReadClienteDto clienteDto) {
		string emailCliente = clienteDto.Email;
		string userName = clienteDto.UserName;

		var apiKey = _config["SendGridKey"];
		var client = new SendGridClient(apiKey);
		var from = new EmailAddress("bsgomes16@hotmail.com", "HelpDesk - Solutis");
		var subject = "Teve atualização no seu chamado!";
		var to = new EmailAddress(emailCliente, userName);
		var plainTextContent = $"Olá {userName}, teve atualização no seu chamado. ";
		var htmlContent = "<strong>Para mais detalhes, acesse o HelpDesk</strong>";
		var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
		var response = await client.SendEmailAsync(msg);
	}
}
