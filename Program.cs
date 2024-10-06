
using Microsoft.EntityFrameworkCore;
using SolutisHelpDesk.Data;
using SolutisHelpDesk.Repositories;
using SolutisHelpDesk.Services;

namespace SolutisHelpDesk;

public class Program {
	public static void Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddDbContext<UsuarioContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

		builder.Services.AddScoped<ClienteService>();
		builder.Services.AddScoped<ClienteRepository>();
		builder.Services.AddScoped<TecnicoService>();
		builder.Services.AddScoped<TecnicoRepository>();
		builder.Services.AddScoped<AdministradorService>();
		builder.Services.AddScoped<AdministradorRepository>();

		builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseAuthorization();


		app.MapControllers();

		app.Run();
	}
}
