using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


public class AppDbContext : DbContext
{
	public DbSet<Number> Numbers { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=ComputeData.db");
	}
}

public class Number
{
	public int Value { get; set; }
	public bool IsPrime { get; set; }
}