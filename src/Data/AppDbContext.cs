using LuxuryPaintJohnsonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LuxuryPaintJohnsonAPI.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }

	public DbSet<Project> Projects { get; set; } = null!;

	public DbSet<Photo> Photos { get; set; } = null!;
}
