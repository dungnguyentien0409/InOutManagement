using System;
using DomainHistory.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessEF
{
	public sealed class HistoryContext : DbContext
	{
		public HistoryContext()
		{
		}

        public HistoryContext(DbContextOptions options) : base(options)
        {
        }

		public DbSet<InOutHistory> InOutHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:127.0.0.1,1442;Database=History;User Id = SA;Password=MyPass@word;TrustServerCertificate=true");
            }
        }
    }
}

