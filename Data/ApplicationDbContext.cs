﻿using BaseWebApplication.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Contracts;

namespace BaseWebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // add-migration Initial -o Data\Migrations

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AppUserSeedConfiguration());
            builder.ApplyConfiguration(new RoleSeedConfiguration());
            builder.ApplyConfiguration(new AppUserRoleSeedConfiguration());
        }

        public DbSet<AppUserConfig> AppUserConfig { get; set; }
        public DbSet<DummyClass> DummyClass { get; set; }
        public DbSet<DummyClassType> DummyClassType { get; set; }
    }
}
