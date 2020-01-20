namespace SecuroteckWebApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SecuroteckWebApplication.Models.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true; // Uncomment this line to do a db update that would cause data loss
            ContextKey = "SecuroteckWebApplication.Models.UserContext";
        }

        protected override void Seed(SecuroteckWebApplication.Models.UserContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
