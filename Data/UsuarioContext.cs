using Microsoft.EntityFrameworkCore;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Data;

public class UsuarioContext : DbContext {

	public UsuarioContext(DbContextOptions<UsuarioContext> opts) : base(opts) {

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		// Garantir que o campo UserName seja único
		modelBuilder.Entity<Cliente>()
			 .HasIndex(c => c.UserName)
			 .IsUnique()
			 .HasDatabaseName("IX_Cliente_UserName");

		modelBuilder.Entity<Tecnico>()
			 .HasIndex(t => t.UserName)
			 .IsUnique()
			 .HasDatabaseName("IX_Tecnico_UserName");

		modelBuilder.Entity<Administrador>()
			 .HasIndex(t => t.UserName)
			 .IsUnique()
			 .HasDatabaseName("IX_Administrador_UserName");


	}


	public DbSet<Cliente> Clientes { get; set; }
	public DbSet<Tecnico> Tecnicos { get; set; }
	public DbSet<Administrador> Administradores { get; set; }
}
