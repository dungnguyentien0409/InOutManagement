using System;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessEF
{
	public sealed class InOutManagementContext : DbContext
	{
		public InOutManagementContext()
		{
		}

        public InOutManagementContext(DbContextOptions options) : base(options)
        {
        }

		public DbSet<Door> Doors { get; set; }
		public DbSet<DoorRole> DoorRoles { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<UserInfo> UserInfos { get; set; }
		public DbSet<UserInfoRole> UserInfoRoles { get; set; }
		public DbSet<InOutHistory> InOutHistories { get; set; }
		public DbSet<ActionStatus> ActionStatues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:127.0.0.1,1441;Database=InOutManagement;User Id = SA;Password=MyPass@word;TrustServerCertificate=true");
            }
        }
    }
}

