using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SecuroteckWebApplication.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UserContext, Migrations.Configuration>());
        }

        public DbSet<User> Users { get; set; }
        
        //TODO: Task11

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}