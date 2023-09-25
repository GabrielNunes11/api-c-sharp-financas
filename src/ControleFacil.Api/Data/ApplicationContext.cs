using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Data.Mappings;
using ControleFacil.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<NatureRelease> NatureRelease { get; set; }
        public DbSet<ToPay> ToPay { get; set; }
        public DbSet<ToReceive> ToReceive { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new NatureReleaseMap());
            modelBuilder.ApplyConfiguration(new ToPayMap());
            modelBuilder.ApplyConfiguration(new ToReceiveMap());
            ///
        }
    }
}