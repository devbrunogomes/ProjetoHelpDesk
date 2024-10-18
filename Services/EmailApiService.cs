
using SendGrid;
using System.Security.Claims;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace SolutisHelpDesk.Services;

public class EmailApiService {
	private IConfiguration _config;

	public EmailApiService(IConfiguration config) {
		_config = config;
	}

	public async Task EnviarNotificacaoDeAtualizacaoPorEmail(ClaimsPrincipal user) {
		string emailCliente = user.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value!;

		var apiKey = _config["SendGridKey"];
		var client = new SendGridClient(apiKey);
		var from = new EmailAddress("bsgomes16@hotmail.com", "HelpDesk");
		var subject = "Teve atualização no seu chamado!";
		var to = new EmailAddress("test@example.com", "Example User");
		var plainTextContent = "and easy to do anywhere, even with C#";
		var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
		var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
		var response = await client.SendEmailAsync(msg);
	}
}
