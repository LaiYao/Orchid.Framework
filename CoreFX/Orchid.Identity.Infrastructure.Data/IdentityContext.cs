using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Orchid.Identity.Domain.Entities;

namespace Orchid.Identity.Infrastructure.Data
{
    public class IdentityContext : DbContext
    {
        DbSet<User> UserSet;

        DbSet<Organization> OrganizationSet;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasKey("Id")

            base.OnModelCreating(modelBuilder);
        }
    }
}
