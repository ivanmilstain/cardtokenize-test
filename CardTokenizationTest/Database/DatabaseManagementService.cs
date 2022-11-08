using Microsoft.EntityFrameworkCore;

namespace CardTokenizationTest.Database
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<TokenizationContext>().Database.Migrate();
            }
        }
    }
}

