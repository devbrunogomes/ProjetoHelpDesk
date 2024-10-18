
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SolutisHelpDesk.Data;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Repositories;
using SolutisHelpDesk.Services;
using System.Text;

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
		builder.Services.AddAuthentication(options => {
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options => {
			options.TokenValidationParameters = new TokenValidationParameters {
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = builder.Configuration["Jwt:Issuer"],
				ValidAudience = builder.Configuration["Jwt:Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
			};
		});

		builder.Services.AddScoped<ClienteService>();
		builder.Services.AddScoped<ClienteRepository>();
		builder.Services.AddScoped<TecnicoService>();
		builder.Services.AddScoped<TecnicoRepository>();
		builder.Services.AddScoped<AdministradorService>();
		builder.Services.AddScoped<AdministradorRepository>();
		builder.Services.AddScoped<ChamadoService>();
		builder.Services.AddScoped<ChamadoRepository>();
		builder.Services.AddScoped<RespostaService>();
		builder.Services.AddScoped<RespostaRepository>();
		builder.Services.AddScoped<ClimaApiService>();

		builder.Services.AddScoped<TokenService>();
		builder.Services.AddScoped<UsuarioService>();

		builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(options => {
			options.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

			// Configuração para adicionar o campo de Autorização no Swagger
			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = "Insira o token JWT no formato: Bearer {seu token}"
			});

			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						 Reference = new OpenApiReference
						 {
							  Type = ReferenceType.SecurityScheme,
							  Id = "Bearer"
						 }
					},
					new string[] {}
				}
			});
		});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();
		} else {
			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
				c.RoutePrefix = string.Empty;
			});
		}



		app.UseAuthentication();
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
