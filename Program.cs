
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolutisHelpDesk.Data;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Repositories;
using SolutisHelpDesk.Services;

namespace SolutisHelpDesk;

public class Program {
	public static async Task Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddDbContext<UsuarioContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

		builder.Services
			.AddIdentity<Usuario, IdentityRole<int>>()
			.AddEntityFrameworkStores<UsuarioContext>()
			.AddDefaultTokenProviders();

		builder.Services.AddScoped<ClienteService>();
		builder.Services.AddScoped<ClienteRepository>();
		builder.Services.AddScoped<TecnicoService>();
		builder.Services.AddScoped<TecnicoRepository>();
		builder.Services.AddScoped<AdministradorService>();
		builder.Services.AddScoped<AdministradorRepository>();
		builder.Services.AddScoped<ChamadoService>();
		builder.Services.AddScoped<ChamadoRepository>();

		builder.Services.AddScoped<UsuarioService>();

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

		// Inicializa os papéis
		using (var scope = app.Services.CreateScope()) {
			var services = scope.ServiceProvider;
			await RoleInitializer.InitializeAsync(services);
		}

		app.Run();
	}
}
