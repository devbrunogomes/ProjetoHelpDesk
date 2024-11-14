using Microsoft.AspNetCore.Identity;

namespace SolutisHelpDesk;

public static class RoleInitializer {
	public static async Task InitializeAsync(IServiceProvider serviceProvider) {
		var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

		string[] roleNames = { "CLIENTE", "TECNICO", "ADMINISTRADOR" };

		foreach (var roleName in roleNames) {
			// Verifica se o papel já existe, e caso contrário, cria-o
			if (!await roleManager.RoleExistsAsync(roleName)) {
				await roleManager.CreateAsync(new IdentityRole<int>(roleName));
			}
		}
	}
}

