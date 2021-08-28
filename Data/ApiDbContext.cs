using System;
using UserLogin.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Sqlite;


namespace UserLogin.Data
{
	public class ApiDbContext : DbContext
	{
		public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
		{

		}
		public DbSet<user> users { get; set; }
		public DbSet<Favorite> favorites { get; set; }
	}
}
