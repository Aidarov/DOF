using DOF.WebService.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Database
{
    public class MainDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<OilWell> OilWells { get; set; }
        public DbSet<OilField> OilFields { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DevicePath> DevicePaths { get; set; }
        public DbSet<TabletUser> TabletUsers { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=dof;Username=postgres;Password=Orapas#123");
        }
    }
}
